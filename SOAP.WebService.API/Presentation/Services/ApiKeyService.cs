using System.Security.Cryptography;
using System.Text;
using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Configuration;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Interfaces.Services;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Services;

public class ApiKeyService(
    IApiKeyAssociationRepository apiKeyAssociationRepository,
    IAppSettings appSettings) 
    : IApiKeyService
{
    public async Task<ApiKeyResponse> CreateApiKey(string owner)
    {
        var activeApiKey = await apiKeyAssociationRepository.GetActiveKeyByOwner(owner);

        if (activeApiKey is not null)
            await RevokeApiKey(activeApiKey);
        
        var bytes = RandomNumberGenerator.GetBytes(appSettings.ApiKeySettings.KeyLength);

        var base64String = Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_");
    
        var keyLength = appSettings.ApiKeySettings.KeyLength - appSettings.ApiKeySettings.KeyPrefix.Length; 
        var apiKey = appSettings.ApiKeySettings.KeyPrefix + base64String[..keyLength];

        var apiKeyAssociation = new ApiKeyAssociation
        {
            Id = Guid.NewGuid(),
            KeyHash =  Sha256Hash(apiKey),
            Owner = owner,
            CreatedAt =  DateTime.UtcNow,
        };

        await apiKeyAssociationRepository.Create(apiKeyAssociation);

        return new ApiKeyResponse { ApiKey = apiKey };
    }

    private async Task<int> RevokeApiKey(ApiKeyAssociation apiKeyAssociation)
    {
        apiKeyAssociation.RevokedAt = DateTime.UtcNow;
        return await apiKeyAssociationRepository.Update(apiKeyAssociation);
    }

    private string Sha256Hash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = SHA256.HashData(inputBytes);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}