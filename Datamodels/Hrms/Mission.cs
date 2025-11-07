using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Mission
{
    public string MissionId { get; set; } = null!;

    public string? MissionNameEng { get; set; }

    public string? MissionNameThai { get; set; }
}
