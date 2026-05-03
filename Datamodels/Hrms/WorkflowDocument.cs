using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowDocument
{
    public string WorkflowDocumentId { get; set; } = null!;

    public string WorkflowDefinitionId { get; set; } = null!;

    public string? DocumentNo { get; set; }

    public string? DocumentTitle { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentData { get; set; }

    public string? RequesterEmployeeId { get; set; }

    public int? CurrentStep { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public virtual ICollection<WorkflowAttachment> WorkflowAttachments { get; set; } = new List<WorkflowAttachment>();

    public virtual ICollection<WorkflowComment> WorkflowComments { get; set; } = new List<WorkflowComment>();

    public virtual WorkflowDefinition WorkflowDefinition { get; set; } = null!;

    public virtual ICollection<WorkflowInbox> WorkflowInboxes { get; set; } = new List<WorkflowInbox>();

    public virtual ICollection<WorkflowInstance> WorkflowInstances { get; set; } = new List<WorkflowInstance>();

    public virtual ICollection<WorkflowTransaction> WorkflowTransactions { get; set; } = new List<WorkflowTransaction>();
}
