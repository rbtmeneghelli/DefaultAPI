using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Notification
    {
        public long? Id { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool IsActive { get; set; }

        public Notification()
        {

        }

        public Notification(long? id, string description, DateTime createdTime, bool isActive)
        {
            this.Id = id;
            this.Description = description;
            this.CreatedTime = createdTime;
            this.IsActive = isActive;
        }

        public Notification(string description, DateTime createdTime, bool isActive)
        {
            this.Description = description;
            this.CreatedTime = createdTime;
            this.IsActive = isActive;
        }
    }
}