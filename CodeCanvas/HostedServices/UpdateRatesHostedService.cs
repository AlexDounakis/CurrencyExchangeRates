using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutoMapper;
using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using EuropeanCentralBank;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog;
using System.Net.Http;
using System.Timers;
using CodeCanvas.Database;
using System.Net;

namespace CodeCanvas.HostedServices
{
	public class UpdateRatesHostedService : BackgroundService
	{
        //private readonly ILogger<UpdateRatesHostedService> _logger;
        public IServiceProvider Services { get; }
        public UpdateRatesHostedService(IServiceProvider services )
        {
          
            this.Services = services;
        }


        public override Task StopAsync(CancellationToken cancellationToken)
		{
            Log.Information("UpdateRatesHostedService is stopping.");
            return Task.CompletedTask;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Log.Information("Execute Async Started ...");
                while (!stoppingToken.IsCancellationRequested)
                {
                    await UpdateDatabase(stoppingToken);
                    await Task.Delay(TimeSpan.FromSeconds(5) , stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Log.Information("Something happend in Execute async()" , ex);
            }
        }

        private async Task UpdateDatabase(CancellationToken cancellationToken)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedRepositoryService = scope.ServiceProvider.GetRequiredService<IRatesRepository>();
                var _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                var _client = scope.ServiceProvider.GetRequiredService<IEuropeanCentralBankClient>();

                var ratesDaily = await _client.GetRates();
                var ratesDatabase = await scopedRepositoryService.GetRatesByCondition(DateTime.Today);
                var ratesTesting = await scopedRepositoryService.GetRatesAsync();
                if (ratesDaily.Rates != null)
                {
                    var ratesToAdd = _mapper.Map<IEnumerable<EuropeanCentralBank.CurrencyRate>, IEnumerable<CurrencyRateEntity>>(ratesDaily.Rates);


                    if (ratesDatabase.Count() == 0)
                    {
                        scopedRepositoryService.AddRates(ratesToAdd.Where(x => x.CurrencyCode != null).ToList());
                    }
                    else
                    {
                        // Update entity  Entity.Update(ratesToAdd.Rate)



                        ratesDatabase
                            .ToList()
                            .ForEach(x =>
                            {
                                var record = ratesDatabase.First(y => ratesToAdd.Any(rate => rate.CurrencyCode != null && rate.CurrencyCode == y.CurrencyCode));
                                x.Update(record.Rate);
                            });

                        // Update Database
                        scopedRepositoryService.UpdateRates(
                            ratesDatabase.Where(x => ratesToAdd.Any(y => y != null
                                                                      && y.CurrencyCode != null
                                                                      && y.CurrencyCode.Equals(x.CurrencyCode))));

                    }
                    scopedRepositoryService.SaveChangesAsync();
                    //_logger.LogInformation("UpdateRatesHostedService rates updated.");    
                }
                else
                    throw new Exception("Rates are null");
                

            }

        }

	}
}
