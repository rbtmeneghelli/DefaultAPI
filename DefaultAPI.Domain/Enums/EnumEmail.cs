using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumEmail : byte
    {
        [Display(Name = "Gmail")]
        Gmail = 1,

        [Display(Name = "Outlook")]
        Outlook = 2,

        [Display(Name = "Hotmail")]
        Hotmail = 3
    }
}
