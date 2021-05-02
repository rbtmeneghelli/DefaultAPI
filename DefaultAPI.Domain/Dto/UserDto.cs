using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Dto
{
    public class UserReturnedDto : BaseDto
    {
        [DisplayName("Login")]
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastPassword { get; set; }
        [DisplayName("Autenticado")]
        public bool IsAuthenticated { get; set; }
        public long IdProfile { get; set; }

        [DisplayName("Perfil")]
        public string Profile { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        public override string ToString() => $"Login: {Login}";
    }

    public class UserSendDto : BaseDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastPassword { get; set; }
        public bool IsAuthenticated { get; set; }
        public long IdProfile { get; set; }

        public override string ToString() => $"Login: {Login}";
    }
}
