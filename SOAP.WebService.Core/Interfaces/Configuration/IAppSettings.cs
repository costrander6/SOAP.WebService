using SOAP.WebService.Core.Configuration.Settings;

namespace SOAP.WebService.Core.Interfaces.Configuration;

public interface IAppSettings
{
    DatabaseSettings DatabaseSettings { get; set; }
    AwsSettings AWS { get; set; }
    ApiKeySettings ApiKeySettings { get; set; }
    string CorsOrigin { get; set; }
}