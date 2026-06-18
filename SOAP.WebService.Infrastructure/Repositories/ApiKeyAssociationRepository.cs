using Microsoft.EntityFrameworkCore;
using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Infrastructure.Database;

namespace SOAP.WebService.Infrastructure.Repositories;

public class ApiKeyAssociationRepository(SoapDbContext dbContext) : IApiKeyAssociationRepository
{
    public Task<int> Create(ApiKeyAssociation apiKeyAssociation)
    {
        dbContext.ApiKeyAssociations.Add(apiKeyAssociation);
        return dbContext.SaveChangesAsync();
    }

    public Task<ApiKeyAssociation?> GetActiveKeyByOwner(string owner)
    {
        return dbContext.ApiKeyAssociations.FirstOrDefaultAsync(
            association => association.Owner == owner && association.RevokedAt == null);
    }

    public Task<ApiKeyAssociation?> GetActiveKeyByHash(string hash)
    {
        return dbContext.ApiKeyAssociations.FirstOrDefaultAsync(
            association => association.KeyHash == hash && association.RevokedAt == null);
    }

    public Task<int> Update(ApiKeyAssociation apiKeyAssociation)
    {
        dbContext.ApiKeyAssociations.Update(apiKeyAssociation);
        return dbContext.SaveChangesAsync();
    }
}