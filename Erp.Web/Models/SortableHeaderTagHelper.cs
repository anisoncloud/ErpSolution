using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;

namespace Erp.Web.Models
{
    [HtmlTargetElement("sortable-th")]
    public class SortableHeaderTagHelper : TagHelper
    {
        [HtmlAttributeName("column")]
        public string Column { get; set; } = default!;

        [HtmlAttributeName("display-text")]
        public string DisplayText { get; set; } = default!;

        [HtmlAttributeName("current-sort-column")]
        public string? CurrentSortColumn { get; set; }

        [HtmlAttributeName("current-sort-direction")]
        public string? CurrentSortDirection { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = default!;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "th";
            output.TagMode = TagMode.StartTagAndEndTag;

            var isCurrentColumn = string.Equals(Column, CurrentSortColumn, StringComparison.OrdinalIgnoreCase);
            var nextDirection = isCurrentColumn && string.Equals(CurrentSortDirection, "asc", StringComparison.OrdinalIgnoreCase)
                ? "desc"
                : "asc";

            // Keep every existing querystring value (search term, page size...)
            // and only overwrite sortColumn / sortDirection / pageNumber.
            var query = ViewContext.HttpContext.Request.Query
                .ToDictionary(q => q.Key, q => (string?)q.Value.ToString());

            query["sortColumn"] = Column;
            query["sortDirection"] = nextDirection;
            query["pageNumber"] = "1"; // re-sorting resets to page 1

            var url = QueryHelpers.AddQueryString(ViewContext.HttpContext.Request.Path.Value ?? "", query);

            var arrow = isCurrentColumn ? (CurrentSortDirection == "asc" ? " ▲" : " ▼") : "";

            output.Content.SetHtmlContent($"<a href=\"{url}\" style=\"text-decoration:none\">{DisplayText}{arrow}</a>");
        }
    }
}
