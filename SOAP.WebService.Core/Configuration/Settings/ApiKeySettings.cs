using System.ComponentModel.DataAnnotations;

namespace SOAP.WebService.Core.Configuration.Settings;

public class ApiKeySettings
{
    [Required]
    public int KeyLength { get; set; }
    [Required]
    public string KeyPrefix { get; set; }
}