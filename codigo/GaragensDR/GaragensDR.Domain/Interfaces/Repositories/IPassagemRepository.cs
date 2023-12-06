using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases.Interface;

namespace GaragensDR.Domain.Interfaces.Repositories
{
    public interface IPassagemRepository : IBaseRepository<Passagem>
    {
        Task<int> AdicionarLista(List<Passagem> lista);
    }
}
