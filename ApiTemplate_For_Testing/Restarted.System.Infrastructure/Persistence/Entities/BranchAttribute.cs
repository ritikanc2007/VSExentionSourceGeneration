namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class BranchAttribute
{
    public int BranchId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual AttributeMaster Attribute { get; set; } = null!;

    public virtual Branch Branch { get; set; } = null!;
}