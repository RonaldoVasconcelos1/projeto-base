namespace Domain.Entidades;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid(); // Gera um novo ID único para cada instância
        CreatedAt = DateTime.UtcNow; // Define a data de criação como o momento atual em UTC
    }
}