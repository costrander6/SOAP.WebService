using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Interfaces.Services;

public interface IWorkflowRunDetailsService
{
    Task<WorkflowRunDetailsResponse?> GetMostRecentWorkflowRunDetails(string owner, string repo, string branch);
}