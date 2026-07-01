using SOAP.WebService.Core.Interfaces.Configuration;

namespace SOAP.WebService.Core.Configuration.Settings;

public class AppSettings : IAppSettings
{
    public DatabaseSettings DatabaseSettings { get; set; } = new();
    public AwsSettings AWS { get; set; } = new();
    public ApiKeySettings ApiKeySettings { get; set; } = new();
    public string CorsOrigin { get; set; } = null!;
}