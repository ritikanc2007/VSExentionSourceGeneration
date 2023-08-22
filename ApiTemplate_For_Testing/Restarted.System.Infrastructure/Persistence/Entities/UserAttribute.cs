namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class UserAttribute
{
    public int UserId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual AttributeMaster Attribute { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
