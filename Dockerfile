ARG APP_NAME="FUT-23-LATEST-NEWS.API"

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /build
COPY ./src .
RUN dotnet restore && dotnet build
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0
ARG APP_NAME
ARG GITHUB_SHA
ARG GITHUB_BRANCH
ENV GITHUB_BRANCH $GITHUB_BRANCH
ENV GITHUB_SHA $GITHUB_SHA
WORKDIR /app
COPY --from=build-env /build/$APP_NAME/bin/Release/net7.0/publish /app
CMD ASPNETCORE_URLS=http://*:$PORT dotnet FUT-23-LATEST-NEWS.API.dll
