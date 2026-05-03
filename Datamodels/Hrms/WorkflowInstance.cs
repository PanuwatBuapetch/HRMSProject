using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowInstance
{
    public string WorkflowInstanceId { get; set; } = null!;

    public string WorkflowDocumentId { get; set; } = null!;

    public string WorkflowDefinitionId { get; set; } = null!;

    public int? CurrentStepNo { get; set; }

    public string? CurrentApproverRole { get; set; }

    public string? CurrentApproverEmployeeId { get; set; }

    public string? Status { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public virtual WorkflowDefinition WorkflowDefinition { get; set; } = null!;

    public virtual WorkflowDocument WorkflowDocument { get; set; } = null!;

    public virtual ICollection<WorkflowInbox> WorkflowInboxes { get; set; } = new List<WorkflowInbox>();

    public virtual ICollection<WorkflowTransaction> WorkflowTransactions { get; set; } = new List<WorkflowTransaction>();
}
