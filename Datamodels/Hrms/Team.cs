using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Team
{
    public string TeamId { get; set; } = null!;

    public string? LocationId { get; set; }

    public string? DivisionId { get; set; }

    public string? DeptId { get; set; }

    public string? TeamNameEng { get; set; }

    public string? TeamNameThai { get; set; }

    public string? Isactive { get; set; }

    public string? MissionId { get; set; }
}
