using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Infrastructure.Database;

namespace SOAP.WebService.Infrastructure.Repositories;

public class WorkflowRunRepository(SoapDbContext dbContext) : IWorkflowRunRepository
{
    public Task<int> Create(WorkflowRun workflowRun)
    {
        dbContext.WorkflowRuns.Add(workflowRun);
        return dbContext.SaveChangesAsync();
    }
}