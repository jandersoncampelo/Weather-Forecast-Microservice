#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Wheater.Microservice2/Wheater.Microservice2.csproj", "Wheater.Microservice2/"]
RUN dotnet restore "Wheater.Microservice2/Wheater.Microservice2.csproj"
COPY . .
WORKDIR "/src/Wheater.Microservice2"
RUN dotnet build "Wheater.Microservice2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wheater.Microservice2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wheater.Microservice2.dll"]