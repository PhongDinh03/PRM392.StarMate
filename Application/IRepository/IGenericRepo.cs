namespace Application.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Remove(T entity);
        public Task<int> SaveChangeAsync();
    }
}
