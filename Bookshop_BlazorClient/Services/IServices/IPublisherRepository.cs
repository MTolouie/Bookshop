namespace Bookshop_BlazorClient.Services.IServices;

public interface IPublisherRepository : IRepository<PublisherDTO>
{
    public Task<List<PublisherDTO>> GetPublishersWithoutPagination(string url,string action);
}
