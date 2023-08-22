namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class BusinessType
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}