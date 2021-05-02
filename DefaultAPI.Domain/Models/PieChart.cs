using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class PieChart
    {
        public decimal[] ArrDataSets { get; set; }
        public string[] Labels { get; set; }
        public ChartOptions Options { get; set; }
        public bool chartLegend { get; set; }
        public string chartType { get; set; }
        public string[] ArrColors { get; set; }
    }
}
