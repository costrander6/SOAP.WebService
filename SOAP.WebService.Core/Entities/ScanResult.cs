namespace SOAP.WebService.Core.Entities;

public class ScanResult
{
    public Guid Id { get; set; }
    public required Guid WorkflowRunId { get; set; }
    public required string Scanner { get; set; }
    public required DateTimeOffset Timestamp { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}