namespace MyRESTServices.Data.Interfaces
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(int id, T entity);
        Task<int> Delete(int id);
    }
}
