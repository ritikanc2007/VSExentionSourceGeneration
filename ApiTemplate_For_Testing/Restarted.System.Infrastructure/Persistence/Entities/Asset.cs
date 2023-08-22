namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Asset
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int AssetTypeId { get; set; }

    public int? BranchId { get; set; }

    public int? BeaconId { get; set; }

    public string? ExternalId { get; set; }

    public byte[]? Image { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<AssetAttribute> AssetAttributes { get; set; } = new List<AssetAttribute>();

    public virtual AssetType AssetType { get; set; } = null!;

    public virtual Beacon? Beacon { get; set; }

    public virtual Branch? Branch { get; set; }
}