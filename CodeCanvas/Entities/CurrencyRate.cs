using System;

namespace CodeCanvas.Entities
{
	public class CurrencyRateEntity
	{
		/// <summary>
		/// currency id
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// ISO code of the currency
		/// </summary>
		public string? CurrencyCode { get; private set; }
		/// <summary>
		/// currency rate related to EUR
		/// </summary>
		public decimal Rate { get; private set; }
		/// <summary>
		/// creation date of the Currency
		/// </summary>
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// date of the last update of the Currency
		/// </summary>
		public DateTime UpdatedAt { get; private set; }

		public CurrencyRateEntity(int id, string currencyCode, decimal rate, DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			CurrencyCode = currencyCode;
			Rate = rate;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
		public CurrencyRateEntity()
		{
			//CreatedAt = DateTime.Today;
		}
		public void Update(decimal rate)
		{
			Rate = rate;
			UpdatedAt = DateTime.Today;
		}
	}
}
