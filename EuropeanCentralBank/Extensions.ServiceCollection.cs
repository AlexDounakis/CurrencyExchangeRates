using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EuropeanCentralBank
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEuropeanCentralBank(this IServiceCollection services, IConfiguration configuration)
		{
			// todo: register EuropeanCentralBankClient and EuropeanCentralBankSettings

			// use this method to:
			// 1) register EuropeanCentralBankClient
			// 2) register the EuropeanCentralBankSettings 

			//1) registering EuropeanCentralBankClient
			services.AddScoped<EuropeanCentralBank.IEuropeanCentralBankClient, EuropeanCentralBank.EuropeanCentralBankClient>();

			//2) EuropeanCentralBankSettings
			services.Configure<EuropeanCentralBank.EuropeanCentralBankSettings>(configuration.GetSection(EuropeanCentralBankSettings._europeanCentralBankSettings));
            
            // create 
            services.AddHttpClient();

			return services;
		}

	}
}
