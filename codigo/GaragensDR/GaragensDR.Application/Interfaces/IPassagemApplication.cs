using GaragensDR.Application.Parameters;
using GaragensDR.Application.ViewModel;
using GaragensDR.Domain.DTO;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Application.Interfaces
{
    public interface IPassagemApplication
    {
        Task<BaseResponse<IEnumerable<PassagemViewModel>>> Listar(PassagemParams parametros);

        Task<BaseResponse<bool>> CriarComLista(List<PassagemDTO> passagemDto);

        Task<BaseResponse<PassagemViewModel>> Criar(PassagemDTO PassagemDto);

        Task<BaseResponse<PassagemViewModel>> Atualizar(PassagemDTO PassagemDto);

        Task<BaseResponse<PassagemViewModel>> Excluir(string PassagemKey);

        Task<BaseResponse<PassagemViewModel>> Desativar(string PassagemKey);

        Task<BaseResponse<PassagemViewModel>> Ativar(string PassagemKey);

        Task Dispose();
    }
}
