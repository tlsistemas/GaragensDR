using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Infra.Data.Repositories
{
    public class FormaPagamentoRepository : BaseRepository<FormaPagamentoDTO>, IFormaPagamentoRepository
    {
        private readonly SqlContext _context;

        public FormaPagamentoRepository(SqlContext context) : base(context)
        {
            _context = context;
        }
    }
}