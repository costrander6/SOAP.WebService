using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SOAP.WebService.Core.Interfaces.Configuration;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.API.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IAmazonCognitoIdentityProvider cognitoService,
    IAppSettings appSettings) 
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
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
        
        var response = await cognitoService.InitiateAuthAsync(authRequest);

        return Ok(new LoginResponse{ IdToken = response.AuthenticationResult.IdToken });
    }

    [Authorize]
    [HttpPost("api-key")]
    public IActionResult CreateApiKey()
    {
        var bytes = RandomNumberGenerator.GetBytes(appSettings.ApiKeySettings.KeyLength);

        var base64String = Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_");
    
        var keyLength = appSettings.ApiKeySettings.KeyLength - appSettings.ApiKeySettings.KeyPrefix.Length; 

        var apiKey = appSettings.ApiKeySettings.KeyPrefix + base64String[..keyLength];
        
        return Ok(new ApiKeyResponse{ ApiKey =  apiKey });
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