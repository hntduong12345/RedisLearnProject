﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RedisAndUoW.Domain.Paginate;
using RedisAndUoW.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedisAndUoW.Repository.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Dispose()
        {
           _dbContext?.Dispose();
        }

        #region Get Functions
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if(include != null) query = include(query);
            if(predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().FirstOrDefaultAsync();

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector = null,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).FirstOrDefaultAsync();

            return await query.AsNoTracking().Select(selector).FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<ICollection<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector = null,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).ToListAsync();

            return await query.AsNoTracking().Select(selector).ToListAsync();
        }

        public virtual async Task<IPaginate<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int page = 1, int size = 10)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().ToPaginateAsync(page, size, 1);

            return await query.AsNoTracking().ToPaginateAsync(page, size, 1);
        }

        public virtual async Task<IPaginate<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector = null,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int page = 1, int size = 10)
        {
            IQueryable<T> query = _dbSet;

            if (include != null) query = include(query);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).ToPaginateAsync(page, size, 1);

            return await query.AsNoTracking().Select(selector).ToPaginateAsync(page, size, 1);
        }
        #endregion

        #region Insert Functions
        public async Task InsertAsync(T entity)
        {
            if(entity == null) return;
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities == null) return;
            await _dbSet.AddRangeAsync(entities);
        }
        #endregion

        #region Update Functions
        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        #endregion

        #region Delete Functions
        public void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        #endregion
    }
}
