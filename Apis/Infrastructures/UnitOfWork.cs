using Application;
using Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
     
        private IDbContextTransaction _transaction;

        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext,
                          IUserRepository userRepository,
                            IBookRepository bookRepository)

        {
            _context = dbContext;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
          
        }

        public IUserRepository UserRepository => _userRepository;
        public IBookRepository BookRepository => _bookRepository;
        



        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
