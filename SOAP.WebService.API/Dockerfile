FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SOAP.WebService.API/SOAP.WebService.API.csproj", "SOAP.WebService.API/"]
COPY ["SOAP.WebService.Core/SOAP.WebService.Core.csproj", "SOAP.WebService.Core/"]
COPY ["SOAP.WebService.Infrastructure/SOAP.WebService.Infrastructure.csproj", "SOAP.WebService.Infrastructure/"]
COPY ["SOAP.WebService.Models/SOAP.WebService.Models.csproj", "SOAP.WebService.Models/"]
RUN dotnet restore "SOAP.WebService.API/SOAP.WebService.API.csproj"
COPY . .
WORKDIR "/src/SOAP.WebService.API"
RUN dotnet build "./SOAP.WebService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SOAP.WebService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SOAP.WebService.API.dll"]
