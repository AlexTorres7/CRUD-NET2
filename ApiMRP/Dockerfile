# ETAPA 1: Compilación (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ApiMRP/src/API/ApiMRP.csproj", "src/API/"]
RUN dotnet restore "ApiMRP/src/API/ApiMRP.csproj"

COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "ApiMRP.csproj" -c Release -o /app/build


# ETAPA 2: Publicación (Publish)
FROM build AS publish
RUN dotnet publish "ApiMRP.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 3: Imagen Final de Producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiMRP.dll"]