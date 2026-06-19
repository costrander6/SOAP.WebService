namespace SOAP.WebService.Models.Requests;

public class WorkflowRunCreateRequest
{
    public required string Repo { get; set; }
    public required string Branch { get; set; }
    public required string Commit { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
}