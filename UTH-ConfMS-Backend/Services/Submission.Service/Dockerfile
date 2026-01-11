# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["UTH-ConfMS-Backend/Services/Submission.Service/Submission.Service.csproj", "Services/Submission.Service/"]
COPY ["UTH-ConfMS-Backend/Shared/UTH.ConfMS.Shared/UTH.ConfMS.Shared.csproj", "Shared/UTH.ConfMS.Shared/"]
RUN dotnet restore "Services/Submission.Service/Submission.Service.csproj"

# Copy everything else and build
COPY UTH-ConfMS-Backend/ .
WORKDIR "/src/Services/Submission.Service"
RUN dotnet build "Submission.Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Submission.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5003

# Create uploads directory
RUN mkdir -p /app/uploads

# Copy published app
COPY --from=publish /app/publish .

# Set environment
ENV ASPNETCORE_URLS=http://+:5003

ENTRYPOINT ["dotnet", "Submission.Service.dll"]
