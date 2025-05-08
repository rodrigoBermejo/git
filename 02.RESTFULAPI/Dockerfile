# Use the official .NET Core SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore as distinct layers
COPY CRM.API.sln ./
COPY CRM.API/CRM.API.csproj CRM.API/
COPY CRM.Application/CRM.Application.csproj CRM.Application/
COPY CRM.Core/CRM.Core.csproj CRM.Core/
COPY CRM.Infrastructure/CRM.Infrastructure.csproj CRM.Infrastructure/

# Copy everything else and build
COPY . .
WORKDIR /src/CRM.API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CRM.API.dll"]