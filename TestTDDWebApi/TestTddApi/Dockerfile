#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestTddApi/TestTddApi.csproj", "TestTddApi/"]
COPY ["../TestTddCore/TestTddCore.csproj", "../TestTddCore/"]
COPY ["../TestTddInfra/TestTddInfra.csproj", "../TestTddInfra/"]
RUN dotnet restore "TestTddApi/TestTddApi.csproj"
COPY . .
WORKDIR "/src/TestTddApi"
RUN dotnet build "TestTddApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTddApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTddApi.dll"]
