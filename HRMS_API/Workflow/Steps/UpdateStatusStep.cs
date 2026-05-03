using Datamodels.Hrms;
using HRMS_API.Workflows;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace HRMS_API.Workflows.Steps
{
    public class UpdateStatusStep : StepBody
    {
        private readonly Hrms_dbContext _context;

        // รับค่า Status เข้ามาเพื่อกำหนดว่า Step นี้จะทำอะไร
        public string Status { get; set; }

        public UpdateStatusStep(Hrms_dbContext context)
        {
            _context = context;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            // ดึงค่า Data จาก Workflow
            var data = context.Workflow.Data as DocumentWorkflowData;

            // ดึง DocumentId ออกมา
            string docId = data?.DocumentId;

            if (!string.IsNullOrEmpty(docId))
            {
                // ค้นหาเอกสารจากฐานข้อมูล
                var doc = _context.WorkflowDocuments.Find(Guid.Parse(docId));

                if (doc != null)
                {
                    // อัปเดตสถานะและเวลา
                    doc.Status = this.Status;
                    doc.UpdatedAt = DateTime.Now;

                    // บันทึกการเปลี่ยนแปลง
                    _context.SaveChanges();
                }
            }

            // จบการทำงานของ Step นี้และไปยัง Step ถัดไป
            return ExecutionResult.Next();
        }
    }
    public class DocumentWorkflowData
    {
        public string DocumentId { get; set; }
    }
}