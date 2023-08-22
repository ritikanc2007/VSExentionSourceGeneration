namespace Restarted.System.Infrastructure.Persistence.Entities;

public partial class TimeZone
{
    public string TimeZoneId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? StandardName { get; set; }

    public string? DaylightName { get; set; }

    public string BaseUtcOffset { get; set; } = null!;

    public bool SupportsDaylightSavingTime { get; set; }
}
