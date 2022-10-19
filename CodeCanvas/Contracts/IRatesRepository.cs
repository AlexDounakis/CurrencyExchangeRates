using CodeCanvas.Entities;
using EuropeanCentralBank;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CodeCanvas.Contracts
{
    public interface IRatesRepository
    {
        public Task<IEnumerable<CurrencyRateEntity>> GetRatesAsync();
        public Task<IEnumerable<CurrencyRateEntity>> GetNextAvailableRateAsync();
        public Task<IEnumerable<CurrencyRateEntity>> GetRatesByCondition(DateTime date);
        public Task<CurrencyRateEntity> GetRateByCondition(DateTime date, string currencyCode);
        public Task<IEnumerable<CurrencyRateEntity>> GetRatesByCondition(DateTime date, string currencyCode);
        public void AddRates(IEnumerable<CurrencyRateEntity> rates);
        public void UpdateRates(IEnumerable<CurrencyRateEntity> rates);
        public void SaveChangesAsync();
    }
}
