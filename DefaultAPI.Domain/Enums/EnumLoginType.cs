using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumLoginType : byte
    {
        [Display(Name = "Modo Corporativo")]
        Coorporate = 0,

        [Display(Name = "Modo Cloud XRProj")]
        Cloud = 1,
    }
}
