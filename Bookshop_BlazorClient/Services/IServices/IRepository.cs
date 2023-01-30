namespace Bookshop_BlazorClient.Services.IServices;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(string url, int Id/*, string token*/);
    Task<IEnumerable<T>> GetAllAsync(string url/*, string token*/);
    Task<bool> CreateAsync(string url, T objToCreate, string token);
    Task<bool> UpdateAsync(string url,string token, T objToUpdate);
    Task<bool> DeleteAsync(string url, int Id, string token);
}
