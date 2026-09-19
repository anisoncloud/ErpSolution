using Erp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Entities
{
    public class ProjectMaintenance : BaseEntity
    {
        public int ProjectItemId {  get; set; }
        public ProjectItem? ProjectItem { get; set; }
        public DateOnly? StartDate {  get; set; }
        public decimal? Value {  get; set; }
    }
}
