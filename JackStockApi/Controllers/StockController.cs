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
        private readonly StockService _stockDayHistoryService;
        public StockController(StockService stockDayHistoryService)
        {
            _stockDayHistoryService = stockDayHistoryService;
        }

        [HttpPost("StockDayHis")]
        public async Task<IActionResult> InsertStockDayHis([FromBody] StockDayHistoryDto dto)
        {
            var result = await _stockDayHistoryService.InsertAsync(dto);
            return Ok(result);
        }

        [HttpPost("StockDayHis/List")]
        public async Task<IActionResult> InsertStockDayHisList([FromBody] IList<StockDayHistoryDto> dtos)
        {
            var result = await _stockDayHistoryService.InsertAsync(dtos);
            return Ok(result);
        }

        //public async Task<IActionResult> InsertStock([FromBody])
    }
}
