using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IApiKeyAssociationRepository
{
    Task<int> Create(ApiKeyAssociation apiKeyAssociation);
    Task<ApiKeyAssociation?> GetActiveKeyByOwner(string owner);
    Task<int> Update(ApiKeyAssociation apiKeyAssociation);
}