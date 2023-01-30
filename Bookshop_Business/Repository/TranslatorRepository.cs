

namespace Bookshop_Business.Repository;

public class TranslatorRepository : ITranslatorRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public TranslatorRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddTranslator(TranslatorDTO translatorDTO)
    {
        try
        {

            var translator = _mapper.Map<TranslatorDTO, Translator>(translatorDTO);
            translator.TranslatorName.Trim();
            _db.Translators.Add(translator);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteTranslator(int translatorId)
    {
        try
        {
            var translator = await _db.Translators.FindAsync(translatorId);

            if (translator is null)
                return false;

            _db.Translators.Remove(translator);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Tuple<List<TranslatorDTO>, int>> GetAllTranslators(int pageId = 1, string translatorName = "")
    {
        IQueryable<Translator> translators = _db.Translators;

        if (!string.IsNullOrEmpty(translatorName))
        {
            translators = translators.Where(a => a.TranslatorName.Contains(translatorName.ToLower().Trim()));
        }


        var translatorsDTO = _mapper.Map<IQueryable<Translator>, List<TranslatorDTO>>(translators);


        int take = 5;
        int skip = (pageId - 1) * take;
        var pageCount = translatorsDTO.Count() / take;
        if (pageCount % 2 != 0)
        {
            pageCount++;
        }
        var query = translatorsDTO.OrderByDescending(a => a.TranslatorId).Skip(skip).Take(take).ToList();
        return Tuple.Create(query, pageCount);
    }

    public async Task<TranslatorDTO> GetTranslatorById(int translatorId)
    {
        try
        {
            var translator = await _db.Translators.FindAsync(translatorId);

            if (translator is null)
                return null;

            var traslatorDTO = _mapper.Map<Translator, TranslatorDTO>(translator);
            return traslatorDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> IsTranslatorNameUnique(string TranslatorName)
    {
        var title = await _db.Translators.FirstOrDefaultAsync(t => t.TranslatorName.ToLower() == TranslatorName.ToLower());
        if (title is not null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> UpdateTranslator(int translatorId, TranslatorDTO translatorDTO)
    {
        try
        {
            if (translatorDTO.TranslatorId != translatorId)
                return false;

            var translatorFromDb = await _db.Translators.FindAsync(translatorId);
            translatorFromDb.TranslatorName = translatorDTO.TranslatorName;

            _db.Translators.Update(translatorFromDb);
            await _db.SaveChangesAsync();

            return true;


        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
