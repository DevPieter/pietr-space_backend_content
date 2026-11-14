namespace PietrSpace.Content.Quotes.Logic.Models.Domain;

public abstract class DomainEntity
{
    public DateTime CreatedAt { get; protected init; }
    public DateTime UpdatedAt { get; protected set; }

    protected static string ClampString(string input, int maxLength)
        => input.Length > maxLength ? input[..maxLength] : input;
}