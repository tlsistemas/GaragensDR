using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases.Interface;

namespace GaragensDR.Domain.Interfaces.Repositories
{
    public interface IGaragemRepository : IBaseRepository<Garagem>
    {
        Task<int> AdicionarLista(List<Garagem> lista);
    }
}
