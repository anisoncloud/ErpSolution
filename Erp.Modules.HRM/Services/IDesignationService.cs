using Erp.Modules.HRM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.HRM.Services
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetDesignationAsync();
        Task<DesignationDto> CreateDesignation(DesignationCreateDto dto);
        Task<DesignationDto?> GetDesignationByIdAsync(int id);
        Task<DesignationDto> UpdateDesignationAsync(int id, DesignationUpdateDto dto);
        Task<IEnumerable<DesignationDto>> GetDesignationAscSortNameAsync();
    }
}
