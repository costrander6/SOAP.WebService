namespace SOAP.WebService.Models.Responses;

public class ScanResultReponse
{
    public Guid WorkflowRunId { get; set; }
    public string Scanner { get; set; } = null!;
    public IEnumerable<FindingResponse> Findings { get; set; } = [];
    public DateTimeOffset Timestamp { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}