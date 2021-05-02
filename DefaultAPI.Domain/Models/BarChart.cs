using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class BarChart
    {
        public string[] Labels { get; set; }
        public string chartType { get; set; }
        public bool chartLegend { get; set; }
        public List<ChartDataSets> ArrDataSets { get; set; }
    };
}
