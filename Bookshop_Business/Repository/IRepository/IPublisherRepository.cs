
namespace Bookshop_Business.Repository.IRepository;

public interface IPublisherRepository
{
    public Task<Tuple<List<PublisherDTO>, int>> GetAllPublishers(int pageId = 1, string publisherName="");

    public Task<List<PublisherDTO>> GetPublishersWithoutPagination();
    public PublisherDTO GetPublisherById(int PublisherId);
    public Task<bool> AddPublisher(PublisherDTO PublisherDTO);
    public Task<bool> IsPublisherTitleUnique(string PublisherTitle);
    public Task<bool> DeletePublisher(int PublisherId);
    public Task<bool> UpdatePublisher(int PublisherId, PublisherDTO PublisherDTO);
}
