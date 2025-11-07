using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class ManagementPosition
{
    public string ManagementPositionId { get; set; } = null!;

    public int? PositionLevel { get; set; }

    public string? PositionNameEng { get; set; }

    public string? PositionNameThai { get; set; }
}
