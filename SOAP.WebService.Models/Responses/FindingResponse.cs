namespace SOAP.WebService.Models.Responses;

public class FindingResponse
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string File { get; set; } = null!;
    public uint LineStart { get; set; }
    public uint LineEnd { get; set; }
}