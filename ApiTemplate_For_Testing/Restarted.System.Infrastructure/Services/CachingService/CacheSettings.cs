namespace Restarted.System.Infrastructure.Services.CachingService
{
    public record CacheSettings(string? InstanceName, string? ServerConnection)
    {
        public static string SectionName { get; } = "CacheSettings";
        public CacheSettings() : this(null, null) { }
    }

}
