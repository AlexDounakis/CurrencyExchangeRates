using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCanvas.Contracts;
using CodeCanvas.Entities;
using CodeCanvas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CodeCanvas.Controllers
{
	[ApiController]
	[Route("api/rates")]
	public class RatesController : ControllerBase
	{
		private readonly ILogger<RatesController> _logger;
		private readonly IRatesRepository _repository;
		private readonly IMapper _mapper;
		
		public RatesController(ILogger<RatesController> logger , IRatesRepository ratesRepository , IMapper mapper)
		{
			_logger = logger;
			_repository = ratesRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(CurrencyRateModel[]), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetRates([FromQuery] DateTime date)
		{

			// todo: implement RatesController.GetRates()

			// get rates for the requested date, or 404 (not found) in case requested date is missing
			//_logger.LogWarning(Request.Headers.ToString());
            //Log.Information(Request.Headers.ToString());

            var ratesByDay = await _repository.GetRatesByCondition(date);
			if (ratesByDay.Count() == 0)
				return NotFound();//return 404

			
			var ratesToReturn =  _mapper.Map< IEnumerable<CurrencyRateEntity>, IEnumerable<CurrencyRateModel>>(ratesByDay);

			// log each request along with its corresponding response
			_logger.LogWarning("Response Status code Logging"+Response.StatusCode.ToString());
            _logger.LogWarning("Response Body Logging" + Response.Body.ToString());

            return Ok(ratesToReturn);
        }
	}
}
