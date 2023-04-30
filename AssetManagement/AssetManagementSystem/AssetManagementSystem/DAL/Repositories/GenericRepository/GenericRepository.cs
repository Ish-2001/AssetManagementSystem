using AssetManagementSystem.Data.Domain;
using AssetManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AssetManagementSystem.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;
        private readonly DBContext _dbContext;

        public GenericRepository(DBContext _context)
        {
            _dbContext = _context;
            _entities = _dbContext.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _entities.IgnoreAutoIncludes().ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).FirstOrDefault();
        }
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}
