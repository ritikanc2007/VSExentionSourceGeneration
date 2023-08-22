namespace Restarted.System.Application.Common.Behaviours.Caching
{

    public class CacheInvalidationAttribute : Attribute
    {
        public CacheInvalidationAttribute()
        {

        }

        public CacheInvalidationAttribute(string key)
        {
            this.Key= key;
        }
        public string Key { get; set; }



    }
}
