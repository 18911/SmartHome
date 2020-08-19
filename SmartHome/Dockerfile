#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm32v7 AS build
WORKDIR /src
COPY ["SmartHome/SmartHome.csproj", "SmartHome/"]
RUN dotnet restore "SmartHome/SmartHome.csproj"
COPY . .
WORKDIR "/src/SmartHome"
RUN dotnet build "SmartHome.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartHome.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartHome.dll"]
