using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Interfaces.Services;
using SOAP.WebService.Core.Mappers;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Services;

public class WorkflowRunDetailsService(
    IWorkflowRunRepository workflowRunRepository,
    IScanResultRepository scanResultRepository,
    IFindingRepository findingRepository) 
    : IWorkflowRunDetailsService
{
    public async Task<WorkflowRunDetailsResponse?> GetMostRecentWorkflowRunDetails(string owner, string repo, string branch)
    {
        Console.WriteLine($"[WorkflowRunDetailsService.GetMostRecentWorkflowRunDetails] Querying for {owner} - {repo} - {branch}");
        var mostRecentWorkflow = await workflowRunRepository.GetMostRecent(owner, repo, branch);
        if (mostRecentWorkflow is null) return null;
        
        var workflowRunDetails = new WorkflowRunDetailsResponse
        {
            Id = mostRecentWorkflow.Id,
            Repo =  repo,
            Branch = branch,
            Commit =  mostRecentWorkflow.Commit,
            CreatedAt =  mostRecentWorkflow.CreatedAt,
        };
        
        var workflowScanResults = await scanResultRepository.GetWorkflowScanResults(mostRecentWorkflow.Id);
        if (workflowScanResults.Count == 0) return workflowRunDetails;

        foreach (var scanResult in workflowScanResults)
        {
            var findings = await findingRepository.GetAllFindingsForScan(scanResult.Id);
            if (findings.Count == 0)
            {
                var emptyFindingScan = ScanResultMapper.MapScanResultToResponse(scanResult);
                workflowRunDetails.Scans = workflowRunDetails.Scans.Append(emptyFindingScan);
                continue;
            };
            
            var scanResultResponse = ScanResultMapper.MapScanResultAndFindingsToResponse(scanResult, findings);
            workflowRunDetails.Scans = workflowRunDetails.Scans.Append(scanResultResponse);
        }

        return workflowRunDetails;
    }
}