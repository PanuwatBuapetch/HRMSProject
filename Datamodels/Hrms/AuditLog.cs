using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class AuditLog
{
    public int Logid { get; set; }

    public int Studentid { get; set; }

    public string Action { get; set; } = null!;

    public string Actionby { get; set; } = null!;

    public DateTime? Actionat { get; set; }
}
