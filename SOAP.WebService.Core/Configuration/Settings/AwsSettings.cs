using System.ComponentModel.DataAnnotations;

namespace SOAP.WebService.Core.Configuration.Settings;

public class AwsSettings
{
    [Required]
    public string UserPoolId { get; set; } = null!;
    
    [Required]
    public string UserPoolClientId { get; set; } = null!;
    
    [Required]
    public string UserPoolClientSecret { get; set; } = null!;
    
    [Required]
    public string Region { get; set; } = null!;
    
    [Required]
    public string FrontendClientId { get; set; } = null!;
}