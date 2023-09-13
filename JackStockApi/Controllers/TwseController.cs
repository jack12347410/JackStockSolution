using JackStockApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JackStockApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class TwseController: ControllerBase
    {
        private readonly TwseService _twseService;
        private readonly StockService _stockService;

        public TwseController(TwseService twseService, StockService stockService) 
        {
            _twseService = twseService;
            _stockService = stockService;
        }

        /// <summary>
        /// 自動新增股市歷史資料(每日)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> InsertStockDayHisByTwse(string stockCode)
        {
            var result = await _twseService.GetTwseStockDayAsync(stockCode);
            return Ok(result);
        }
    }
}
