
namespace Bookshop_Business.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CategoryRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddCategory(CategoryDTO catDTO)
    {
        try
        {

            var category = _mapper.Map<CategoryDTO, Category>(catDTO);
            category.CatTitle.Trim();
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteCategory(int CatId)
    {
        try
        {
            var category = await _db.Categories.FindAsync(CatId);

            if (category is null)
                return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Tuple<List<CategoryDTO>, int>> GetAllCategories(int pageId = 1,string catTitle = "")
    {
        IQueryable<Category> categories = _db.Categories;

        if (!string.IsNullOrEmpty(catTitle))
        {
            categories = categories.Where(c => c.CatTitle.Contains(catTitle.ToLower().Trim()));
        }


        var categoriesDTO = _mapper.Map<IQueryable<Category>, List<CategoryDTO>>(categories);


        int take = 5;
        int skip = (pageId - 1) * take;
        var pageCount = categoriesDTO.Count() / take;
        if (pageCount % 2 != 0)
        {
            pageCount++;
        }
        var query = categoriesDTO.OrderByDescending(c => c.catId).Skip(skip).Take(take).ToList();
        return Tuple.Create(query,pageCount);
    }

    public async Task<List<CategoryDTO>> GetAllCategoriesForCreateOrEdit()
    {
        try
        {
            var categories =  _db.Categories.ToList();
            var categoriesDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return categoriesDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public  List<string> GetBookCategoriesByBookId(int BookId)
    {
        try
        {
            var bookCategoriyIds =  _db.BookCategories.Where(b=>b.BookId == BookId).Select(b=>b.CatId).ToList();
            if (bookCategoriyIds.Count == 0)
                return null;

            var categories =  _db.Categories.Where(c => bookCategoriyIds.Contains(c.catId)).Select(c=>c.CatTitle).ToList();
            if(categories.Count == 0)
                return  null;

            return categories;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<CategoryDTO> GetCategoryById(int CatId)
    {
        try
        {
            var category = await _db.Categories.FindAsync(CatId);

            if (category is null)
                return null;

            var CategoryDTO = _mapper.Map<Category, CategoryDTO>(category);
            return CategoryDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> GetCategoryTitleById(int CatId)
    {
        try
        {
            var category = await _db.Categories.FindAsync(CatId);
            if (category is null)
                return null;

            return category.CatTitle;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> IsCatTitleUnique(string catTitle)
    {
        var title = await _db.Categories.FirstOrDefaultAsync(c => c.CatTitle.ToLower() == catTitle.ToLower());
        if (title is not null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> UpdateCategory(int CatId, CategoryDTO categoryDTO)
    {
        try
        {
            if (categoryDTO.catId != CatId)
                return false;

            var categoryFromDb = await _db.Categories.FindAsync(CatId);
            categoryFromDb.CatTitle = categoryDTO.CatTitle;

            _db.Categories.Update(categoryFromDb);
            await _db.SaveChangesAsync();

            return true;


        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
