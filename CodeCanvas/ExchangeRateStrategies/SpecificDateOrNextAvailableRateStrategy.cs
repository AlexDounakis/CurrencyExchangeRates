using AutoMapper;
using AutoMapper.Execution;
using CodeCanvas.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCanvas.ExchangeRateStrategies
{
    public class SpecificDateOrNextAvailableRateStrategy : ExchangeRateStrategyBase
    {
        private readonly IRatesRepository _repository;
        private readonly ILogger<SpecificDateOrNextAvailableRateStrategy> _logger;
        private readonly IMapper _mapper;

        public SpecificDateOrNextAvailableRateStrategy(IRatesRepository repository, ILogger<SpecificDateOrNextAvailableRateStrategy> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        protected override async Task<decimal> GetRate(string currencyCodeFrom, string currencyCodeTo, DateTime date)
        {
            var rateFrom = await _repository.GetRateByCondition(date, currencyCodeFrom); //DateTime.Today.AddDays(3)
            var rateTo = await _repository.GetRateByCondition(date, currencyCodeTo);
            
            if (rateFrom ==null || rateTo ==null)
            {
                var sortedRatesByDate = await _repository.GetNextAvailableRateAsync();
                var nextAvailableDate = sortedRatesByDate.First();
                rateFrom = await _repository.GetRateByCondition(nextAvailableDate.CreatedAt, currencyCodeFrom);
                rateTo = await _repository.GetRateByCondition(nextAvailableDate.CreatedAt, currencyCodeTo);
                if (rateFrom == null || rateTo == null)
                    throw new Exception("Rate For Specific Date Not Found...");  //return 404 ,  
            }

            var finalRate = System.Convert.ToDecimal(rateTo.Rate / rateFrom.Rate);
            return finalRate;
        }
    }
}
