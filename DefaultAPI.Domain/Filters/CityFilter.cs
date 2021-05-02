using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Filters
{
    public class CityFilter : BaseFilter
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public long? IBGE { get; set; }

        public long? IdState { get; set; }
    }
}
