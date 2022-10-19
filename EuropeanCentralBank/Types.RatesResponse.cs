using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Serialization;
namespace EuropeanCentralBank
{
    [XmlRootAttribute(Namespace = "http://www.gesmes.org/xml/2002-08-01", IsNullable = false, ElementName = "Cube")]
    public class RatesResponse
	{
        //nullable for no params contstuctor
        [XmlElement(Namespace = "")]
        public DateTime? Date { get; set; }

        [XmlElement(Namespace = "")]
        public IReadOnlyCollection<CurrencyRate>?  Rates { get; set; }

		public RatesResponse(DateTime date, IReadOnlyCollection<CurrencyRate> rates)
		{
			Date = date;
			Rates = rates;
		}

		//param less constructor used in xmlserilization
		public RatesResponse()
		{
			
		}
	}
}
