using SOAP.WebService.Core.Entities;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Mappers;

public static class ScanResultMapper
{
    public static ScanResultReponse MapScanResultAndFindingsToResponse(ScanResult scanResult, IEnumerable<Finding> findings)
    {
        return new ScanResultReponse
        {
            WorkflowRunId = scanResult.WorkflowRunId,
            Scanner = scanResult.Scanner,
            Findings = FindingMapper.MapFindingEntitiesToResponses(findings),
            Timestamp = scanResult.Timestamp,
            CreatedAt = scanResult.CreatedAt,
        };
    }
}