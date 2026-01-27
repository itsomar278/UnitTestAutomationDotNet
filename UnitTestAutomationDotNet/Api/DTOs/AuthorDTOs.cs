namespace Api.DTOs;

public record AuthorDto(
    Guid Id,
    string Name,
    string Email,
    string Bio,
    DateTime CreatedAt,
    bool IsActive);

public record AuthorSummaryDto(
    Guid Id,
    string Name,
    string Email,
    bool IsActive);

public record CreateAuthorDto(
    string Name,
    string Email,
    string? Bio);

public record UpdateAuthorDto(
    string? Name,
    string? Email,
    string? Bio);

public static class AuthorMappings
{
    public static AuthorDto ToDto(this Domain.Entities.Author author)
    {
        return new AuthorDto(
            author.Id,
            author.Name,
            author.Email,
            author.Bio,
            author.CreatedAt,
            author.IsActive);
    }

    public static AuthorSummaryDto ToSummaryDto(this Domain.Entities.Author author)
    {
        return new AuthorSummaryDto(
            author.Id,
            author.Name,
            author.Email,
            author.IsActive);
    }

    public static IEnumerable<AuthorDto> ToDtos(this IEnumerable<Domain.Entities.Author> authors)
    {
        return authors.Select(a => a.ToDto());
    }

    public static IEnumerable<AuthorSummaryDto> ToSummaryDtos(this IEnumerable<Domain.Entities.Author> authors)
    {
        return authors.Select(a => a.ToSummaryDto());
    }
}
