# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["UTH-ConfMS-Backend/Services/Conference.Service/Conference.Service.csproj", "Services/Conference.Service/"]
COPY ["UTH-ConfMS-Backend/Shared/UTH.ConfMS.Shared/UTH.ConfMS.Shared.csproj", "Shared/UTH.ConfMS.Shared/"]
RUN dotnet restore "Services/Conference.Service/Conference.Service.csproj"

# Copy everything else and build
COPY UTH-ConfMS-Backend/ .
WORKDIR "/src/Services/Conference.Service"
RUN dotnet build "Conference.Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Conference.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5002

# Copy published app
COPY --from=publish /app/publish .

# Set environment
ENV ASPNETCORE_URLS=http://+:5002

ENTRYPOINT ["dotnet", "Conference.Service.dll"]
