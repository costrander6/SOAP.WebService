namespace SOAP.WebService.Core.Entities;

public class Finding
{
    public Guid Id { get; set; }
    public required Guid ScanResultId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string File { get; set; }
    public required uint LineStart { get; set; }
    public required uint LineEnd { get; set; }
}