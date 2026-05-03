using HRMS_API.Workflows.Steps;
using WorkflowCore.Interface;

namespace HRMS_API.Workflows
{
    public class EmployeeDocumentWorkflow : IWorkflow
    {
        public string Id => "EmployeeDocumentWorkflow";
        public int Version => 1;

        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                // ขั้นตอนที่ 1: ตั้งค่า Status เป็น Pending
                .StartWith<UpdateStatusStep>()
                .Input(step => step.Status, data => "Pending")

                // ขั้นตอนที่ 2: ตั้งค่า Status เป็น Approved
                .Then<UpdateStatusStep>()
                .Input(step => step.Status, data => "Approved")

                .Then(context => Console.WriteLine("เอกสารเสร็จสมบูรณ์!"));
        }
    }
}