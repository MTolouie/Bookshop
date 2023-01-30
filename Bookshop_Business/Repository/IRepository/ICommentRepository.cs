
namespace Bookshop_Business.Repository.IRepository;

public interface ICommentRepository
{
    public Task<List<CommentDTO>> GetAllComments();
    public Task<List<CommentDTO>> GetCommentsByBookId(int bookId);
    public Task<bool> AddComment(CommentDTO comment);
    
}
