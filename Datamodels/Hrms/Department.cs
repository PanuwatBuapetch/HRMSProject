using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class Department
{
    public string DeptId { get; set; } = null!;

    public string? CampusId { get; set; }

    public string? CentralId { get; set; }

    public string? DeptDesc { get; set; }

    public string? DeptMissionId { get; set; }

    public string? DeptNameEng { get; set; }

    public string? DeptNameThai { get; set; }

    public string? DeptTel { get; set; }

    public string? DeptTelFax { get; set; }

    public string? ExternalDocNo { get; set; }

    public string? FacId { get; set; }

    public string? InternalDocNo { get; set; }

    public string? Isactive { get; set; }

    public string? IscedId { get; set; }

    public string? JobId { get; set; }

    public string? MissionId { get; set; }

    public string? NewDeptId { get; set; }

    public string? OldDept { get; set; }

    public string? Priority { get; set; }
}
