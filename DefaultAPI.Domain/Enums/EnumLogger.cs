using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumLogger : byte
    {
        [Display(Name = "Information")]
        LogInformation = 0,

        [Display(Name = "Trace")]
        LogTrace = 1,

        [Display(Name = "Debug")]
        LogDebug = 2,

        [Display(Name = "Warning")]
        LogWarning = 3,

        [Display(Name = "Error")]
        LogError = 4,

        [Display(Name = "Critical")]
        LogCritical = 5,
    }
}
