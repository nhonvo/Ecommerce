using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(context,
                  timeService,
                  claimsService)
        {
            _context = context;
        }

        public async Task<bool> CheckExistUser(string email) => await _context.Users.AnyAsync(x => x.Email == email);

        public async Task<User> Find(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new Exception("Incorrect Email!!!");
            }
            return user;
        }
    }
}
