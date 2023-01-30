namespace Bookshop_BlazorClient.Services.IServices;

public interface ICommentRepository : IRepository<CommentDTO>
{
    Task<List<CommentDTO>> GetCommentsByBookId(string url,string action,int bookId);
}
