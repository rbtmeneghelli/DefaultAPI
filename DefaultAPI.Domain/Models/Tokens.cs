using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public sealed class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
