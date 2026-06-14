using SOAP.WebService.Core.Configuration;
using SOAP.WebService.Core.Configuration.Settings;

namespace SOAP.WebService.Core.Interfaces.Configuration;

public interface IAppSettings
{
    public DatabaseSettings DatabaseSettings { get; set; }
}