
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["src/Web/API/WebApi/API.csproj", "src/Web/API/WebApi/"]

RUN dotnet restore "src/Web/API/WebApi/API.csproj"

COPY . .

WORKDIR "/src/src/Web/API/WebApi"

RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
