using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class PushNotification
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
    }
}
