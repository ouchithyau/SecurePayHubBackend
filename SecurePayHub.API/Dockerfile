# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SecurePayHub.API/SecurePayHub.API.csproj", "SecurePayHub.API/"]
COPY ["SecurePayHub.Application/SecurePayHub.Application.csproj", "SecurePayHub.Application/"]
COPY ["SecurePayHub.Infrastructure/SecurePayHub.Infrastructure.csproj", "SecurePayHub.Infrastructure/"]
COPY ["SecurePayHub.Domain/SecurePayHub.Domain.csproj", "SecurePayHub.Domain/"]

RUN dotnet restore "SecurePayHub.API/SecurePayHub.API.csproj"

COPY . .
WORKDIR "/src/SecurePayHub.API"
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SecurePayHub.API.dll"]