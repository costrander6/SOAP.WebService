using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOAP.WebService.API.Attributes;
using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Interfaces.Services;
using SOAP.WebService.Core.Mappers;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Controllers;

[ApiController]
[Route("workflow-run")]
public class WorkflowRunController(
    IWorkflowRunRepository workflowRunRepository,
    IWorkflowRunDetailsService workflowRunDetailsService) 
    : ControllerBase
{
    [ApiKeyAuth]
    [HttpPost]
    public async Task<ActionResult<WorkflowRunResponse>> CreateWorkflowRun(WorkflowRunCreateRequest workflowRunCreateRequest)
    {
        var owner = HttpContext.Items["owner"] as string;
        var workflowRunEntity = new WorkflowRun(workflowRunCreateRequest, owner!);

        await workflowRunRepository.Create(workflowRunEntity);
        
        return Created($"/workflow-run/id/{workflowRunEntity.Id}", WorkflowRunMapper.MapEntityToResponse(workflowRunEntity));
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult<WorkflowRunDetailsResponse>> GetMostRecentWorkflowRun([FromQuery] string repo, [FromQuery] string? branch)
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (sub is null)
        {
            Console.WriteLine("[WorkflowRunController.GetMostRecentWorkflowRun] sub not found in token. Returning 401");
            return Unauthorized();
        }
        
        if (string.IsNullOrWhiteSpace(branch)) branch = "main";

        var runDetails = await workflowRunDetailsService.GetMostRecentWorkflowRunDetails(sub, repo, branch);
        
        if (runDetails is null) return NotFound();

        return Ok(runDetails);

    }
}