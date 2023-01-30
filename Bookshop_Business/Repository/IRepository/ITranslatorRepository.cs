
namespace Bookshop_Business.Repository.IRepository;

public interface ITranslatorRepository
{
    public Task<Tuple<List<TranslatorDTO>, int>> GetAllTranslators(int pageId = 1, string translatorName = "");
    public Task<TranslatorDTO> GetTranslatorById(int translatorId);
    public Task<bool> AddTranslator(TranslatorDTO translatorDTO);
    public Task<bool> IsTranslatorNameUnique(string TranslatorName);
    public Task<bool> DeleteTranslator(int translatorId);

    public Task<bool> UpdateTranslator(int translatorId, TranslatorDTO translatorDTO);
}
