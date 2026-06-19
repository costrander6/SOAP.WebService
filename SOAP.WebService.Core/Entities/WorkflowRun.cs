using System.ComponentModel.DataAnnotations.Schema;
using SOAP.WebService.Models.Requests;

namespace SOAP.WebService.Core.Entities;

public class WorkflowRun
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public string Owner { get; set; } = null!;
    public string Repo { get; set; } = null!;
    public string Branch { get; set; } = null!;
    public string Commit { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }

    public WorkflowRun() {}
    
    public WorkflowRun(WorkflowRunCreateRequest workflowRunCreateRequest, string owner)
    {
        Id = Guid.NewGuid();
        Owner = owner;
        Repo = workflowRunCreateRequest.Repo;
        Branch = workflowRunCreateRequest.Branch;
        Commit = workflowRunCreateRequest.Commit;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}