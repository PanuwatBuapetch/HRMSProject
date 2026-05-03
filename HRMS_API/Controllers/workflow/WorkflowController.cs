using Datamodels.Hrms; // อย่าลืมเพิ่มบรรทัดนี้
using HRMS_API.Workflows;
using Microsoft.AspNetCore.Mvc;
using WorkflowCore.Interface;
using System.Linq; // สำหรับ FirstOrDefault

namespace HRMS_API.Controllers.workflow
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowHost _workflowHost;
        private readonly Hrms_dbContext _context; // 1. ประกาศตัวแปร _context

        // 2. ปรับ Constructor ให้รับ Hrms_dbContext เข้ามา
        public WorkflowController(IWorkflowHost workflowHost, Hrms_dbContext context)
        {
            _workflowHost = workflowHost;
            _context = context;
        }

        [HttpPost("start/{documentId}")]
        public async Task<IActionResult> Start(string documentId)
        {
            var data = new DocumentWorkflowData
            {
                DocumentId = documentId
            };

            var id = await _workflowHost.StartWorkflow("EmployeeDocumentWorkflow", 1, data);

            return Ok(new { InstanceId = id });
        }

        [HttpGet("status/{docId}")]
        public IActionResult GetStatus(string docId)
        {
            // ตอนนี้ _context จะใช้งานได้แล้ว
            var doc = _context.WorkflowDocuments.FirstOrDefault(x => x.WorkflowDocumentId == docId);

            if (doc == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Status = doc.Status,
                UpdatedAt = doc.UpdatedAt
            });
        }

        [HttpPost("approve/{docId}")]
        public IActionResult Approve(string docId)
        {
            var doc = _context.WorkflowDocuments.FirstOrDefault(x => x.WorkflowDocumentId == docId);
            if (doc == null) return NotFound();

            doc.Status = "Approved";
            doc.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentRequest req)
        {
            var docId = Guid.NewGuid().ToString();

            // 1. ตรวจสอบว่า WorkflowDefinitionId ที่คุณต้องใช้คืออะไร 
            // (สมมติว่ามี ID พื้นฐานที่ใช้สำหรับเอกสารลา)
            string defaultWorkflowDefId = "EmployeeDocumentWorkflow";

            var doc = new WorkflowDocument
            {
                WorkflowDocumentId = docId,
                DocumentTitle = req.Title,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

                // --- เพิ่มบรรทัดนี้ครับ ---
                WorkflowDefinitionId = defaultWorkflowDefId
            };

            _context.WorkflowDocuments.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new { Url = $"/approve/{docId}" });
        }


    }

    public class DocumentWorkflowData
    {
        public string DocumentId { get; set; }
    }

    public class DocumentRequest
    {
        public string Title { get; set; }
        public string ApproverId { get; set; }
    }
}