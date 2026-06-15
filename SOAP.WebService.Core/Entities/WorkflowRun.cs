using System.ComponentModel.DataAnnotations.Schema;

namespace SOAP.WebService.Core.Entities;

public class WorkflowRun
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required Guid Id { get; set; }
    
    public required string Owner { get; set; }
    public required string Repo { get; set; }
    public required string Branch { get; set; }
    public required string Commit { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}