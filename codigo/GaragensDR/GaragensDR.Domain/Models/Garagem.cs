using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Models
{
    public class Garagem : BaseEntity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Preco1aHora { get; set; }
        public string PrecoHorasExtra { get; set; }
        public string PrecoMensalista { get; set; }
    }
}
