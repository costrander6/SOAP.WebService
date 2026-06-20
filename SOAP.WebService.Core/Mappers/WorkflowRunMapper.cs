using SOAP.WebService.Core.Entities;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Mappers;

public static class WorkflowRunMapper
{
    public static WorkflowRunResponse MapEntityToResponse(WorkflowRun workflowRun)
    {
        return new WorkflowRunResponse
        {
            Id = workflowRun.Id,
            Repo = workflowRun.Repo,
            Branch = workflowRun.Branch,
            Commit = workflowRun.Commit,
            Timestamp = workflowRun.Timestamp,
            CreatedAt = workflowRun.CreatedAt
        };
    }
}