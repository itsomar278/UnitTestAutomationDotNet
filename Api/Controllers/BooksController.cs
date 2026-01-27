using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableBooks()
    {
        var books = await _bookService.GetAvailableBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound($"Book with ID {id} not found");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
    {
        try
        {
            var book = await _bookService.CreateBookAsync(
                request.Title,
                request.Author,
                request.ISBN,
                request.PublicationYear);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/borrow")]
    public async Task<IActionResult> BorrowBook(Guid id)
    {
        try
        {
            await _bookService.BorrowBookAsync(id);
            return Ok(new { message = "Book borrowed successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        try
        {
            await _bookService.ReturnBookAsync(id);
            return Ok(new { message = "Book returned successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public record CreateBookRequest(string Title, string Author, string ISBN, int PublicationYear);
