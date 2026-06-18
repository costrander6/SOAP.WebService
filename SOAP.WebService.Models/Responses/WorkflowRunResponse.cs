namespace SOAP.WebService.Models.Responses;

public class WorkflowRunResponse
{
    public Guid Id { get; set; }
    public string Repo { get; set; } = null!;
    public string Branch { get; set; } = null!;
    public string Commit { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}