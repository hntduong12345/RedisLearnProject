﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using RedisAndUoW.Domain.Paginate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedisAndUoW.Repository.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        #region Get Functions
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector = null,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<ICollection<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector = null,
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<IPaginate<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null,
               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
               int page = 1, int size = 10);

        Task<IPaginate<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector = null,
           Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           int page = 1, int size = 10);
        #endregion

        #region Insert Functions
        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Update Functions
        void UpdateAsync(T entity);

        void UpdateRangeAsync(IEnumerable<T> entities);
        #endregion

        #region Delete Functions
        void DeleteAsync(T entity);

        void DeleteRangeAsync(IEnumerable<T> entities);
        #endregion
    }
}
