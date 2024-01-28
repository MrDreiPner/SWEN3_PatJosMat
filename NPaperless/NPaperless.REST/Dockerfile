#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Container we use for final publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install libs for Tesseract & ImageMagick
RUN apt-get update
RUN apt-get install -y git cmake build-essential ghostscript
RUN mkdir leptonica
RUN git clone https://github.com/DanBloomberg/leptonica.git /leptonica

WORKDIR /leptonica
RUN mkdir build
WORKDIR /leptonica/build
RUN cmake ..

RUN apt-get install -y libleptonica-dev libtesseract-dev

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
# Link libs for Tesseract
WORKDIR /app/x64
RUN ln -s /usr/lib/x86_64-linux-gnu/liblept.so.5 libleptonica-1.80.0.so
RUN ln -s /usr/lib/x86_64-linux-gnu/libtesseract.so.4.0.1 libtesseract41.so

WORKDIR /app
COPY --from=publish /app/publish .

# Copy TesseractOCR data into the container
COPY ["NPaperless.BusinessLogic/TesseractOCR/tessdata/deu.traineddata", "tessdata/"]
COPY ["NPaperless.BusinessLogic/TesseractOCR/tessdata/eng.traineddata", "tessdata/"]

# some basic dependencies
RUN apt-get update -y && apt-get install -y libc6-dev libleptonica-dev

# needed to get tesseract 5 
RUN apt-get update -y && apt-get install -y wget gnupg2 apt-transport-https software-properties-common
RUN tee /etc/apt/sources.list.d/notesalexp.list<<EOF
RUN echo "deb https://notesalexp.org/tesseract-ocr5/$(lsb_release -cs)/ $(lsb_release -cs) main" | tee /etc/apt/sources.list.d/notesalexp.list > /dev/null
RUN wget -O - https://notesalexp.org/debian/alexp_key.asc | apt-key add - 

# tesseract
RUN apt-get update -y && apt-get install -y tesseract-ocr

# ghostscript
RUN apt-get update -y && apt-get install -y ghostscript libgs-dev

# get architecture
ARG TARGETPLATFORM
RUN if [ "$TARGETPLATFORM" = "linux/amd64" ]; then \
        export ARCHITECTURE="x86_64"; \
        export CPU_BIT="64"; \
    elif [ "$TARGETPLATFORM" = "linux/arm/v7" ]; then \
        export ARCHITECTURE="arm"; \
        export CPU_BIT="32"; \
    elif [ "$TARGETPLATFORM" = "linux/arm64" ]; then \
        ARCHITECTURE="aarch64"; \
    else \
        export ARCHITECTURE=x86; \
        export CPU_BIT=32; \
    fi

# needed for macos & linux arm64 (because .NET is doing Microsoft's Only-Windows-Policy again)
RUN if [ "$CPU_BIT" != "64" ]; then \
        if [ "$ARCHITECTURE" != "x86_64" ]; then \
            ln -s /usr/lib/aarch64-linux-gnu /usr/lib/x86_64-linux-gnu; \
        fi \
    fi


# Create a symbolic link inside /App/x64/
RUN ln -s /usr/lib/x86_64-linux-gnu/libleptonica.so /app/x64/libleptonica-1.82.0.so
RUN ln -s /usr/lib/x86_64-linux-gnu/libtesseract.so.5 /app/x64/libtesseract50.so

# some libs are needed
RUN ln -s /usr/lib/x86_64-linux-gnu/* /app/x64/

# Set entrypoint
ENTRYPOINT ["dotnet", "NPaperless.REST.dll"]



