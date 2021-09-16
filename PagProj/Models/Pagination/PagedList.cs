using System.Linq;
using System;
using System.Collections.Generic;

namespace PagProj.Models.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            this.TotalCount = source.Count();
            this.PageSize = pageSize;
            this.CurrentPage = pageNumber;
            this.TotalPages = (int)Math.Ceiling(TotalCount / (double)this.PageSize);

            var items = source.Skip((this.CurrentPage - 1) * this.PageSize)
                                .Take(this.PageSize)
                                .ToList();

            this.AddRange(items);
        }
    }
}