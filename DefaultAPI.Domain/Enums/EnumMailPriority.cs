using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumMailPriority : byte
    {
        [Display(Name = "Normal")]
        Normal = 1,

        [Display(Name = "Baixa")]
        Low = 2,

        [Display(Name = "Alta")]
        High = 3
    }
}
