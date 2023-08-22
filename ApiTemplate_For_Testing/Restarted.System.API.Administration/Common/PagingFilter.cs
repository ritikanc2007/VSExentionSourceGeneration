namespace Restarted.System.Api.Administration.Common
{
    public class PagingFilter
    {
        public string? SearchFilter { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; }
    }
}