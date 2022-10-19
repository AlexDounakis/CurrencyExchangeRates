using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCanvas.Database
{
    public class RatesRepository : RepositoryBase<CurrencyRateEntity> , IRatesRepository
    {
        private readonly ApplicationDbContext _context;
        
        public RatesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyRateEntity>> GetRatesAsync() => await FindAll().ToListAsync();
        public async Task<IEnumerable<CurrencyRateEntity>> GetRatesByCondition(DateTime date)
        {
            return await FindByCondition(c =>  c.CreatedAt.Date == date.Date).ToListAsync();
        }
        public async Task<IEnumerable<CurrencyRateEntity>> GetRatesByCondition(DateTime date , string currencyCode)
        {
            return await FindByCondition(c => c.CreatedAt.Date == date.Date && c.CurrencyCode == currencyCode).ToListAsync();
        } 
        public void AddRates(IEnumerable<CurrencyRateEntity> rates) => rates.ToList().ForEach(rate=> Create(rate));
        public void UpdateRates(IEnumerable<CurrencyRateEntity> rates) => rates.ToList().ForEach(rate => Update(rate));
        public void SaveChangesAsync()
        {
            _context.SaveChanges();
        }
    }
}
