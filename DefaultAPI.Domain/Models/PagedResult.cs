using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class PagedResult<T> : BasePaged where T : class
    {
        public List<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
