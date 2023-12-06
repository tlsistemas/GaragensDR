using GaragensDR.Domain.Interfaces.Repositories;
using GaragensDR.Domain.Models;
using GaragensDR.Infra.Data.Contexts;
using GaragensDR.Infra.Shared.Bases;

namespace GaragensDR.Infra.Data.Repositories
{
    public class PassagemRepository : BaseRepository<PassagemDTO>, IPassagemRepository
    {
        private readonly SqlContext _context;

        public PassagemRepository(SqlContext context) : base(context)
        {
            _context = context;
        }
    }
}