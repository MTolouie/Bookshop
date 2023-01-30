
namespace Bookshop_Business.Repository;

public class PublisherRepository : IPublisherRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public PublisherRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddPublisher(PublisherDTO PublisherDTO)
    {
        try
        {

            var publisher = _mapper.Map <PublisherDTO, Publisher>(PublisherDTO);
            publisher.PublisherTitle.Trim();
            _db.Publishers.Add(publisher);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeletePublisher(int PublisherId)
    {
        try
        {
            var publisher = await _db.Publishers.FindAsync(PublisherId);

            if (publisher is null)
                return false;

            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Tuple<List<PublisherDTO>,int>> GetAllPublishers(int pageId = 1, string publisherName="")
    {
        IQueryable<Publisher> publishers = _db.Publishers;

        if (!string.IsNullOrEmpty(publisherName))
        {
            publishers = publishers.Where(p => p.PublisherTitle.Contains(publisherName.ToLower().Trim()));
        }


        var publishersDTO = _mapper.Map<IQueryable<Publisher>, List<PublisherDTO>>(publishers);


        int take = 5;
        int skip = (pageId - 1) * take;
        var pageCount = publishersDTO.Count() / take;
        if (pageCount % 2 != 0)
        {
            pageCount++;
        }
        var query = publishersDTO.OrderByDescending(a => a.PublisherId).Skip(skip).Take(take).ToList();
        return Tuple.Create(query, pageCount);
    }

    public PublisherDTO GetPublisherById(int PublisherId)
    {

        try
        {
            var publisher =  _db.Publishers.Find(PublisherId);

            if (publisher is null)
                return null;

            var publisherDTO = _mapper.Map<Publisher, PublisherDTO>(publisher);
            return publisherDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<PublisherDTO>> GetPublishersWithoutPagination()
    {
        var publishers = await _db.Publishers.ToListAsync();


        var publishersDTO = _mapper.Map<List<Publisher>, List<PublisherDTO>>(publishers);

        return publishersDTO.OrderByDescending(p=>p.PublisherId).ToList();
    }

    public async Task<bool> IsPublisherTitleUnique(string PublisherTitle)
    {
        var title = await _db.Publishers.FirstOrDefaultAsync(p => p.PublisherTitle.ToLower() == PublisherTitle.ToLower());
        if (title is not null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> UpdatePublisher(int PublisherId, PublisherDTO PublisherDTO)
    {
        try
        {
            if (PublisherDTO.PublisherId != PublisherId)
                return false;

            var PublisherFromDb = await _db.Publishers.FindAsync(PublisherId);
            PublisherFromDb.PublisherTitle = PublisherDTO.PublisherTitle;

            _db.Publishers.Update(PublisherFromDb);
            await _db.SaveChangesAsync();

            return true;


        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
