FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG TARGETARCH
WORKDIR /source

COPY ./* .
RUN dotnet restore ./Domain.csproj
RUN dotnet restore ./Application.csproj
RUN dotnet restore ./TestTask.Persistence.csproj

COPY ./WebApi/WebApi.csproj .
RUN dotnet restore ./WebApi.csproj

COPY ./* .
RUN dotnet publish TestTask.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY --from=build /app .
USER $APP_UID

ENTRYPOINT ["dotnet", "TestTask.dll"]
