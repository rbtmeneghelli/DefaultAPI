using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumJobTypeExecution : byte
    {
        [Display(Name = "Nunca")]
        Never = 0,

        [Display(Name = "Diário")]
        Daily = 1,

        [Display(Name = "Semanal")]
        Weekly = 2,

        [Display(Name = "Mensal")]
        Monthly = 3,

        [Display(Name = "Trimestral")]
        Quarterly = 4,

        [Display(Name = "Anual")]
        Yearly = 5
    }
}
