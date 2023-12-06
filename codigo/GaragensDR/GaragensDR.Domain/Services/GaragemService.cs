using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Services
{
    public class GaragemService : BaseService<Garagem>, IGaragemService
    {
        public readonly IGaragemRepository _repository;

        public GaragemService(IGaragemRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> AdicionarLista(List<Garagem> lista)
        {
            return await _repository.AdicionarLista(lista);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
