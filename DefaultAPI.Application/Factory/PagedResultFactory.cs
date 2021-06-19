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
        public static PagedResult<T> GetPaged<T> (this IQueryable<T> query, int page, int total) where T : class
        {
            var result = new PagedResult<T>();
            result.Page = ++page;
            result.Total = total;
            result.TotalRecords = query.Count();
            result.Total = (int)Math.Ceiling((decimal)result.TotalRecords / result.Total);
            result.NextPage = (result.Total * result.Page) >= result.TotalRecords ? null : (int?)result.Page + 1;
            result.Results = query.Any() ? query.Skip((result.Page - 1) * result.Total).Take(result.Total).ToList() : new List<T>();
            return result;
        }
    }
}
