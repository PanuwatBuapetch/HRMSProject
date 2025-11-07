using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class ExecutivePosition
{
    public string? Id { get; set; }

    public int? Level { get; set; }

    public string? NameEng { get; set; }

    public string? NameThai { get; set; }
}
