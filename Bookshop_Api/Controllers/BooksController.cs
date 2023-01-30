using Bookshop_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]

    public async Task<IActionResult> GetAllBooksWithoutPagination()
    {
        //var (authors,pageCount) = await _authorRepository.GetAllAuthors();
        var books = await _bookRepository.GetAllBooksWithoutPagination();
        if (books is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(books);
    }


    [HttpGet("[action]/{pageId}/{categoryId}/{endPrice}/{startPrice}/{publisherId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tuple<List<BookDTO>, int>))]

    public async Task<IActionResult> GetBooksWithPagination(int pageId = 0, int categoryId = 0, double endPrice = 0, double startPrice = 0, int publisherId = 0)
    {
        //var (authors,pageCount) = await _authorRepository.GetAllAuthors();
        var (books, pageCount) = _bookRepository.GetAllBooks(DateTime.MinValue, DateTime.MinValue, pageId, "", "", categoryId, startPrice, endPrice, publisherId);
        if (books is null)
            return StatusCode(StatusCodes.Status204NoContent);

        var tuple = Tuple.Create(books, pageCount);

        return Ok(tuple);
    }


    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))]
    public async Task<IActionResult> GetBookById(int Id)
    {
        var book = await _bookRepository.GetBookById(Id);
        if (book is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(book);
    }


    [HttpGet("[action]/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> GetBooksByCategoryId(int categoryId)
    {
        var books = await _bookRepository.GetBooksByCategoryId(categoryId);
        if (books is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(books);
    }


    [HttpGet("[action]/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    public async Task<IActionResult> GetbookCategoryTitle(int bookId)
    {
        var CategoryTitles = await _bookRepository.GetbookCategoryTitle(bookId);
        if (CategoryTitles is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(CategoryTitles);
    }

    //[HttpGet("[action]/{userId}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WishlistBookViewModel>))]
    //public async Task<IActionResult> GetBooksForWishlist(string userId)
    //{
    //    var booksForWishlist =  await _bookRepository.GetBooksForWishlist(userId);
    //    if (booksForWishlist is null)
    //        return StatusCode(StatusCodes.Status204NoContent);

    //    return Ok(booksForWishlist);
    //}


    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> GetLatestBooks()
    {
        var books = await _bookRepository.GetLatestBooks();
        if (books is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(books);
    }

    [HttpGet("[action]/{bookId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> DecreaseQuantity(int bookId)
    {
        var IsDecreased = await _bookRepository.DecreaseQuantity(bookId);

        if (IsDecreased)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("[action]/{q}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> GetBookBySearchTxt(string q)
    {
        var books = await _bookRepository.GetBookBySearchTxt(q);

        if (books is not null)
        {
            return Ok(books);
        }
        else
        {
            return BadRequest();
        }
    }


    
    [HttpGet("[action]/{AuthorId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> GetBooksByAuthorId(int AuthorId)
    {
        var books = await _bookRepository.GetBooksByAuthorId(AuthorId);

        if (books is not null)
        {
            return Ok(books);
        }
        else
        {
            return BadRequest();
        }
    }
    
    [HttpGet("[action]/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDTO>))]
    public async Task<IActionResult> GetbookAuthorName(int bookId)
    {
        var author = await _bookRepository.GetbookAuthorName(bookId);

        if (author is not null)
        {
            return Ok(author);
        }
        else
        {
            return BadRequest();
        }
    }
}
