namespace PietrSpace.Content.Quotes.Logic.Models.Domain;

public abstract class IdDomainEntity : DomainEntity
{
    public Guid Id { get; protected init; }
}