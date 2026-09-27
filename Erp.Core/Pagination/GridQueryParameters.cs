using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.Pagination
{
    public class GridQueryParameters
    {
        private int _pageNumber = 1;
        private int _pageSize = 25;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? 10 : (value > 100 ? 100 : value); // cap to protect the DB
        }

        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string SortDirection { get; set; } = "asc"; // "asc" | "desc"

    }
}
