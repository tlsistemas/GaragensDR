using GaragensDR.Domain.Models;
using GaragensDR.Infra.Shared.Bases;
using GaragensDR.Infra.Shared.Delegates;
using System.Linq.Expressions;

namespace GaragensDR.Application.Parameters
{
    public class GaragemParams : BaseParams<GaragemDTO>
    {
        public string Key { get; set; } = "";
        public string Codigo { get; set; } = "";
        public string Nome { get; set; } = "";
        public bool? Ativo { get; set; } = null;

        public override Expression<Func<GaragemDTO, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<GaragemDTO>();

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var obj = new GaragemDTO { Key = Key };
                predicate = predicate.And(p => p.Id.Equals(obj.Id));
            }

            if (Id > 0)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(Codigo))
            {
                predicate = predicate.And(p => p.Codigo.Contains(Codigo));
            }

            if (!string.IsNullOrWhiteSpace(Nome))
            {
                predicate = predicate.And(p => p.Nome.Contains(Nome));
            }

            if (Ativo.HasValue)
            {
                predicate = predicate.And(p => p.Ativo == Ativo);
            }

            return predicate;
        }
    }
}
