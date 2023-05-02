﻿using Application.Repositories;

namespace Application
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes to the database. This is called when the user changes the data or saves a new version of the data.
        /// </summary>
        /// <returns>The number of changes saved or - 1 if there was an error saving the changes ( in which case the error code will be set accordingly )</returns>
        int SaveChanges();
        /// <summary>
        /// Saves changes to the data source. This is a no - op if there are no changes to save
        /// </summary>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// Starts a transaction. This is a no - op if there is already a transaction in progress. The transaction must be committed or rolled back
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commits changes to the database. This is called after all changes have been committed to the database and can be used to make sure that the database is up to date
        /// </summary>
        void Commit();
        /// <summary>
        /// Commits the changes that have been made to the file. This is called after the file has been opened
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// Rolls back the transaction. This is called when an error occurs during a transaction that is in progress
        /// </summary>
        void Rollback();
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public Task ExecuteTransactionAsync(Action action);
    }
}
