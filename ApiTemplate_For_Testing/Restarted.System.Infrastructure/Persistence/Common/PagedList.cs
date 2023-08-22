using Microsoft.EntityFrameworkCore;

namespace Restarted.System.Infrastructure.Persistence.Common
{
    internal class PagedList<T>
    {
        public PagedList(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items=items;
            PageIndex=pageIndex;
            PageSize=pageSize;
            TotalCount=totalCount;
        }

        public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasNextPage => (PageIndex * PageSize) < TotalCount;
        public bool HasPreviousPage => PageIndex>1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            int totalCount = await query.CountAsync();

            var items = await query.Skip((page -1) * pageSize).Take(pageSize).ToListAsync();

            return new(items, page, pageSize, totalCount);
        }
    }
}