namespace Microgate.Extranet.Services;

public interface IBaseService<T, in TKey> 
{
    Task<T> GetAsync(TKey id);
    Task<List<T>> ListAsync();
    Task<List<T>> ListCurrentAsync(TKey id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAsync(TKey id);
}

