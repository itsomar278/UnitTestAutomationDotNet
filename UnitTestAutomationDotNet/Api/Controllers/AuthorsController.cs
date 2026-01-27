using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorsController(AuthorService authorService)
    {
        _authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool activeOnly = false)
    {
        var authors = activeOnly
            ? await _authorService.GetAllActiveAuthorsAsync()
            : await _authorService.GetAllAuthorsAsync();

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        if (author == null)
            return NotFound($"Author with id '{id}' not found");

        return Ok(author);
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var author = await _authorService.GetAuthorByEmailAsync(email);
            if (author == null)
                return NotFound($"Author with email '{email}' not found");

            return Ok(author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
        try
        {
            var author = await _authorService.CreateAuthorAsync(
                request.Name,
                request.Email,
                request.Bio ?? string.Empty);

            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}/name")]
    public async Task<IActionResult> UpdateName(Guid id, [FromBody] UpdateNameRequest request)
    {
        try
        {
            await _authorService.UpdateAuthorNameAsync(id, request.NewName);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}/email")]
    public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] UpdateEmailRequest request)
    {
        try
        {
            await _authorService.UpdateAuthorEmailAsync(id, request.NewEmail);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("not found")
                ? NotFound(ex.Message)
                : Conflict(ex.Message);
        }
    }

    [HttpPut("{id}/bio")]
    public async Task<IActionResult> UpdateBio(Guid id, [FromBody] UpdateBioRequest request)
    {
        try
        {
            await _authorService.UpdateAuthorBioAsync(id, request.NewBio);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        try
        {
            await _authorService.DeactivateAuthorAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("not found")
                ? NotFound(ex.Message)
                : BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        try
        {
            await _authorService.ActivateAuthorAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return ex.Message.Contains("not found")
                ? NotFound(ex.Message)
                : BadRequest(ex.Message);
        }
    }
}

public record CreateAuthorRequest(string Name, string Email, string? Bio);
public record UpdateNameRequest(string NewName);
public record UpdateEmailRequest(string NewEmail);
public record UpdateBioRequest(string NewBio);
