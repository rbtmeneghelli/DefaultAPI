using DefaultAPI.Domain.Base;
using System.ComponentModel;

namespace DefaultAPI.Domain.Dto
{
    public class CityReturnedDto : BaseDto
    {
        [DisplayName("Código IBGE")]
        public long? IBGE { get; set; }

        [DisplayName("Municipio")]
        public string Name { get; set; }

        public long? IdState { get; set; }

        [DisplayName("Estado")]
        public string StateDesc { get; set; }

        [DisplayName("Ativo")]
        public string IsActiveDesc { get; set; }

        public override string ToString() => $"Municipio: {Name}";
    }

    public class CitySendDto : BaseDto
    {
        public long? IBGE { get; set; }

        public string Name { get; set; }

        public long? IdState { get; set; }

        public string StateDesc { get; set; }

        public override string ToString() => $"Municipio: {Name}";
    }
}
