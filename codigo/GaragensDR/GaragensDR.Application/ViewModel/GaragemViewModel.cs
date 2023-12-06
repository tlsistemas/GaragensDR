using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Application.ViewModel
{
    public class GaragemViewModel : BaseEntity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Preco1aHora { get; set; }
        public string PrecoHorasExtra { get; set; }
        public string PrecoMensalista { get; set; }
    }
}
