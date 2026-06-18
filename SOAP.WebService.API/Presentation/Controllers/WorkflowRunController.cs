using Microsoft.AspNetCore.Mvc;
using SOAP.WebService.API.Attributes;
using SOAP.WebService.Core.Entities;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Mappers;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Controllers;

[ApiController]
[Route("workflow-run")]
public class WorkflowRunController(IWorkflowRunRepository workflowRunRepository) : ControllerBase
{
    [ApiKeyAuth]
    [HttpPost]
    public async Task<ActionResult<WorkflowRunResponse>> CreateWorkflowRun(WorkflowRunCreateRequest workflowRunCreateRequest)
    {
        var owner = HttpContext.Items["owner"] as string;
        var workflowRunEntity = new WorkflowRun(workflowRunCreateRequest, owner!);

        await workflowRunRepository.Create(workflowRunEntity);
        
        return Created($"/workflow-run/{workflowRunEntity.Id}", WorkflowRunResponseMapper.MapEntityToResponse(workflowRunEntity));
    }
}