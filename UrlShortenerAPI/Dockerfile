#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["UrlShortenerAPI/UrlShortenerAPI.csproj", "UrlShortenerAPI/"]
#RUN dotnet restore "UrlShortenerAPI/UrlShortenerAPI.csproj"
#COPY . .
#WORKDIR "/src/UrlShortenerAPI"
#RUN dotnet build "UrlShortenerAPI.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "UrlShortenerAPI.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "UrlShortenerAPI.dll"]

# multistage

# FROM=imagem de origem AS=alias
# workdir=
# RUN=comando a ser executado. RUN<command> ou RUN["executable", "param1", "param2"]
# COPY=copia arquivos ou diretórios da origem para o caminho destino no filesystem do container
# COPY . . = copy everything from the same place as the dockerfile, to “where i am now” in the container.
# The “where i am now” in the image/container is defined by https://docs.docker.com/engine/reference/builder/#workdir 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY UrlShortenerAPI.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /api

# Criar o diretório app e depois copia o conteúdo de build/app para o diretório /app da imagem
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /api
COPY --from=build /api .
ENTRYPOINT ["dotnet", "UrlShortenerAPI.dll"]