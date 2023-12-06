using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Application.Interfaces
{
    public interface IGaragemApplication
    {
        Task<BaseResponse<IEnumerable<GaragemViewModel>>> Listar(GaragemParams parametros);

        Task<BaseResponse<GaragemViewModel>> Criar(GaragemDTO GaragemDto);

        Task<BaseResponse<GaragemViewModel>> Atualizar(GaragemDTO GaragemDto);

        Task<BaseResponse<GaragemViewModel>> Excluir(string GaragemKey);

        Task<BaseResponse<GaragemViewModel>> Desativar(string GaragemKey);

        Task<BaseResponse<GaragemViewModel>> Ativar(string GaragemKey);

        Task Dispose();
    }
}
