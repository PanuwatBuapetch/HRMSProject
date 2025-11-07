using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class JobPosition
{
    public string PositionId { get; set; } = null!;

    public string? PositionNameEng { get; set; }

    public string? PositionNameThai { get; set; }

    public string? PositionType { get; set; }
}
