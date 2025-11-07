using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Location
{
    public string LocationId { get; set; } = null!;

    public string? LocationAddress { get; set; }

    public string? LocationDesc { get; set; }

    public string? LocationNameEng { get; set; }

    public string? LocationNameThai { get; set; }
}
