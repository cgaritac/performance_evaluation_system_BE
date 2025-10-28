using PACE.Utils.Constants;

namespace PACE.Models.CommonModels;

public class PageResponseDTO<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public PageResponseDTO(List<T> items, int totalCount, int page, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public PageResponseDTO()
    {
        Items = new List<T>();
        TotalCount = NumericConstants.DefaultTotalCount;
        Page = NumericConstants.DefaultPage;
        PageSize = NumericConstants.DefaultPageSize;
        TotalPages = NumericConstants.DefaultTotalPages;
    }
}
