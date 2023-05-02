using Application;
using Application.Interfaces;
using AutoMapper;

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
        // public async Task<SalesReport> GetSalesReport(DateTime startDate, DateTime endDate)
        // {
        //     var orders = await _unitOfWork.OrderRepository.GetAsync(
        //         filter: o => o.OrderDate >= startDate && o.OrderDate <= endDate,
        //         include: o => o.Include(o => o.OrderDetails),
        //         pageIndex: 0,
        //         pageSize: int.MaxValue);

        //     var salesByDay = orders.GroupBy(
        //             o => new { Year = o.OrderDate.Year, Month = o.OrderDate.Month, Day = o.OrderDate.Day })
        //         .Select(g => new { Date = g.Key, TotalSales = g.Sum(o => o.OrderDetails.Sum(od => od.UnitPrice * od.Quantity)) })
        //         .OrderBy(g => g.Date.Year)
        //         .ThenBy(g => g.Date.Month)
        //         .ThenBy(g => g.Date.Day)
        //         .ToList();

        //     var totalSales = salesByDay.Sum(s => s.TotalSales);

        //     var salesReport = new SalesReport
        //     {
        //         StartDate = startDate,
        //         EndDate = endDate,
        //         TotalSales = totalSales,
        //         SalesByDay = salesByDay.Select(s => new DailySalesReport
        //         {
        //             Date = new DateTime(s.Date.Year, s.Date.Month, s.Date.Day),
        //             TotalSales = s.TotalSales
        //         }).ToList()
        //     };

        //     return salesReport;
        // }

        // public async Task<SalesReport> GetSalesReport(int year, int month)
        // {
        //     var startDate = new DateTime(year, month, 1);
        //     var endDate = startDate.AddMonths(1).AddDays(-1);

        //     return await GetSalesReport(startDate, endDate);
        // }

    }
}
