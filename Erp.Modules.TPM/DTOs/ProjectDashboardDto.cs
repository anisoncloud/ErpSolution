using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Modules.TPM.DTOs
{
    public record ProjectDashboardDto(
        int Id,
        string Name,
        List<TaskDetailsDto> TasksDetails
        ); // Collection of tasks assigned to this project);


}
