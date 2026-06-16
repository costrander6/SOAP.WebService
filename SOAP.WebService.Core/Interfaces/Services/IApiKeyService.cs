using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Interfaces.Services;

public interface IApiKeyService
{
    Task<ApiKeyResponse> CreateApiKey(string owner);
}