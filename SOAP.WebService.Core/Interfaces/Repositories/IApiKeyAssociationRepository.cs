using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IApiKeyAssociationRepository
{
    Task<int> Create(ApiKeyAssociation apiKeyAssociation);
    Task<ApiKeyAssociation?> GetActiveKeyByOwner(string owner);
    Task<ApiKeyAssociation?> GetActiveKeyByHash(string key);
    Task<int> Update(ApiKeyAssociation apiKeyAssociation);
}