using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;

namespace Application.Interfaces
{
    public interface IOrderDetailService
    {
        Task<ApiResult<SalesReport>> GetSalesReport(DateTime startDate, DateTime endDate);
        Task<ApiResult<SalesReport>> GetSalesReport(int year, int month);

    }
}
