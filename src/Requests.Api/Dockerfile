#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Requests.Api/Requests.Api.csproj", "src/Requests.Api/"]
COPY ["src/Requests.Infrastructure/Requests.Infrastructure.csproj", "src/Requests.Infrastructure/"]
COPY ["src/Requests.Domain/Requests.Domain.csproj", "src/Requests.Domain/"]
COPY ["src/Requests.Application/Requests.Application.csproj", "src/Requests.Application/"]
RUN dotnet restore "src/Requests.Api/Requests.Api.csproj"
COPY . .
WORKDIR "/src/src/Requests.Api"
RUN dotnet build "Requests.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Requests.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Requests.Api.dll"]