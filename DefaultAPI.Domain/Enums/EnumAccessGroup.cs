using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumAccessGroup : byte
    {
        [Display(Name = "Tria")]
        Tria = 0,

        [Display(Name = "XR")]
        XR = 1,
    }
}
