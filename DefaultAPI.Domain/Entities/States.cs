using DefaultAPI.Domain.Base;
using System.Collections.Generic;

namespace DefaultAPI.Domain.Entities
{
    public class States : BaseEntity
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }

        public Region Regiao { get; set; }
        public long IdRegiao { get; set; }
        public List<Ceps> Ceps { get; set; }
    }
}
