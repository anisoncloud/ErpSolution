using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Pagination
{
    public interface IPagedResult
    {
        int PageNumber { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

    }
}
