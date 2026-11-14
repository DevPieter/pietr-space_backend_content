using PietrSpace.Common.Results;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;
using PietrSpace.Content.Quotes.Data.Models.Entities;
using PietrSpace.Content.Quotes.Logic.Models.Args.Quotes;
using PietrSpace.Content.Quotes.Logic.Models.Domain;

namespace PietrSpace.Content.Quotes.Logic.Models;

public sealed class Quote : IdDomainEntity
{
    public Guid ExportId { get; private init; }
    public Guid? QuoteCollectionId { get; private set; }

    public string Text { get; private set; } = null!;
    public string? HtmlText { get; private set; }

    public string? Author { get; private set; }
    public string? Credits { get; private set; }

    public bool IsPublished { get; private set; }

    private Quote(
        Guid id,
        Guid exportId,
        Guid? quoteCollectionId,
        string text,
        string? htmlText,
        string? author,
        string? credits,
        bool isPublished,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        ExportId = exportId;
        QuoteCollectionId = quoteCollectionId;

        SetText(text);
        SetHtmlText(htmlText);

        SetAuthor(author);
        SetCredits(credits);

        SetPublished(isPublished);

        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Quote(
        CreateQuoteArg arg
    ) : this(
        id: Guid.NewGuid(),
        exportId: Guid.NewGuid(),
        quoteCollectionId: arg.QuoteCollectionId,
        text: arg.Text,
        htmlText: arg.HtmlText,
        author: arg.Author,
        credits: arg.Credits,
        isPublished: arg.IsPublished,
        createdAt: DateTime.UtcNow,
        updatedAt: DateTime.UtcNow
    )
    {
    }

    public Quote(
        QuoteEntity entity
    ) : this(
        entity.Id,
        entity.ExportId,
        entity.QuoteCollectionId,
        entity.Text,
        entity.HtmlText,
        entity.Author,
        entity.Credits,
        entity.IsPublished,
        entity.CreatedAt,
        entity.UpdatedAt
    )
    {
    }

    public Quote SetText(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Text cannot be empty");
        Text = ClampString(text, 2000);
        return this;
    }

    public Quote SetHtmlText(string? text)
    {
        HtmlText = text is null ? null : ClampString(text, 2000);
        return this;
    }

    public Quote SetAuthor(string? author)
    {
        Author = author is null ? null : ClampString(author, 255);
        return this;
    }

    public Quote SetCredits(string? credits)
    {
        Credits = credits is null ? null : ClampString(credits, 255);
        return this;
    }

    public Quote SetPublished(bool published)
    {
        IsPublished = published;
        return this;
    }

    public CreateQuoteDto ToCreateQuoteDto() => new(
        Id: Id,
        ExportId: ExportId,
        QuoteCollectionId: QuoteCollectionId,
        Text: Text,
        HtmlText: HtmlText,
        Author: Author,
        Credits: Credits,
        IsPublished: IsPublished
    );

    public static Result ValidateCreate(CreateQuoteArg arg)
    {
        return Result.Success();
    }
}