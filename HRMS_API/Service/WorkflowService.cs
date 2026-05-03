using Datamodels.Hrms;
using WorkflowCore.Interface;

namespace HRMS_API.Service
{
    public class WorkflowService
    {
        private readonly IWorkflowHost _workflowHost;
        private readonly Hrms_dbContext _context;

        public WorkflowService(IWorkflowHost workflowHost, Hrms_dbContext context)
        {
            _workflowHost = workflowHost;
            _context = context;
        }

        public async Task<string> StartWorkflow(string workflowDefinitionId, object data)
        {
            // 1. แปลง string จาก API เป็น Guid เพื่อให้ WorkflowCore ใช้งาน
            string definitionGuid = Guid.NewGuid().ToString();

            // 2. เรียกใช้งาน WorkflowCore
            // workflowHost จะคืนค่าเป็น Guid
            var instanceId = await _workflowHost.StartWorkflow(definitionGuid, data);

            // 3. คืนค่ากลับไปเป็น string ให้ Frontend
            return instanceId.ToString();
        }
    }
}
