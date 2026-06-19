namespace SOAP.WebService.Models.Responses;

public class WorkflowRunDetailsResponse : WorkflowRunResponse
{
    public IEnumerable<ScanResultResponse> Scans { get; set; } = [];
}