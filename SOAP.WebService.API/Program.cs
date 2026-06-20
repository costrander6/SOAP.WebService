using System.Text.Json.Serialization;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOAP.WebService.API.Presentation.Services;
using SOAP.WebService.Core.Configuration.Settings;
using SOAP.WebService.Core.Interfaces.Configuration;
using SOAP.WebService.Core.Interfaces.Repositories;
using SOAP.WebService.Core.Interfaces.Services;
using SOAP.WebService.Infrastructure.Database;
using SOAP.WebService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
    builder.Configuration.AddSystemsManager("/soap/prod");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddSingleton<IAppSettings>(sp =>
    sp.GetRequiredService<IOptions<AppSettings>>().Value);

var appSettings = builder.Configuration.Get<AppSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://cognito-idp.{appSettings!.AWS.Region}.amazonaws.com/{appSettings.AWS.UserPoolId}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = $"https://cognito-idp.{appSettings.AWS.Region}.amazonaws.com/{appSettings.AWS.UserPoolId}",
            ValidateAudience = true,
            ValidAudience = appSettings.AWS.UserPoolClientId,
            ValidateLifetime = true,
        };
    });

var awsConfig = new AmazonCognitoIdentityProviderConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(appSettings!.AWS.Region) };
builder.Services.AddSingleton<IAmazonCognitoIdentityProvider>(new AmazonCognitoIdentityProviderClient(awsConfig));

builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IApiKeyAssociationRepository, ApiKeyAssociationRepository>();
builder.Services.AddScoped<IWorkflowRunRepository, WorkflowRunRepository>();
builder.Services.AddScoped<IScanResultRepository, ScanResultRepository>();
builder.Services.AddScoped<IFindingRepository, FindingRepository>();
builder.Services.AddScoped<IWorkflowRunDetailsService, WorkflowRunDetailsService>();

var databaseSettings = appSettings.DatabaseSettings;
var connectionString = $"Host={databaseSettings.Url};Port={databaseSettings.Port};Database={databaseSettings.DatabaseName};Username={databaseSettings.Username};Password={databaseSettings.Password}";

builder.Services.AddDbContextPool<SoapDbContext>(opt => 
    opt.UseNpgsql(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();