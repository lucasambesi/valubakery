# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar los .csproj de todos los proyectos
COPY ValuBakery.Web/*.csproj ValuBakery.Web/
COPY ValuBakery.Bootstrap/*.csproj ValuBakery.Bootstrap/
COPY ValuBakery.Data/*.csproj ValuBakery.Data/
COPY ValuBakery.Percistence/*.csproj ValuBakery.Percistence/
COPY ValuBakery.Services/*.csproj ValuBakery.Services/
COPY ValuBakery.Shared/*.csproj ValuBakery.Shared/
COPY ValuBackery.sln .

# Restaurar dependencias
RUN dotnet restore ./ValuBakery.Web/ValuBakery.Web.csproj

# Copiar todo el código
COPY . .

# Compilar en modo Release
WORKDIR /src/ValuBakery.Web
RUN dotnet publish ValuBakery.Web.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ValuBakery.Web.dll"]
