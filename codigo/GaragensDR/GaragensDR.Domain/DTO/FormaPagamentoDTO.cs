using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.DTO
{
    public class FormaPagamentoDTO : BaseDTO
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
