using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Interfaces.Services;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Domain.Services
{
    public class FormaPagamentoService : BaseService<FormaPagamento>, IFormaPagamentoService
    {
        public readonly IFormaPagamentoRepository _repository;

        public FormaPagamentoService(IFormaPagamentoRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<int> AdicionarLista(List<FormaPagamento> lista)
        {
            return await _repository.AdicionarLista(lista);
        }
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
