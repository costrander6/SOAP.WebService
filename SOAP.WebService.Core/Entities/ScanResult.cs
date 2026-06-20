using System.ComponentModel.DataAnnotations.Schema;
using SOAP.WebService.Models.Requests;

namespace SOAP.WebService.Core.Entities;

public class ScanResult
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid WorkflowRunId { get; set; }
    public string Scanner { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    
    public ScanResult() {}

    public ScanResult(ScanResultCreateRequest scanResultCreateRequest)
    {
        Id = Guid.NewGuid();
        WorkflowRunId = scanResultCreateRequest.WorkflowRunId;
        Scanner = scanResultCreateRequest.Scanner;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}