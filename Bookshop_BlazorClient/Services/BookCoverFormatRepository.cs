namespace Bookshop_BlazorClient.Services;

public class BookCoverFormatRepository : Repository<BookCoverFormatDTO>, IBookCoverFormatRepository
{
    private readonly IHttpClientFactory _clientfactory;
    public BookCoverFormatRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientfactory = clientFactory;
    }
}
