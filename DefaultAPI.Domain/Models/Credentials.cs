using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class Credentials
    {
        public long? Id { get; set; }
        public string Login { get; set; }
        public string Perfil { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
