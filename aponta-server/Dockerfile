#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["aponta-server.csproj", "./"]
RUN dotnet restore "aponta-server.csproj"
COPY . .
WORKDIR "/src/aponta-server"
RUN dotnet build "../aponta-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "../aponta-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "aponta-server.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet aponta-server.dll