namespace EuropeanCentralBank
{
	public class CurrencyRate
	{
		public string? CurrencyCode { get; set; }
		public decimal? Rate { get; set;  }

		public CurrencyRate(string CurrencyCode, decimal rate)
		{
			this.CurrencyCode= CurrencyCode;
			Rate = rate;
		}
		public CurrencyRate()
		{

		}
	}
}
