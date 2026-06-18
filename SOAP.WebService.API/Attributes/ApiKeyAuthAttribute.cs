using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SOAP.WebService.Core.Interfaces.Repositories;

namespace SOAP.WebService.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var rawKey))
        {
            Console.WriteLine("[ApiKeyAuth] Api key header not found");
            context.Result = new UnauthorizedResult();
            return;
        }
        
        var keyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawKey!)));
        var repository = context.HttpContext.RequestServices.GetRequiredService<IApiKeyAssociationRepository>();
        var apiKey = await repository.GetActiveKeyByHash(keyHash);
        
        if (apiKey is null)
        {
            Console.WriteLine($"[ApiKeyAuth] No matching Api key hash was found for {keyHash}");
            context.Result = new UnauthorizedResult();
            return;
        }
        
        context.HttpContext.Items["owner"] = apiKey.Owner;

        await next();
    }
}