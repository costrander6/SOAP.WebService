using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Infrastructure.Database;

namespace SOAP.WebService.Infrastructure.Repositories;

public class ScanResultRepository(SoapDbContext dbContext) : IScanResultRepository
{
    public Task<int> Create(ScanResult scanResult)
    {
        dbContext.ScanResults.Add(scanResult);
        return dbContext.SaveChangesAsync();
    }
}