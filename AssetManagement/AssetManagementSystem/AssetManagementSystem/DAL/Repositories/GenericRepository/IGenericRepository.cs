using System.Linq.Expressions;

namespace AssetManagementSystem.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll();
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
    }
}
