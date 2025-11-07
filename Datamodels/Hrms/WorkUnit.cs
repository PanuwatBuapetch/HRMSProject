using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkUnit
{
    public string UnitId { get; set; } = null!;

    public string? LocationId { get; set; }

    public string? DivisionId { get; set; }

    public string? DeptId { get; set; }

    public string? TeamId { get; set; }

    public string? UnitNameEng { get; set; }

    public string? UnitNameThai { get; set; }

    public string? Isactive { get; set; }

    public string? MissionId { get; set; }
}
