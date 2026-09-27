using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Pagination
{
    public class PagedResult<T> : IPagedResult
    {
        public List<T> Items { get; set; } = new();

        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Echoed back so the view can re-render the search box / sort arrows
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string SortDirection { get; set; } = "asc";

    }
}
