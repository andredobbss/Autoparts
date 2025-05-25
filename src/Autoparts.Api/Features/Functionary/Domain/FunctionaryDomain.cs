namespace Autoparts.Api.Features.Functionary.Domain;

public class FunctionaryDomain
{
  
    protected FunctionaryDomain() { }

    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public FunctionaryDomain(DateTime createdAt)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(DateTime updatedAt)
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete(DateTime deletedAt)
    {
        DeletedAt = DateTime.UtcNow;
    }

}
