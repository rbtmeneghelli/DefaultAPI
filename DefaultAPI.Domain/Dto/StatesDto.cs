using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DefaultAPI.Domain.Dto
{
    public class StatesReturnedDto : BaseDto
    {
        [DisplayName("Sigla")]
        public string Initials { get; set; }

        [DisplayName("Estado")]
        public string Name { get; set; }

        public override string ToString() => $"Estado: {Name}";
    }

    public class StatesSendDto : BaseDto
    { 
        [DisplayName("Sigla")]
        public string Initials { get; set; }

        [DisplayName("Estado")]
        public string Name { get; set; }

        public override string ToString() => $"Estado: {Name}";
    }
}
