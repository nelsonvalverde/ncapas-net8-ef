namespace WebApi.Entities.Base.Audit;

public class AuditEntity
{
    public string CreatedBy { get; set; } = default!;
    public DateTime Created { get; set; }
    public string LastModifiedBy { get; set; } = default!;
    public DateTime LastModified { get; set; }
}