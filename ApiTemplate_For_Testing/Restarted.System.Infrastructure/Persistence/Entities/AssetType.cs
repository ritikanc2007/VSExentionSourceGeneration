namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class AssetType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? BranchId { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Branch? Branch { get; set; }
}