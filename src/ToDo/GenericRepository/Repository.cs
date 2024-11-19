using Microsoft.EntityFrameworkCore;

namespace ToDo.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _ctx;
        protected DbSet<T> _dbSet;

        public Repository(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public virtual T Save(T entity)
        {
            if (_ctx.Entry(entity).State == EntityState.Detached)
                _dbSet.Add(entity);
            else
                _dbSet.Update(entity);

            _ctx.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _ctx.SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
