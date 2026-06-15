using Microsoft.EntityFrameworkCore;
using SOAP.WebService.Core.Entities;

namespace SOAP.WebService.Infrastructure.Database;

public class SoapDbContext(DbContextOptions<SoapDbContext> options) : DbContext(options)
{
    public DbSet<WorkflowRun> WorkflowRuns { get; set; }
    public DbSet<ScanResult> ScanResults { get; set; }
    public DbSet<Finding> Findings { get; set; }
    public DbSet<ApiKeyAssociation> ApiKeyAssociations { get; set; }
}