using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class LoginSession
{
    public Guid Id { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateExpired { get; set; }

    public string? Ip { get; set; }

    public string? StaffId { get; set; }
}
