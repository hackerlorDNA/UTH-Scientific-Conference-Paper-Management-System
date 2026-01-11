# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["UTH-ConfMS-Backend/Services/Notification.Service/Notification.Service.csproj", "Services/Notification.Service/"]
COPY ["UTH-ConfMS-Backend/Shared/UTH.ConfMS.Shared/UTH.ConfMS.Shared.csproj", "Shared/UTH.ConfMS.Shared/"]
RUN dotnet restore "Services/Notification.Service/Notification.Service.csproj"

# Copy everything else and build
COPY UTH-ConfMS-Backend/ .
WORKDIR "/src/Services/Notification.Service"
RUN dotnet build "Notification.Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Notification.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5005

# Copy published app
COPY --from=publish /app/publish .

# Set environment
ENV ASPNETCORE_URLS=http://+:5005

ENTRYPOINT ["dotnet", "Notification.Service.dll"]
