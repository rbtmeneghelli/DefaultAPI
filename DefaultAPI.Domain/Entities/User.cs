using DefaultAPI.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefaultAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastPassword { get; set; }
        public bool IsAuthenticated { get; set; }
        public long IdProfile { get; set; }
        public Profile Profile { get; set; }

        [NotMapped]
        public string NewPassword { get; set; }

        public override string ToString() => $"Login: {Login}";
    }
}
