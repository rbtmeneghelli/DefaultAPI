using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumArquivo : byte
    {
        [Display(Name = "Word")]
        Word = 0,

        [Display(Name = "Excel")]
        Excel = 1,

        [Display(Name = "Pdf")]
        Pdf = 2,

        [Display(Name = "Txt")]
        Txt = 3,

        [Display(Name = "Jpg")]
        Jpg = 4,

        [Display(Name = "Jpeg")]
        Jpeg = 5,

        [Display(Name = "Png")]
        Png = 6
    }
}
