using System;
using System.Collections.Generic;

namespace Datamodels.Hrms;

public partial class WorkflowStep
{
    public string WorkflowStepId { get; set; } = null!;

    public string WorkflowDefinitionId { get; set; } = null!;

    public int StepNo { get; set; }

    public string? StepName { get; set; }

    public string? StepType { get; set; }

    public string? ApproverRole { get; set; }

    public string? ApproverEmployeeId { get; set; }

    public string? OnApprove { get; set; }

    public string? OnReject { get; set; }

    public string? Expression { get; set; }

    public bool? IsRequired { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual WorkflowDefinition WorkflowDefinition { get; set; } = null!;
}
