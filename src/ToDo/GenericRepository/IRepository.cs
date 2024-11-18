namespace ToDo.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        T Save(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        T? GetById(int id);
    }
}
