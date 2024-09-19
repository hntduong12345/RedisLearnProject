using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisAndUoW.Domain.Paginate
{
    public static class PaginateExtension
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> queryable, int page, int size, int firstPage)
        {
            if (firstPage > page) throw new ArgumentException($"page ({page}) must greater or equal than firstPage ({firstPage})");

            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - firstPage) * size).Take(size).ToListAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)size);

            return new Paginate<T>
            {
                Page = page,
                Size = size,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };
        }
    }
}
