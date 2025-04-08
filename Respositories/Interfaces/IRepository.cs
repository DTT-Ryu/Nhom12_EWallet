namespace Nhom12_EWallet.Respositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        //Task<IQueryable<T>> GetAll();
        Task<T?> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
