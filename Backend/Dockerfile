# ---------- Estágio 1: build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1º só os .csproj → restore fica em cache enquanto as dependências não mudarem
COPY CalculadoraCustos.API/CalculadoraCustos.API.csproj CalculadoraCustos.API/
COPY CalculadoraCustos.Application/CalculadoraCustos.Application.csproj CalculadoraCustos.Application/
COPY CalculadoraCustos.Domain/CalculadoraCustos.Domain.csproj CalculadoraCustos.Domain/
COPY CalculadoraCustos.Infrastructure/CalculadoraCustos.Infrastructure.csproj CalculadoraCustos.Infrastructure/
RUN dotnet restore CalculadoraCustos.API/CalculadoraCustos.API.csproj

# 2º o resto do código
COPY . .
RUN dotnet publish CalculadoraCustos.API/CalculadoraCustos.API.csproj -c Release -o /app/publish --no-restore

# ---------- Estágio 2: runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "CalculadoraCustos.API.dll"]
