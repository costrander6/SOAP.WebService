using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IScanResultRepository
{
    Task<int> Create(ScanResult scanResult);
}