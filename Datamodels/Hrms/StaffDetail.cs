using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class StaffDetail
{
    public string StaffId { get; set; } = null!;

    public string? CampusId { get; set; }

    public string? DeptId { get; set; }

    public string? FacId { get; set; }

    public string? FullNameEng { get; set; }

    public string? FullNameThai { get; set; }

    public string? Password { get; set; }

    public string? Pincode { get; set; }

    public string? PosId { get; set; }

    public decimal? SecretCode { get; set; }

    public string? SectId { get; set; }

    public string? StaffDateOut { get; set; }

    public string? StaffDepart { get; set; }

    public string? StaffEmail { get; set; }

    public string? StaffEnd { get; set; }

    public string? StaffNameEng { get; set; }

    public string? StaffNameThai { get; set; }

    public string? StaffPersId { get; set; }

    public string? StaffPicture { get; set; }

    public string? StaffSnameEng { get; set; }

    public string? StaffSnameThai { get; set; }

    public string? TitleId { get; set; }

    public string? UnitId { get; set; }

    public string? Username { get; set; }

    public string? WorkBegin { get; set; }

    public string? WorkEnd { get; set; }
}
