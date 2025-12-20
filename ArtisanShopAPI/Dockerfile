FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["ArtisanShopAPI.csproj", "./"]
RUN dotnet restore "ArtisanShopAPI.csproj"

# Copy everything and build
COPY . .
RUN dotnet build "ArtisanShopAPI.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ArtisanShopAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080

# Copy published app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ArtisanShopAPI.dll"]