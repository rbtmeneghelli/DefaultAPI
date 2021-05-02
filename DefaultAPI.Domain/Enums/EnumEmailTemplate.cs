
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumEmailTemplate : byte
    {
        [Display(Name = "DefaultAPI")]
        DefaultAPI = 1
    }
}
