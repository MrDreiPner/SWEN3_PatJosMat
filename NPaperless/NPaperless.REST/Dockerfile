# Container we use for final publish
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

ENV ConnectionStrings__PostgreSQLConnection "Host=NPaperless-db;Database=NPaperless;Username=NPaperless;Password=NPaperless"

COPY *.sln .
COPY NPaperless.REST/*.csproj ./NPaperless.REST/
COPY NPaperless.BusinessLogic.Interfaces/*.csproj ./NPaperless.BusinessLogic.Interfaces/
COPY NPaperless.BusinessLogic.Entities/*.csproj ./NPaperless.BusinessLogic.Entities/
COPY NPaperless.BusinessLogic.Tests/*.csproj ./NPaperless.BusinessLogic.Tests/
COPY NPaperless.BusinessLogic/*.csproj ./NPaperless.BusinessLogic/

RUN dotnet restore

COPY NPaperless.REST/. ./NPaperless.REST/
COPY NPaperless.BusinessLogic.Interfaces/. ./NPaperless.BusinessLogic.Interfaces/
COPY NPaperless.BusinessLogic.Entities/. ./NPaperless.BusinessLogic.Entities/
COPY NPaperless.BusinessLogic.Tests/. ./NPaperless.BusinessLogic.Tests/
COPY NPaperless.BusinessLogic/. ./NPaperless.BusinessLogic/

WORKDIR /app/NPaperless.REST
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app

COPY --from=build /app/NPaperless.REST/out ./
#COPY entrypoint.sh .

ENTRYPOINT ["dotnet", "NPaperless.REST.dll"]