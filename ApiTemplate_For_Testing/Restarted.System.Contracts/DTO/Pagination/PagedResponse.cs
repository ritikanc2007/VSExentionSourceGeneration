namespace Restarted.System.Contracts.DTO.Pagination
{
    public class PagedResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;


        public T Data { get; set; }

        public PagedResponse(T data, int pageIndex, int pageSize, int totalCount)
        {
            PageIndex=pageIndex;
            PageSize=pageSize;
            TotalCount=totalCount;
            TotalPages=(int)Math.Ceiling(totalCount / (double)pageSize);

            this.Data=  data;

        }


    }
}
