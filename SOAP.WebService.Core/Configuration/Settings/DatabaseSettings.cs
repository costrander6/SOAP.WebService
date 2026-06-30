using System.ComponentModel.DataAnnotations;

namespace SOAP.WebService.Core.Configuration.Settings;

public class DatabaseSettings
{
    [Required]
    public string Url { get; set; } = null!;
    [Required]
    public string DatabaseName { get; set; } = null!;
    [Required]
    public string Port { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}