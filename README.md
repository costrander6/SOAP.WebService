# SOAP.WebService

## Development

### Database Migrations

- Use EF Core for database migrations
- Example
  ```shell
  cd SOAP.WebService.Infrastructure
  dotnet ef migrations add <migration-name> --output-dir Migrations --startup-project ../SOAP.WebService.API
  dotnet ef migrations script --idempotent --output ../initdb/init.sql --startup-project ../SOAP.WebService.API
  ```
  
- To remove the most recent migration
```shell
cd SOAP.WebService.Infrastructure
dotnet ef migrations remove --startup-project ../SOAP.WebService.API --force
```