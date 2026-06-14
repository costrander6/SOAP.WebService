using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SOAP.WebService.Core.Configuration.Settings;
using SOAP.WebService.Core.Interfaces.Configuration;
using SOAP.WebService.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddSingleton<IAppSettings>(sp =>
    sp.GetRequiredService<IOptions<AppSettings>>().Value);

var appSettings = builder.Configuration.Get<AppSettings>();

var databaseSettings = appSettings.DatabaseSettings;
var connectionString = $"Host={databaseSettings.Url};Port={databaseSettings.Port};Database={databaseSettings.DatabaseName};Username={databaseSettings.Username};Password={databaseSettings.Password}";

builder.Services.AddDbContextPool<SoapDbContext>(opt => 
    opt.UseNpgsql(connectionString));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();