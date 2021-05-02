using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class ChartDataSets
    {
        public decimal[] data { get; set; }
        public string label { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public string[] pointBackgroundColor { get; set; }
    }
}
