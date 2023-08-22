namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Beacon
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string UniqueId { get; set; } = null!;

    public int Major { get; set; }

    public int Minor { get; set; }

    public string? ProviderCode { get; set; }

    public string? DeviceId { get; set; }

    public int? BranchId { get; set; }

    public bool IsAllocated { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime? DumpTimeStamp { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public virtual Branch? Branch { get; set; }
}