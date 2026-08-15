using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
