using Application;
using Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        private IDbContextTransaction _transaction;

        private bool _disposed;

        public UnitOfWork(ApplicationDbContext dbContext,
                          IUserRepository userRepository,
                          ICustomerRepository customerRepository,
                          IProductRepository productRepository,
                          IOrderRepository orderRepository,
                          IOrderDetailRepository orderDetailRepository)

        {
            _context = dbContext;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;

        }

        public IUserRepository UserRepository => _userRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ICustomerRepository CustomerRepository => _customerRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository;

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
        public async Task ExecuteTransactionAsync(Action action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                action();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
