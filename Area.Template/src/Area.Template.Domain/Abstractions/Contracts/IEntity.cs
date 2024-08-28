namespace Area.Template.Domain.Abstractions.Contracts;

public interface IEntity 
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}
