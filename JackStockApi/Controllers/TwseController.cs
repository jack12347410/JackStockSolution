﻿using JackStockApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JackStockApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class TwseController: ControllerBase
    {
        private readonly TwseService _twseService;

        public TwseController(TwseService twseService) 
        {
            _twseService = twseService;
        }

        /// <summary>
        /// 新增股市(日)歷史資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("StockDayHis")]
        public async Task<IActionResult> InsertStockDayHisByTwseAsync(string stockCode, string? date)
        {
            var result = await _twseService.GetTwseStockDayAndInsertToDbAsync(stockCode, date);
            return result == null? NotFound() : Ok(result);
        }
    }
}
