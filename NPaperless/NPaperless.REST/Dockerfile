#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NPaperless.REST/NPaperless.REST.csproj", "NPaperless.REST/"]
RUN dotnet restore "NPaperless.REST/NPaperless.REST.csproj"
COPY . .
WORKDIR "/src/NPaperless.REST"
RUN dotnet build "NPaperless.REST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NPaperless.REST.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NPaperless.REST.dll"]