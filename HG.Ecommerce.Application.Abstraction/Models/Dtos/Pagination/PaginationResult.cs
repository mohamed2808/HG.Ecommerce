namespace HG.Ecommerce.Application.Abstraction.Models.Dtos.Pagination
{
        public class PaginationResult<T>
        {
            public IEnumerable<T> Data { get; set; } = new List<T>();
            public int TotalCount { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        }
}
