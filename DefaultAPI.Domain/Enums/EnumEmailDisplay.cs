using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Enums
{
    public enum EnumEmailDisplay : byte
    {
        [Display(Name = "Padrão")]
        Padrao = 1,

        [Display(Name = "Boas vindas")]
        BoasVindas = 2,

        [Display(Name = "Esqueci a senha")]
        EsqueciSenha = 3,

        [Display(Name = "Troca de senha")]
        TrocaSenha = 4,

        [Display(Name = "Confirmação de senha")]
        ConfirmacaoSenha = 5,

        [Display(Name = "Relatorio")]
        Relatorio = 6,
    }
}
