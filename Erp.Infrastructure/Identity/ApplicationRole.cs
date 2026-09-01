using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
