
namespace Bookshop_Business.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CommentRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddComment(CommentDTO comment)
    {
        try
        {
            var commentDTO = _mapper.Map<CommentDTO, Comment>(comment);
            _db.Comments.Add(commentDTO);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<CommentDTO>> GetAllComments()
    {
        try
        {
            var comments = await _db.Comments.ToListAsync();
            var commentsDTO = _mapper.Map<List<Comment>, List<CommentDTO>>(comments);

            return commentsDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<CommentDTO>> GetCommentsByBookId(int bookId)
    {
        try
        {
            var comments = await _db.Comments.Where(c => c.BookId == bookId).ToListAsync();

            if (comments is null)
                return null;

            var commentsDTO = _mapper.Map<List<Comment>, List<CommentDTO>>(comments);
            return commentsDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
