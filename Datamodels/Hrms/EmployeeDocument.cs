using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class EmployeeDocument
{
    public Guid DocumentId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? DocumentType { get; set; }

    public string? FileName { get; set; }

    public string? FileUrl { get; set; }

    public DateTime? UploadedAt { get; set; }
}
