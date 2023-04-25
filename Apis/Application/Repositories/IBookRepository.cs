using Domain.Entities;

namespace Application.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> AsQueryable();
    }
}
