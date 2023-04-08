#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DC.AnimalChipization.WebApi/DC.AnimalChipization.WebApi.csproj", "DC.AnimalChipization.WebApi/"]
RUN dotnet restore "DC.AnimalChipization.WebApi/DC.AnimalChipization.WebApi.csproj"
COPY . .
WORKDIR "/src/DC.AnimalChipization.WebApi"
RUN dotnet build "DC.AnimalChipization.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DC.AnimalChipization.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DC.AnimalChipization.WebApi.dll"]