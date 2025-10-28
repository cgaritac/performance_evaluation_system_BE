using PACE.Utils.Constants;
using System.ComponentModel.DataAnnotations;

namespace PACE.Models.CommonModels;

public class PageRequestDTO
{
    [Range(NumericConstants.MinPage, NumericConstants.MaxPage)]
    public int Page { get; set; }

    [Range(NumericConstants.MinPageSize, NumericConstants.MaxPageSize)]
    public int PageSize { get; set; }

    [StringLength(NumericConstants.MaxSearchTerm, ErrorMessage = ErrorConstants.SearchTermLengthError)]
    public string? SearchTerm { get; set; }

    public string? SortBy { get; set; }

    public string? SortDirection { get; set; }

    public int? Year { get; set; }

    public int? DepartmentId { get; set; }

    public PageRequestDTO()
    {
        Page = NumericConstants.DefaultPage;
        PageSize = NumericConstants.DefaultPageSize;
        SearchTerm = null;
        SortBy = null;
        SortDirection = null;
        Year = null;
        DepartmentId = null;
    }

    public PageRequestDTO(int page, int pageSize, string? searchTerm, string? sortBy, string? sortDirection, int? year, int departmentId)
    {
        Page = page;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        SortBy = sortBy;
        SortDirection = sortDirection;
        Year = year;
        DepartmentId = departmentId;
    }
}
