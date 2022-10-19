using System;
using System.Threading.Tasks;
using AutoMapper;
using CodeCanvas.Contracts;
using CodeCanvas.Models;
using CodeCanvas.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCanvas.Controllers
{
	[ApiController]
	[Route("api/wallets")]
	public class WalletsController : ControllerBase
	{
		private readonly ILogger<WalletsController> _logger;
		private readonly IWalletAdjustmentService _walletAdjustmentService;
		private readonly IWalletRepository _walletRepository;
		private readonly IMapper _mapper;


        public WalletsController(ILogger<WalletsController> logger , IWalletAdjustmentService wallet , IWalletRepository walletRepository ,IMapper mapper)
		{
			_logger = logger;
            _walletAdjustmentService = wallet;
			_walletRepository = walletRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetWallet()
		{
			var wallet = await _walletRepository.GetWalletAsync();

			return Ok();
		}

		[HttpPost]
		[ProducesResponseType(typeof(CurrencyRateModel[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> AdjustBalance([FromQuery] string exchangeRateStrategy, [FromBody] AdjustBalancePayload payload)
		{
			// todo: implement WalletsController.AdjustBalance()
			
			try
			{
				// use IWalletAdjustmentService.AdjustBalance() to adjust the balance of the wallet
				if (exchangeRateStrategy == null)
					throw new Exception();
                var res = await _walletAdjustmentService.AdjustBalance(exchangeRateStrategy,payload.WalletId,payload.CurrencyCode,payload.Amount);

                // return 400 (bad request) in case ther is no sufficient balance to subtract

                // return new balance
				return Ok(res);
            }
            catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}
