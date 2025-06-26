# Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8082
EXPOSE 8083

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["API/API.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restaura as dependências
RUN dotnet restore "API/API.csproj"

# Copia o restante dos arquivos
COPY . .

# Define o diretório de trabalho para o projeto principal
WORKDIR "/src/API"

# Publica o projeto
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]