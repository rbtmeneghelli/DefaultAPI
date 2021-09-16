using DefaultAPI.Domain.Filters;
using DefaultAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefaultAPI.Application.Factory
{
    public static class PagedFactory
    {
        public static PagedResult<T> GetPaged<T> (this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.Page = ++page;
            result.PageSize = pageSize;
            result.TotalRecords = query.Count();
            result.PageCount = (int)Math.Ceiling((double)result.TotalRecords / pageSize);
            result.NextPage = (pageSize * result.Page) >= result.TotalRecords ? null : (int?)result.Page + 1;
            result.Results = query.Any() ? query.Skip((result.Page - 1) * pageSize).Take(pageSize).ToList() : new List<T>();
            return result;
        }
    }
}