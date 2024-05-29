#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LML.NPOManagement/LML.NPOManagement.csproj", "LML.NPOManagement/"]
COPY ["LML.NPOManagement.Bll/LML.NPOManagement.Bll.csproj", "LML.NPOManagement.Bll/"]
COPY ["LML.NPOManagement.Common/LML.NPOManagement.Common.csproj", "LML.NPOManagement.Common/"]
COPY ["LML.NPOManagement.Dal/LML.NPOManagement.Dal.csproj", "LML.NPOManagement.Dal/"]
RUN dotnet restore "./LML.NPOManagement/./LML.NPOManagement.csproj"
COPY . .
WORKDIR "/src/LML.NPOManagement"
RUN dotnet build "./LML.NPOManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LML.NPOManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LML.NPOManagement.dll"]