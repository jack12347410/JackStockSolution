using JackStockApi.Domain;
using JackStockApi.Dtos;
using JackStockApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JackStockApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;
        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertStock([FromBody] IEnumerable<StockDto> dtos)
        {
            var result = await _stockService.InsertBatchStockAsync(dtos);
            return Ok(result);
        }

        [HttpPost("StockDayHis")]
        public async Task<IActionResult> InsertStockDayHisList([FromBody] IList<StockDayHistoryDto> dtos)
        {
            var result = await _stockService.InsertBatchStockDayHisAsync(dtos);
            return Ok(result);
        }
    }
}
