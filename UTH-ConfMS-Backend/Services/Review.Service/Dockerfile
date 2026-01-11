# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["UTH-ConfMS-Backend/Services/Review.Service/Review.Service.csproj", "Services/Review.Service/"]
COPY ["UTH-ConfMS-Backend/Shared/UTH.ConfMS.Shared/UTH.ConfMS.Shared.csproj", "Shared/UTH.ConfMS.Shared/"]
RUN dotnet restore "Services/Review.Service/Review.Service.csproj"

# Copy everything else and build
COPY UTH-ConfMS-Backend/ .
WORKDIR "/src/Services/Review.Service"
RUN dotnet build "Review.Service.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Review.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5004

# Copy published app
COPY --from=publish /app/publish .

# Set environment
ENV ASPNETCORE_URLS=http://+:5004

ENTRYPOINT ["dotnet", "Review.Service.dll"]
