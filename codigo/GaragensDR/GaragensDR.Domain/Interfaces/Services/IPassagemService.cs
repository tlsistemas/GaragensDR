using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases.Interface;

namespace GaragensDR.Domain.Interfaces.Services
{
    public interface IPassagemService : IBaseService<Passagem>
    {
        Task<int> AdicionarLista(List<Passagem> lista);
    }
}
