using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Unit
{
    public string UnitId { get; set; } = null!;

    public string? CampusId { get; set; }

    public string? CentralId { get; set; }

    public string? DeptId { get; set; }

    public string? ExternalDocNo { get; set; }

    public string? FacId { get; set; }

    public string? InternalDocNo { get; set; }

    public string? Isactive { get; set; }

    public string? MissionId { get; set; }

    public string? NewUnitId { get; set; }

    public string? OldUnit { get; set; }

    public int? Rowid { get; set; }

    public string? SectId { get; set; }

    public string? UnitDesc { get; set; }

    public string? UnitNameEng { get; set; }

    public string? UnitNameThai { get; set; }

    public string? UnitTelFax { get; set; }
}
