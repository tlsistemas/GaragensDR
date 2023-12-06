using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Application.Interfaces
{
    public interface IFormaPagamentoApplication
    {
        Task<BaseResponse<IEnumerable<FormaPagamentoViewModel>>> Listar(FormaPagamentoParams parametros);

        Task<BaseResponse<bool>> CriarComLista(List<FormaPagamentoDTO> formaPagamentoDTO);

        Task<BaseResponse<FormaPagamentoViewModel>> Criar(FormaPagamentoDTO formaPagamentoDto);

        Task<BaseResponse<FormaPagamentoViewModel>> Atualizar(FormaPagamentoDTO formaPagamentoDto);

        Task<BaseResponse<FormaPagamentoViewModel>> Excluir(string formaPagamentoKey);

        Task<BaseResponse<FormaPagamentoViewModel>> Desativar(string formaPagamentoKey);

        Task<BaseResponse<FormaPagamentoViewModel>> Ativar(string formaPagamentoKey);

        Task Dispose();
    }
}

