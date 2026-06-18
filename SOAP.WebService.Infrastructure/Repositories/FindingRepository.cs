using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Infrastructure.Database;

namespace SOAP.WebService.Infrastructure.Repositories;

public class FindingRepository(SoapDbContext dbContext) : IFindingRepository
{
    public Task<int> Create(Finding finding)
    {
        dbContext.Findings.Add(finding);
        return dbContext.SaveChangesAsync();
    }

    public Task<int> CreateBatch(IEnumerable<Finding> findings)
    {
        dbContext.Findings.AddRange(findings);
        return dbContext.SaveChangesAsync();
    }
}