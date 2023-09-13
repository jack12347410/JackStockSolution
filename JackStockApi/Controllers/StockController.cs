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

        [HttpGet("Test")]
        public string GetTest()
        {
            return "Test";
        }

        /// <summary>
        /// 取得股票代號
        /// </summary>
        /// <param name="marketTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStocksByMarketTypeIdAsync(int? marketTypeId)
        {
            var result = await _stockService.FindStockByMarketTypeIdAsync(marketTypeId);
            return Ok(result);
        }

        /// <summary>
        /// 新增股票代號
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertStock([FromBody] IEnumerable<StockDto> dtos)
        {
            var result = await _stockService.InsertBatchStockAsync(dtos);
            return Ok(result);
        }

        /// <summary>
        /// 新增股市歷史資料(每日)
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost("StockDayHis")]
        public async Task<IActionResult> InsertStockDayHisList([FromBody] IList<StockDayHistoryDto> dtos)
        {
            var result = await _stockService.InsertBatchStockDayHisAsync(dtos);
            return Ok(result);
        }

        
    }
}
