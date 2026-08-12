using Erp.Modules.HRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetDesignationAsync();
        Task<bool> CreateDesignation(DesignationCreateDto dto);
    }
}
