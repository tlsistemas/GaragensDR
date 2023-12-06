using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Models
{
    public class FormaPagamento : BaseEntity
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
