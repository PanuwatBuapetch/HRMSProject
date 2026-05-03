using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowComment
{
    public string WorkflowCommentId { get; set; } = null!;

    public string WorkflowDocumentId { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual WorkflowDocument WorkflowDocument { get; set; } = null!;
}
