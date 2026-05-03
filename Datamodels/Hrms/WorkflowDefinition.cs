using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowDefinition
{
    public string WorkflowDefinitionId { get; set; } = null!;

    public string WorkflowName { get; set; } = null!;

    public string WorkflowCode { get; set; } = null!;

    public string? Description { get; set; }

    public string DefinitionJson { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<WorkflowDocument> WorkflowDocuments { get; set; } = new List<WorkflowDocument>();

    public virtual ICollection<WorkflowInstance> WorkflowInstances { get; set; } = new List<WorkflowInstance>();

    public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; } = new List<WorkflowStep>();
}
