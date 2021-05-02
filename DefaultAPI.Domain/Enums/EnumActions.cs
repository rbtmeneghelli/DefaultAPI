using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumActions : byte
    {
        [Display(Name = "Cadastro")]
        Insert = 0,

        [Display(Name = "Edição")]
        Update = 1,

        [Display(Name = "Exclusão")]
        Delete = 2,
    }
}
