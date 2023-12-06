using System.Data.Common;
using System.Linq.Expressions;

namespace GaragensDR.Infra.Shared.Bases.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Dispose();
        void DisposeAsync();
        TEntity GetById(Int32 id);
        Task<TEntity> GetByIdAsync(Int32 id);
        IEnumerable<TEntity> GetAll();
        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        bool Remove(int id);
        Task<bool> Remove(TEntity entity);
        Task<int> RemoveAllAsync();
        Task<bool> RemoveAsync(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, String orderBy = null, String includes = null);
        Task<IEnumerable<TEntity>> GetByParamsAsync(
            Expression<Func<TEntity, bool>> filter = null,
            String orderBy = null,
            String includeProps = null,
            bool asNoTracking = true);

        Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map);

        Task<int> RawSqlQueryAsync(string query);
    }

}
