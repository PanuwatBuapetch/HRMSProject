using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Executive
{
    public string ExecutiveId { get; set; } = null!;

    public string? CampusId { get; set; }

    public string? DeptId { get; set; }

    public string? ExecutivePositionId { get; set; }

    public string? FacId { get; set; }

    public string? Isactive { get; set; }

    public string? SectId { get; set; }

    public string? StaffAdminTemp { get; set; }

    public string? StaffId { get; set; }

    public string? UnitId { get; set; }
}
