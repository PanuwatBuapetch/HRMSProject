using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Section
{
    public string SectId { get; set; } = null!;

    public string? CampusId { get; set; }

    public string? CentralId { get; set; }

    public string? DeptId { get; set; }

    public string? ExternalDocNo { get; set; }

    public string? FacId { get; set; }

    public string? InternalDocNo { get; set; }

    public string? Isactive { get; set; }

    public string? MissionId { get; set; }

    public string? NewSectionId { get; set; }

    public string? OldSect { get; set; }

    public string? SectDesc { get; set; }

    public string? SectNameEng { get; set; }

    public string? SectNameThai { get; set; }

    public string? SectTelFax { get; set; }
}
