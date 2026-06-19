using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Core.Interfaces.Repositories;

public interface IFindingRepository
{
    Task<int> Create(Finding finding);
    Task<int> CreateBatch(IEnumerable<Finding> findings);
    Task<List<Finding>> GetAllFindingsForScan(Guid scanResultId);
}