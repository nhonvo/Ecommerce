using Application.Interfaces;
using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;
        public ReportController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("/sales/{year}/{month}")]
        public async Task<ActionResult<ApiResult<SalesReport>>> GetSalesByMonth(int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var response = await _orderDetailService.GetSalesReport(startDate, endDate);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("/sales")]
        public async Task<ActionResult<SalesReport>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            var response = await _orderDetailService.GetSalesReport(startDate, endDate);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }
    }
}