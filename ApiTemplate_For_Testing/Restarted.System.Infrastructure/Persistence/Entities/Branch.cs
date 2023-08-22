namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? BranchCode { get; set; }

    public int OrganizationId { get; set; }

    public int BusinessType { get; set; }

    public int? AddressId { get; set; }

    public int? ContactId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<AssetType> AssetTypes { get; set; } = new List<AssetType>();

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual ICollection<Beacon> Beacons { get; set; } = new List<Beacon>();

    public virtual ICollection<BranchAttribute> BranchAttributes { get; set; } = new List<BranchAttribute>();

    public virtual BusinessType BusinessTypeNavigation { get; set; } = null!;

    public virtual Contact? Contact { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Receiver> Receivers { get; set; } = new List<Receiver>();
}