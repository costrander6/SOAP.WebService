namespace SOAP.WebService.Models.Requests;

public class ScanResultCreateRequest
{
    public required Guid WorkflowRunId { get; set; }
    public required string Scanner { get; set; }
    public FindingRequest[] Findings { get; set; } = [];
}