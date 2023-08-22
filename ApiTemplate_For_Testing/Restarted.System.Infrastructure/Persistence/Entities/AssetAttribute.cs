namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class AssetAttribute
{
    public int AssetId { get; set; }

    public string AtttributeName { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}