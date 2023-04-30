using Domain.Entities;

namespace Application.Repositories
{
    public interface IBookRepository : IGenericRepository<Product>
    {
        IQueryable<Product> AsQueryable();
    }
}
