namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CountryCode { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}