using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumActions : byte
    {
        [Display(Name = "Nenhum")]
        None = 0,

        [Display(Name = "Cadastro")]
        Insert = 1,

        [Display(Name = "Edição")]
        Update = 2,

        [Display(Name = "Exclusão")]
        Delete = 3,

        [Display(Name = "Procurar")]
        Research = 4,

        [Display(Name = "Exportar")]
        Export = 5,

        [Display(Name = "Importar")]
        Import = 6,
    }
}
