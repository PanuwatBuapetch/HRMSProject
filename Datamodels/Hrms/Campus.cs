using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Campus
{
    public string CampId { get; set; } = null!;

    public string? CampAdd { get; set; }

    public string? CampDesc { get; set; }

    public string? CampNameEng { get; set; }

    public string? CampNameThai { get; set; }
}
