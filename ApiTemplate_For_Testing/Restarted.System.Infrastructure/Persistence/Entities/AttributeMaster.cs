namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class AttributeMaster
{
    public int Id { get; set; }

    public string AttributeType { get; set; } = null!;

    public string AttributeName { get; set; } = null!;

    public string? DataType { get; set; }

    public bool? IsRequired { get; set; }

    public string? DefaultValue { get; set; }

    public virtual ICollection<BranchAttribute> BranchAttributes { get; set; } = new List<BranchAttribute>();

    public virtual ICollection<UserAttribute> UserAttributes { get; set; } = new List<UserAttribute>();
}