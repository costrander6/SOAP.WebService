using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IWorkflowRunRepository
{
    Task<int> Create(WorkflowRun workflowRun);
    Task<WorkflowRun?> Get(Guid id);
    Task<WorkflowRun?> GetMostRecent(string owner, string repo, string branch);
    Task<List<string>> GetAllRepos(string owner);
    Task<List<string>> GetAllBranches(string owner, string repo);
}