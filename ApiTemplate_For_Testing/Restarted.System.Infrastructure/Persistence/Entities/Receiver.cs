namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Receiver
{
    public int Id { get; set; }

    public string? UniqueId { get; set; }

    public string? Name { get; set; }

    public string? Label { get; set; }

    public bool HasCustomProximity { get; set; }

    public bool? ImmediateEvent { get; set; }

    public bool? NearEvent { get; set; }

    public bool? FarEvent { get; set; }

    public int? BranchId { get; set; }

    public int? LocationId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime UpdatedOn { get; set; }

    public int Rssifilter { get; set; }

    public int RssiatOneMeter { get; set; }

    public bool IsDistanceFilter { get; set; }

    public DateTime? DumpTimeStamp { get; set; }

    public decimal? EnvFactor { get; set; }

    public decimal? YValue { get; set; }

    public decimal? XValue { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Location? Location { get; set; }
}