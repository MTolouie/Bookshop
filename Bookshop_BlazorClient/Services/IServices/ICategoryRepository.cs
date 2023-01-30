namespace Bookshop_BlazorClient.Services.IServices;

public interface ICategoryRepository : IRepository<CategoryDTO>
{
    public Task<string> GetCategoryTitleById(string url,string action,int categoryId);
}
