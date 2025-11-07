using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Division
{
    public string DivisionId { get; set; } = null!;

    public string? LocationId { get; set; }

    public string? CentralId { get; set; }

    public string? DivisionDesc { get; set; }

    public string? DivisionNameEng { get; set; }

    public string? DivisionNameThai { get; set; }

    public string? Isactive { get; set; }

    public string? MissionId { get; set; }
}
