using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context,
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
