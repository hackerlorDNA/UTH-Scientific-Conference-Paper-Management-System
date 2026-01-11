# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["UTH-ConfMS-Backend/ApiGateway/ApiGateway.csproj", "ApiGateway/"]
COPY ["UTH-ConfMS-Backend/Shared/UTH.ConfMS.Shared/UTH.ConfMS.Shared.csproj", "Shared/UTH.ConfMS.Shared/"]
RUN dotnet restore "ApiGateway/ApiGateway.csproj"

# Copy everything else and build
COPY UTH-ConfMS-Backend/ApiGateway/ ApiGateway/
COPY UTH-ConfMS-Backend/Shared/ Shared/
RUN dotnet build "ApiGateway/ApiGateway.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ApiGateway/ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5000

# Copy published app
COPY --from=publish /app/publish .

# Set environment (match docker-compose port 5000)
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "ApiGateway.dll"]
