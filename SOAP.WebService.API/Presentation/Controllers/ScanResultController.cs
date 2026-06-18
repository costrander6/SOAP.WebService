using Microsoft.AspNetCore.Mvc;
using SOAP.WebService.API.Attributes;
using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Mappers;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Controllers;

[ApiController]
[Route("scan-result")]
public class ScanResultController(
    IWorkflowRunRepository workflowRunRepository,
    IScanResultRepository scanResultRepository, 
    IFindingRepository findingRepository) 
    : ControllerBase
{
    [ApiKeyAuth]
    [HttpPost]
    public async Task<ActionResult<ScanResultReponse>> CreateScanResult(ScanResultCreateRequest scanResultCreateRequest)
    {
        var workflowRun = await workflowRunRepository.Get(scanResultCreateRequest.WorkflowRunId);
        if (workflowRun is null) return NotFound();

        var scanResult = new ScanResult(scanResultCreateRequest);
        var findings = FindingMapper.MapFindingRequestsToEntities(scanResultCreateRequest.Findings, scanResult.Id).ToList();

        await scanResultRepository.Create(scanResult);
        await findingRepository.CreateBatch(findings);
        
        return Created($"/scan-result/id/{scanResult.Id}", ScanResultMapper.MapScanResultAndFindingsToResponse(scanResult, findings));
    }
}