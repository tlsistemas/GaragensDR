using GaragensDR.Infra.Shared.Bases.Interface;
using System.Data.Common;
using System.Linq.Expressions;

namespace GaragensDR.Infra.Shared.Bases
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repositoryBase;

        public BaseService(IBaseRepository<TEntity> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        public virtual bool Add(TEntity entity)
        {
            return repositoryBase.Add(entity);
        }

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            return await repositoryBase.AddAsync(entity);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> conditions = null, string orderBy = null, string includes = null)
        {
            return repositoryBase.Get(conditions, orderBy, includes);
        }
        public async Task<IEnumerable<TEntity>> GetByParamsAsync(
    Expression<Func<TEntity, bool>> filter = null,
    string orderBy = null,
    string includeProps = null,
    bool asNoTracking = true)
        {
            return await repositoryBase.GetByParamsAsync(filter, orderBy, includeProps, asNoTracking);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repositoryBase.GetAll();
        }
        public virtual TEntity GetById(Int32 id)
        {
            return repositoryBase.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Int32 id)
        {
            return await repositoryBase.GetByIdAsync(id);
        }

        public async Task<bool> Remove(TEntity entity)
        {
            return await repositoryBase.Remove(entity);
        }

        public virtual bool Remove(Int32 id)
        {
            return repositoryBase.Remove(id);
        }

        public async Task<int> RemoveAllAsync()
        {
            return await repositoryBase.RemoveAllAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            await repositoryBase.RemoveAsync(entity);
        }

        public virtual bool Update(TEntity entity)
        {
            return repositoryBase.Update(entity);
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await repositoryBase.UpdateAsync(entity);
        }

        public virtual Task<List<T>> RawSqlQueryAsync<T>(string query, Func<DbDataReader, T> map)
        {
            return repositoryBase.RawSqlQueryAsync<T>(query, map);
        }

        public virtual async Task<int> RawSqlQueryAsync(string query)
        {
            return await repositoryBase.RawSqlQueryAsync(query);
        }

        public virtual void Dispose()
        {
            repositoryBase.Dispose();
        }

        public virtual void DisposeAsync()
        {
            repositoryBase.DisposeAsync();
        }
    }

}
