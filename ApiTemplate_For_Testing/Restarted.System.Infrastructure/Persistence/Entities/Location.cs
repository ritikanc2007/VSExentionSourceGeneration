namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Receiver> Receivers { get; set; } = new List<Receiver>();
}