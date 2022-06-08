#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeveloperApi/DeveloperApi.csproj", "DeveloperApi/"]
RUN dotnet restore "DeveloperApi/DeveloperApi.csproj"
COPY . .
WORKDIR "/src/DeveloperApi"
RUN dotnet build "DeveloperApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeveloperApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveloperApi.dll"]