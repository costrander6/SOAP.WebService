using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOAP.WebService.Core.Interfaces.Configuration;
using SOAP.WebService.Core.Interfaces.Services;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(
    IAmazonCognitoIdentityProvider cognitoService,
    IApiKeyService apiKeyService,
    IAppSettings appSettings) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
    {
        var authParameters = new Dictionary<string, string>
        {
            { "USERNAME", loginRequest.Username },
            { "PASSWORD", loginRequest.Password },
            { "SECRET_HASH", ComputeSecretHash(
                loginRequest.Username,
                appSettings.AWS.UserPoolClientId,
                appSettings.AWS.UserPoolClientSecret) }
        };
        
        var authRequest = new InitiateAuthRequest

        {
            ClientId = appSettings.AWS.UserPoolClientId,
            AuthParameters = authParameters,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        };

        InitiateAuthResponse response;

        try
        {
            response = await cognitoService.InitiateAuthAsync(authRequest);
        }
        catch (NotAuthorizedException)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponse{ IdToken = response.AuthenticationResult.IdToken });
    }

    [Authorize]
    [HttpPost("api-key")]
    public async Task<ActionResult<ApiKeyResponse>> CreateApiKey()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (sub is null)
        {
            Console.WriteLine("[AuthController.CreateApiKey] sub not found in token. Returning 401");
            return Unauthorized();
        }
        
        var apiKeyResponse = await apiKeyService.CreateApiKey(sub);
        return Ok(apiKeyResponse);
    }
    
    private static string ComputeSecretHash(string username, string clientId, string clientSecret)
    {
        var message = username + clientId;
        var key = Encoding.UTF8.GetBytes(clientSecret);
        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return Convert.ToBase64String(hash);
    }
}