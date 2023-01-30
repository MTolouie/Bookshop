using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop_Business.Repository.IRepository;

public interface ICategoryRepository
{
    public Task<Tuple<List<CategoryDTO>,int>> GetAllCategories(int pageId = 1,string catTitle="");
    public Task<List<CategoryDTO>> GetAllCategoriesForCreateOrEdit();
    public Task<CategoryDTO> GetCategoryById(int CatId);
    public Task<string> GetCategoryTitleById(int CatId);
    public List<string> GetBookCategoriesByBookId(int BookId);
    public Task<bool> AddCategory(CategoryDTO catDTO);
    public Task<bool> IsCatTitleUnique(string catTitle);
    public Task<bool> DeleteCategory(int CatId);

    public Task<bool> UpdateCategory(int CatId,CategoryDTO categoryDTO);
}
