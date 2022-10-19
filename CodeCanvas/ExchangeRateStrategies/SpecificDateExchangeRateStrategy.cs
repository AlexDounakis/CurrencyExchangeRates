using AutoMapper;
using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using CodeCanvas.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCanvas.ExchangeRateStrategies
{
    public class SpecificDateExchangeRateStrategy : ExchangeRateStrategyBase
    {
        private readonly IRatesRepository _repository;
        private readonly ILogger<SpecificDateExchangeRateStrategy> _logger;
        private readonly IMapper _mapper;
        public SpecificDateExchangeRateStrategy(IRatesRepository repository , ILogger<SpecificDateExchangeRateStrategy> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        protected override async Task<decimal> GetRate(string currencyCodeFrom, string currencyCodeTo, DateTime date)
        {
            var ratesByDay = await _repository.GetRatesByCondition(date, currencyCodeFrom);
            if (ratesByDay.Count() == 0)
                throw new Exception("Rate For Specific Date Not Found...");  //return 404

            // specific rate
            var rate = ratesByDay.Where(x => x.Rate.Equals(currencyCodeFrom)).FirstOrDefault();

            //var rateToReturn = _mapper.Map<CurrencyRateEntity, CurrencyRateModel>(rate);
            if (rate != null)
                return rate.Rate;
            else
                throw new Exception("rate to return == null");
        }
    }
}
