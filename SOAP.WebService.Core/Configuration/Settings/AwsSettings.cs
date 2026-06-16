using System.ComponentModel.DataAnnotations;

namespace SOAP.WebService.Core.Configuration.Settings;

public class AwsSettings
{
    [Required]
    public string UserPoolId { get; set; }
    [Required]
    public string UserPoolClientId { get; set; }
    [Required]
    public string UserPoolClientSecret { get; set; }
    [Required]
    public string Region { get; set; }
}