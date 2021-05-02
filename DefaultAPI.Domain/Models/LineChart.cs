using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class LineChart
    {
        public List<ChartDataSets> ArrDataSets { get; set; }
        public string[] Labels { get; set; }
        public ChartOptions Options { get; set; }
        public bool chartLegend { get; set; }
        public string chartType { get; set; }
        public List<ChartColors> ArrColors { get; set; }
    }
}
