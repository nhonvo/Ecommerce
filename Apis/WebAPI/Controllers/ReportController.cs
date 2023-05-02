using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;
        public ReportController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        // [HttpGet("/sales/{year}/{month}")]
        // public async Task<ActionResult<SalesReport>> GetSalesByMonth(int year, int month)
        // {
        //     DateTime startDate = new DateTime(year, month, 1);
        //     DateTime endDate = startDate.AddMonths(1).AddDays(-1);

        //     return await GetSalesReport(startDate, endDate);
        // }

        // [HttpGet("/sales")]
        // public async Task<ActionResult<SalesReport>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        // {
        //     return await GetSalesReport(startDate, endDate);
        // }

    }
}