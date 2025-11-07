using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Management
{
    public string ManagementId { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? ManagementPositionId { get; set; }

    public string? TempAdminCode { get; set; }

    public string? Isactive { get; set; }

    public string? LocationId { get; set; }

    public string? DivisionId { get; set; }

    public string? DeptId { get; set; }

    public string? TeamId { get; set; }

    public string? UnitId { get; set; }
}
