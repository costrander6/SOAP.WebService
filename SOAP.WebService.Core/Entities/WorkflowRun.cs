namespace SOAP.WebService.Core.Entities;

public class WorkflowRun
{
    public Guid Id { get; set; }
    public required string ApiKeyHash { get; set; }
    public required string Repo { get; set; }
    public required string Branch { get; set; }
    public required string Commit { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}