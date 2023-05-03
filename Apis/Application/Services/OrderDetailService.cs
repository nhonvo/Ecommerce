using Application;
using Application.Interfaces;
using Application.ViewModels.Order;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private async Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderDetail> orderDetails)
        {
            decimal totalPrice = 0;

            foreach (var item in orderDetails)
            {
                var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                /// if product is not found for this ProductId
                if (product == null)
                {
                    throw new Exception($"Product not found for ProductId: {item.ProductId}");
                }

                decimal itemPrice = product.Price * item.Quantity;
                totalPrice += itemPrice;
            }

            return totalPrice;
        }
        public async Task<ApiResult<SalesReport>> GetSalesReport(DateTime startDate, DateTime endDate)
        {
            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: o => o.OrderDate >= startDate && o.OrderDate <= endDate,
                include: o => o.Include(o => o.OrderDetails),
                pageIndex: 0,
                pageSize: int.MaxValue);

            var orderDetail = orders.Items.SelectMany(x=>x.OrderDetails);
            var total = await CalculateTotalPriceAsync(orderDetail);


            var salesByDay = orders.Items.GroupBy(
                    o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month, Day = o.OrderDate.Day })
                .Select(g => new { Date = g.Key, TotalSales = total })
                .OrderBy(g => g.Date.Year)
                .ThenBy(g => g.Date.Month)
                .ThenBy(g => g.Date.Day)
                .ToList();

            var totalSales = salesByDay.Sum(s => s.TotalSales);

            var salesReport = new SalesReport
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalSales = totalSales,
                DailySales = salesByDay.Select(s => new DailySales
                {
                    Date = new DateTime(s.Date.Year, s.Date.Month, s.Date.Day),
                    TotalSales = s.TotalSales
                }).ToList()
            };

            return new ApiSuccessResult<SalesReport>(salesReport);
        }

        public async Task<ApiResult<SalesReport>> GetSalesReport(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var result = await GetSalesReport(startDate, endDate);
            return new ApiSuccessResult<SalesReport>(result.ResultObject);
        }
    }
}
