#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Calculadora_Worker.csproj", "."]
RUN dotnet restore "./Calculadora_Worker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Calculadora_Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calculadora_Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calculadora_Worker.dll"]