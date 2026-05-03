using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowInbox
{
    public string WorkflowInboxId { get; set; } = null!;

    public string WorkflowInstanceId { get; set; } = null!;

    public string WorkflowDocumentId { get; set; } = null!;

    public string? ReceiverEmployeeId { get; set; }

    public string? ReceiverRole { get; set; }

    public bool? IsRead { get; set; }

    public string? Status { get; set; }

    public DateTime? ReceivedAt { get; set; }

    public virtual WorkflowDocument WorkflowDocument { get; set; } = null!;

    public virtual WorkflowInstance WorkflowInstance { get; set; } = null!;
}
