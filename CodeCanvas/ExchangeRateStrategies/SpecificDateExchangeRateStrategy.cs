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
            var rateFrom = await _repository.GetRateByCondition(date, currencyCodeFrom);
            if (rateFrom == null)
                throw new Exception("Rate For Specific Date Not Found...");  //return 404

            var rateTo = await _repository.GetRateByCondition(date, currencyCodeTo);
            if (rateTo == null)
                throw new Exception("Rate For Specific Date Not Found...");  //return 404

            var finalRate = System.Convert.ToDecimal(rateTo.Rate / rateFrom.Rate);
            return finalRate;
        }
    }
}
