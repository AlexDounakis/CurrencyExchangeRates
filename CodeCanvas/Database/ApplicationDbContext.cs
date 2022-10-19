#nullable disable
using System;
using System.Data.Common;
using System.Reflection;
using CodeCanvas.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeCanvas.Database
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CurrencyRateEntity> CurrencyRates { get; set; }
		public DbSet<WalletEntity> Wallets { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<WalletEntity>().HasData(new WalletEntity(1, "USD", System.Convert.ToDecimal(0) , DateTime.Today , DateTime.Today)) ;
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(builder);
		}
	}
}
