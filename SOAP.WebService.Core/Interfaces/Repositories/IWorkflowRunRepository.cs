using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IWorkflowRunRepository
{
    Task<int> Create(WorkflowRun workflowRun);
}