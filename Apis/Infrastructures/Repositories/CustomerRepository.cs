using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context,
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
