using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public abstract class BaseDto
    {
        public long? Id { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }
    }
}
