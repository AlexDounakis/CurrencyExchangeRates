using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.Xml;

namespace EuropeanCentralBank
{
	internal class EuropeanCentralBankClient : IEuropeanCentralBankClient
	{
		private readonly EuropeanCentralBankSettings _settings;
		private readonly IHttpClientFactory _httpClient;

        public EuropeanCentralBankClient(IOptions<EuropeanCentralBankSettings> settings , IHttpClientFactory clientFactory)
		{
			_settings = settings.Value;
			_httpClient = clientFactory;
		}

		public async Task<RatesResponse> GetRates()
		{
			// todo: implement EuropeanCentralBankClient.GetRates()

			// 1) make http call to European Central Bank (_settings.Endpoint) to get the latest rates
			// 2) parse response
			// 3) create RatesResponse
			// 4) return RatesResponse
			
			var client = _httpClient.CreateClient();
			client.BaseAddress = new Uri(_settings.RatesEndpoint);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Add(HeaderNames.Accept ,"application/xml");
			//var request = new HttpRequestMessage(HttpMethod.Get, _settings.RatesEndpoint);			

			var response = await client.GetAsync($"");
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStreamAsync();
			
            XmlSerializer serializer = new XmlSerializer(typeof(EuropeanCentralBank.Envelope));

			Envelope env = (Envelope)serializer.Deserialize(XmlReader.Create(responseString));

			RatesResponse rates = new RatesResponse()
			{
				Date = env.Cube.Cube1.time,
				Rates = env.Cube.Cube1.Cube.Select(a => new CurrencyRate(a.currency, a.rate)).Where(b =>b.CurrencyCode != null).ToList()
				
			};
				// func must return RatesReponse -> DateTime , IReadOnlyCollection<CurrencyRate>
            return rates; ;
		}
	}
}
