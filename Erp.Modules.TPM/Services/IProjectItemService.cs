using Erp.Modules.TPM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.Services
{
    public interface IProjectItemService
    {
        Task<ProjectItemDto> CreateProjectItem(ProjectItemCreateDto dto);
    }
}
