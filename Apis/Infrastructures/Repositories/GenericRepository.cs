using Application.Interfaces;
using Application.Repositories;
using Application.Commons;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Enums;

namespace Infrastructures.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public GenericRepository(ApplicationDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _dbSet = context.Set<TEntity>();
            _timeService = timeService;
            _claimsService = claimsService;
        }
        // create
        public async Task AddAsync(TEntity entity)
        {
            entity.CreationDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.CurrentUserId;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.CurrentUserId;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        // Read
        public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.OrderByDescending(e => e.CreationDate)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.AnyAsync(filter);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                return await _dbSet.CountAsync();
            return await _dbSet.CountAsync(filter);
        }
        public async Task<int> CountAsync() => await _dbSet.CountAsync();
        public async Task<TEntity> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<Pagination<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
                                                        int pageIndex = 0,
                                                        int pageSize = 10)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var itemCount = await query.CountAsync();

            var items = await query.OrderByDescending(e => e.CreationDate)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<Pagination<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
                                                        int pageIndex = 0,
                                                        int pageSize = 10,
                                                        Expression<Func<TEntity, object>> sortColumn = null,
                                                        SortDirection sortDirection = SortDirection.Descending)
        {
            var query = _dbSet.AsQueryable();

            // Include related entities if specified
            if (include != null)
            {
                query = include(query);
            }

            // Apply filtering if specified
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply sorting if specified
            if (sortColumn != null)
            {
                query = sortDirection == SortDirection.Ascending
                    ? query.OrderBy(sortColumn)
                    : query.OrderByDescending(sortColumn);
            }

            // Calculate the total item count for the query
            var itemCount = await query.CountAsync();

            // Apply pagination to the query
            var items = await query.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            // Create the pagination result
            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.Where(filter).ToListAsync();

        public async Task<Pagination<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.Where(filter)
                                    .OrderByDescending(e => e.CreationDate)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }
        // Update
        public void Update(TEntity entity)
        {
            entity.ModificationDate = _timeService.GetCurrentTime();
            entity.ModificationBy = _claimsService.CurrentUserId;
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimsService.CurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }
        // delete
        public void Delete(TEntity entity)
        {
            entity.DeletionDate = _timeService.GetCurrentTime();
            entity.DeleteBy = _claimsService.CurrentUserId;
            _dbSet.Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimsService.CurrentUserId;
            }
            _dbSet.RemoveRange(entities);
        }
        public async Task Delete(object id)
        {
            TEntity entity = await GetByIdAsync(id);
            entity.DeletionDate = _timeService.GetCurrentTime();
            entity.DeleteBy = _claimsService.CurrentUserId;
            Delete(entity);
        }
        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteBy = _claimsService.CurrentUserId;
            _dbSet.Update(entity);
        }

        public void SoftRemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimsService.CurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
            => await _dbSet.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(filter);

        public async Task<TEntity> FirstOrdDefaultAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(filter);
        }


    }
}
