namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Organization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? OrganizationCode { get; set; }

    public int BusinessType { get; set; }

    public string? ContactPerson { get; set; }

    public int? AddressId { get; set; }

    public int? ContactId { get; set; }

    public string? TimezoneId { get; set; }

    public string? LanguageId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual BusinessType BusinessTypeNavigation { get; set; } = null!;

    public virtual Contact? Contact { get; set; }
}