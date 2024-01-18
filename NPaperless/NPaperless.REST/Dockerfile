#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy the code into the container
WORKDIR /src
COPY ["NPaperless.REST/NPaperless.REST.csproj", "NPaperless.REST/"]
COPY ["NPaperless.BusinessLogic/NPaperless.BusinessLogic.csproj", "NPaperless.BusinessLogic/"]
COPY ["NPaperless.BusinessLogic.Entities/NPaperless.BusinessLogic.Entities.csproj", "NPaperless.BusinessLogic.Entities/"]
COPY ["NPaperless.BusinessLogic.Interfaces/NPaperless.BusinessLogic.Interfaces.csproj", "NPaperless.BusinessLogic.Interfaces/"]
COPY ["NPaperless.BusinessLogic.Tests/NPaperless.BusinessLogic.Tests.csproj", "NPaperless.BusinessLogic.Tests/"]
COPY ["NPaperless.DataAccess.Entities/NPaperless.DataAccess.Entities.csproj", "NPaperless.DataAccess.Entities/"]
COPY ["NPaperless.DataAccess.Interfaces/NPaperless.DataAccess.Interfaces.csproj", "NPaperless.DataAccess.Interfaces/"]
COPY ["NPaperless.DataAccess.SQL/NPaperless.DataAccess.SQL.csproj", "NPaperless.DataAccess.SQL/"]
COPY ["NPaperless.DataAccess.Tests/NPaperless.DataAccess.Tests.csproj", "NPaperless.DataAccess.Tests/"]

# NuGet restore
RUN dotnet restore "NPaperless.REST/NPaperless.REST.csproj"
COPY ["NPaperless.REST/", "NPaperless.REST/"]
COPY ["NPaperless.BusinessLogic/", "NPaperless.BusinessLogic/"]
COPY ["NPaperless.BusinessLogic.Entities/", "NPaperless.BusinessLogic.Entities/"]
COPY ["NPaperless.BusinessLogic.Interfaces/", "NPaperless.BusinessLogic.Interfaces/"]
COPY ["NPaperless.BusinessLogic.Tests/", "NPaperless.BusinessLogic.Tests/"]
COPY ["NPaperless.DataAccess.Entities/", "NPaperless.DataAccess.Entities/"]
COPY ["NPaperless.DataAccess.Interfaces/", "NPaperless.DataAccess.Interfaces/"]
COPY ["NPaperless.DataAccess.SQL/", "NPaperless.DataAccess.SQL/"]
COPY ["NPaperless.DataAccess.Tests/", "NPaperless.DataAccess.Tests/"]


# Build the API
WORKDIR "NPaperless.REST"
RUN dotnet build "NPaperless.REST.csproj" -c Release -o /app/build

# Publish it
FROM build AS publish
RUN dotnet publish "NPaperless.REST.csproj" -c Release -o /app/publish

# Make the final image for publishing
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy TesseractOCR data into the container
COPY ["NPaperless.BusinessLogic/TesseractOCR/tessdata/deu.traineddata", "tessdata/"]
COPY ["NPaperless.BusinessLogic/TesseractOCR/tessdata/eng.traineddata", "tessdata/"]

# Set entrypoint
ENTRYPOINT ["dotnet", "NPaperless.REST.dll"]


