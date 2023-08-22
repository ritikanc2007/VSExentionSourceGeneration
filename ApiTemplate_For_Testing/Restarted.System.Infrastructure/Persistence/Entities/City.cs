namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class City
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StateId { get; set; }

    public virtual State? State { get; set; }
}