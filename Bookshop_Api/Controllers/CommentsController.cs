using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{

    private readonly ICommentRepository _commentRepository;
    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet("[action]/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommentDTO>))]
    public async Task<IActionResult> GetCommentsByBookId(int bookId)
    {
        var Comments = await _commentRepository.GetCommentsByBookId(bookId);
        if (Comments is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(Comments);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CommentDTO>))]
    public async Task<IActionResult> AddComment([FromBody] CommentDTO comment)
    {
        if (comment is null)
        {
            return BadRequest(ModelState);
        }

        var IsAdded = await _commentRepository.AddComment(comment);
        if (IsAdded)
        {
            return Ok();
        }
        else
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }

}
