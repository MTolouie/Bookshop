namespace Bookshop_BlazorClient.Services;

public class SliderRepository : Repository<SlidersDTO>, ISliderRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public SliderRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }
}
