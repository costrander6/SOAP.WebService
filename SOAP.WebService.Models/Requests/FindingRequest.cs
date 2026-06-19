namespace SOAP.WebService.Models.Requests;

public class FindingRequest
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string File { get; set; }
    public required uint LineStart { get; set; }
    public required uint LineEnd { get; set; }
}