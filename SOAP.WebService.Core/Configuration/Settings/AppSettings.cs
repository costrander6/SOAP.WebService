using SOAP.WebService.Core.Interfaces.Configuration;

namespace SOAP.WebService.Core.Configuration.Settings;

public class AppSettings : IAppSettings
{
    public DatabaseSettings DatabaseSettings { get; set; } = new();
}