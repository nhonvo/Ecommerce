using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(context,
                  timeService,
                  claimsService)
        {
            _context = context;
        }
    }
}
