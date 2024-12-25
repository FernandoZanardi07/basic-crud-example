FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY WebApi.csproj WebApi/
WORKDIR /app/WebApi
RUN dotnet restore

COPY . . 
WORKDIR /app/WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/WebApi/out ./

COPY certs/aspnetapp.pfx /https/aspnetapp.pfx

ENV ASPNETCORE_ENVIRONMENT=Docker

ENTRYPOINT ["dotnet", "WebApi.dll"]