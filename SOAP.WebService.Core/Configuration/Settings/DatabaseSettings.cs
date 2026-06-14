using System.ComponentModel.DataAnnotations;

namespace SOAP.WebService.Core.Configuration.Settings;

public class DatabaseSettings
{
    [Required]
    public string Url { get; set; }
    [Required]
    public string DatabaseName { get; set; }
    [Required]
    public string Port { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}