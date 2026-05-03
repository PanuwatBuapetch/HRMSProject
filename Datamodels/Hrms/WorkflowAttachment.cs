using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowAttachment
{
    public string WorkflowAttachmentId { get; set; } = null!;

    public string WorkflowDocumentId { get; set; } = null!;

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public long? FileSize { get; set; }

    public string? UploadedBy { get; set; }

    public DateTime? UploadedAt { get; set; }

    public virtual WorkflowDocument WorkflowDocument { get; set; } = null!;
}
