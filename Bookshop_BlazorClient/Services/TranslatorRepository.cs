namespace Bookshop_BlazorClient.Services;

public class TranslatorRepository : Repository<TranslatorDTO>,ITranslatorRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public TranslatorRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }
}
