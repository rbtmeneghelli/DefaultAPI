using DefaultAPI.Domain.Base;

namespace DefaultAPI.Domain.Filters
{
    public class UserFilter : BaseFilter
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastPassword { get; set; }
        public bool? IsAuthenticated { get; set; }
        public long? IdProfile { get; set; }
        public bool? IsActive { get; set; }
    }
}

