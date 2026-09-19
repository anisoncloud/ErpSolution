using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Entities
{
    public class ProjectProduction : BaseEntity
    {
        public int ProjectItemId { get; set; }
        public ProjectItem? ProjectItem { get; set; }
        public bool? LiveOnServer {  get; set; }
        public bool? Maintenance {  get; set; }
        public DateOnly? StartDate { get; set; }
        public decimal? MaintenanceValue { get; set; }

    }
}
