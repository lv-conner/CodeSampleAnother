FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CodeSampleDockerApp/CodeSampleDockerApp.csproj", "CodeSampleDockerApp/"]
RUN dotnet restore "CodeSampleDockerApp/CodeSampleDockerApp.csproj"
COPY . .
WORKDIR "/src/CodeSampleDockerApp"
RUN dotnet build "CodeSampleDockerApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CodeSampleDockerApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CodeSampleDockerApp.dll"]