﻿using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using CodeCanvas.ExchangeRateStrategies;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CodeCanvas.Services
{
	public interface IWalletAdjustmentService
	{
		Task<decimal> AdjustBalance(string exchangeRateStrategy, int walletId, string currencyCode, decimal amount);
	}

	class WalletAdjustmentService : IWalletAdjustmentService
	{
		private readonly SpecificDateExchangeRateStrategy _strategyA;
		private readonly SpecificDateOrNextAvailableRateStrategy _strategyB;
		private readonly IWalletRepository _wallet;
		public WalletAdjustmentService(SpecificDateExchangeRateStrategy _specificDateExchangeRateStrategy , SpecificDateOrNextAvailableRateStrategy _specificDateOrNextAvailableRateStrategy, IWalletRepository wallet)
		{
			_strategyA = _specificDateExchangeRateStrategy;
			_strategyB = _specificDateOrNextAvailableRateStrategy;
			_wallet = wallet;
		}
		public async Task<decimal> AdjustBalance(string exchangeRateStrategy, int walletId, string currencyCode, decimal amount)
		{
			// todo: implement WalletAdjustmentService.AdjustBalance()

			var numerableWallets= await _wallet.GetWalletByConditionAsync(walletId);
            var wallet = numerableWallets.FirstOrDefault();
            if (wallet == null)
				throw new System.Exception("Wallet not found");
			
			var currencyCodeTo = wallet.CurrencyCode;

            // choose the corresponding IExchangeRateStrategy based on exchangeRateStrategy
            // use IExchangeRateStrategy.Convert() to convert the amount into the currency of the wallet
            decimal result = 0;
			switch (exchangeRateStrategy)
			{
				case "A":
					result = await _strategyA.Convert(amount ,currencyCode ,currencyCodeTo , DateTime.Today);
					break;
				case "B":
                    result = await _strategyB.Convert(amount, currencyCode, currencyCodeTo, DateTime.Today);
                    break;
			}

			// bring WalletEntity and call Adjust() to adjust the balance
			try {
                wallet.Adjust(result);
            }
			catch(NoSufficientBalanceException ex)
			{
				throw new Exception(ex.Message);
			}
			
			// persist the changes
			_wallet.SaveChangesAsync();
			// retun new balance
			return wallet.Balance;
			
		}
	}
}
