using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Faculty
{
    public string FacId { get; set; } = null!;

    public string? CampId { get; set; }

    public string? CentralId { get; set; }

    public string? EmpNumber { get; set; }

    public string? ExternalDocNo { get; set; }

    public string? FacDesc { get; set; }

    public string? FacGrpId { get; set; }

    public string? FacNameEng { get; set; }

    public string? FacNameThai { get; set; }

    public string? FacTelFax { get; set; }

    public string? InternalDocNo { get; set; }

    public string? Isactive { get; set; }

    public string? IscedId { get; set; }

    public string? MissionId { get; set; }

    public string? NFacId { get; set; }

    public string? NewFacId { get; set; }

    public string? OldFac { get; set; }

    public string? ShortNameThai { get; set; }

    public string? StaffNumber { get; set; }
}
