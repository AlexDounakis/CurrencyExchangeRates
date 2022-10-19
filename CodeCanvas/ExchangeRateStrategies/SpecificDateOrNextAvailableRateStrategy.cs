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
            
            var ratesByDay = await _repository.GetRatesByCondition(date, currencyCodeFrom);
            if (ratesByDay.Count() == 0)
            {
                date = date.AddDays(1);
                ratesByDay = await _repository.GetRatesByCondition(date, currencyCodeFrom);
                if (ratesByDay.Count() == 0)
                    return System.Convert.ToDecimal(-1);  //return 404 ,  throw exception
            }
            var rate = ratesByDay.Where(x => x.Rate.Equals(currencyCodeFrom)).FirstOrDefault();
            if (rate != null)
                return rate.Rate;
            else
                return System.Convert.ToDecimal(-1);// throw exception 

        }
    }
}
