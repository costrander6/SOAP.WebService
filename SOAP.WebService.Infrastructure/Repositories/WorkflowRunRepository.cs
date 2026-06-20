using Microsoft.EntityFrameworkCore;
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

    public Task<WorkflowRun?> Get(Guid id)
    {
        return dbContext.WorkflowRuns.FirstOrDefaultAsync(workflowRun => workflowRun.Id == id);
    }

    public Task<WorkflowRun?> GetMostRecent(string owner, string repo, string branch)
    {
        // inefficient but works for my tiny capstone. Better solution would be adding an index
        return dbContext.WorkflowRuns
            .Where(w => w.Owner == owner && w.Repo == repo && w.Branch == branch)
            .OrderByDescending(w => w.Timestamp)
            .FirstOrDefaultAsync();
    }
}