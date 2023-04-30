using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context,
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
