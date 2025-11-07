using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class EmployeeTitle
{
    public string TitleId { get; set; } = null!;

    public string? TitleNameEng { get; set; }

    public string? TitleNameThai { get; set; }

    public string? TitleShortEng { get; set; }

    public string? TitleShortThai { get; set; }
}
