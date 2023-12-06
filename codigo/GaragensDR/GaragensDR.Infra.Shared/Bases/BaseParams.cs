using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace GaragensDR.Infra.Shared.Bases
{
    public interface BaseParams
    {
        [JsonIgnore]
        [BindNever]
        int? Id { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        String OrderBy { get; set; }
        String Include { get; set; }
    }

    public abstract class BaseParams<TEntity> : BaseParams
    {
        [JsonIgnore]
        [BindNever]
        public int? Id { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public int? Page { get; set; }
        public String OrderBy { get; set; } = string.Empty;
        public String Include { get; set; } = string.Empty;

        public abstract Expression<Func<TEntity, bool>> Filter();

        protected BaseParams()
        {
        }
    }

}
