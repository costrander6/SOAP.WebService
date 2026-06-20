using System.ComponentModel.DataAnnotations.Schema;
using SOAP.WebService.Models.Enums;
using SOAP.WebService.Models.Requests;

namespace SOAP.WebService.Core.Entities;

public class Finding
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid ScanResultId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string File { get; set; } = null!;
    public uint LineStart { get; set; }
    public uint LineEnd { get; set; }
    public SeverityLevel Severity { get; set; }
    
    public Finding() {}

    public Finding(FindingRequest findingRequest, Guid scanResultId)
    {
        Id =  Guid.NewGuid();
        ScanResultId = scanResultId;
        Title = findingRequest.Title;
        Description = findingRequest.Description;
        File = findingRequest.File;
        LineStart = findingRequest.LineStart;
        LineEnd = findingRequest.LineEnd;
        Severity = findingRequest.Severity;
    }
}