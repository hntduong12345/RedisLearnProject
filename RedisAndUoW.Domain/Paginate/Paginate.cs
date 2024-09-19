﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisAndUoW.Domain.Paginate
{
    public class Paginate<TResult> : IPaginate<TResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IList<TResult> Items { get; set; }

        public Paginate(IEnumerable<TResult> source, int page, int size, int firstPage)
        {
            var enumerable = source as TResult[] ?? source.ToArray();
            if (firstPage > page) throw new ArgumentException($"Page ({page}) must be greater or equal than firstPage ({firstPage})");

            if(source is IQueryable<TResult> queryable)
            {
                Page = page;
                Size = size;
                TotalItems = queryable.Count();
                Items = queryable.Skip((page - firstPage) * size).Take(size).ToList();
                TotalPages = (int)Math.Ceiling(TotalItems/ (double)Size);
            }
            else
            {
                Page = page;
                Size = size;
                TotalItems = enumerable.Count();
                Items = enumerable.Skip((page - firstPage) * Size).Take(size).ToList();
                TotalPages = (int)Math.Ceiling(TotalItems / (double)Size);
            }
        }

        public Paginate()
        {
            Items = Array.Empty<TResult>();
        }
    }
}
