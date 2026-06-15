using System.ComponentModel.DataAnnotations.Schema;

namespace SOAP.WebService.Core.Entities;

public class ApiKeyAssociation
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public required Guid Id { get; set; }
    
    public required string KeyHash { get; set; }
    public required string Owner { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
}