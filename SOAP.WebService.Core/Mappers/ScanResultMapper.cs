using SOAP.WebService.Core.Entities;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Mappers;

public static class ScanResultMapper
{
    public static ScanResultResponse MapScanResultAndFindingsToResponse(ScanResult scanResult, IEnumerable<Finding> findings)
    {
        return new ScanResultResponse
        {
            WorkflowRunId = scanResult.WorkflowRunId,
            Scanner = scanResult.Scanner,
            Findings = FindingMapper.MapFindingEntitiesToResponses(findings),
            CreatedAt = scanResult.CreatedAt
        };
    }

    public static ScanResultResponse MapScanResultToResponse(ScanResult scanResult)
    {
        return new ScanResultResponse
        {
            WorkflowRunId = scanResult.WorkflowRunId,
            Scanner = scanResult.Scanner,
            CreatedAt = scanResult.CreatedAt
        };
    }
}