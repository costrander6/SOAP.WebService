using System.ComponentModel.DataAnnotations.Schema;

namespace SOAP.WebService.Core.Entities;

public class Finding
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required Guid Id { get; set; }
    
    public required Guid ScanResultId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string File { get; set; }
    public required uint LineStart { get; set; }
    public required uint LineEnd { get; set; }
}