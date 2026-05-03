using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowTransaction
{
    public string WorkflowTransactionId { get; set; } = null!;

    public string WorkflowInstanceId { get; set; } = null!;

    public string WorkflowDocumentId { get; set; } = null!;

    public int? StepNo { get; set; }

    public string? StepName { get; set; }

    public string? ApproverEmployeeId { get; set; }

    public string? ApproverRole { get; set; }

    public string? Action { get; set; }

    public string? Comment { get; set; }

    public DateTime? ActionDate { get; set; }

    public virtual WorkflowDocument WorkflowDocument { get; set; } = null!;

    public virtual WorkflowInstance WorkflowInstance { get; set; } = null!;
}
