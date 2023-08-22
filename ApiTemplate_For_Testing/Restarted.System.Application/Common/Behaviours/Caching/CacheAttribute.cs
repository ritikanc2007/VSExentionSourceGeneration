namespace Restarted.System.Application.Common.Behaviours.Caching
{
    public class CacheAttribute : Attribute
    {
        public CacheAttribute(string key = null, int absoluteExpirationMinutes = 0, int slidingExpirationMinutes = 0)
        {
            Key=key;
            AbsoluteExpirationMinutes=absoluteExpirationMinutes;
            SlidingExpirationMinutes=slidingExpirationMinutes;
        }

        public string Key { get; set; }

        public int? AbsoluteExpirationMinutes { get; set; }

        public int? SlidingExpirationMinutes { get; set; }
    }
}
