FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | sh
ARG FEED_ACCESSTOKEN
ENV FEED_ENDPOINT "https://pkgs.dev.azure.com/ConsumerFinancing/ConsumerFinancing/_packaging/JifitiDotRabbitCore/nuget/v3/index.json"
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS \
    "{\"endpointCredentials\": [{\"endpoint\":\"${FEED_ENDPOINT}\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"

WORKDIR /src
COPY ["src/Area.Template.Api/Area.Template.Api.csproj", "src/Area.Template.Api/"]
COPY ["src/Area.Template.Infrastructure/Area.Template.Infrastructure.csproj", "src/Area.Template.Infrastructure/"]
COPY ["src/Area.Template.Application/Area.Template.Application.csproj", "src/Area.Template.Application/"]
COPY ["src/Area.Template.Domain/Area.Template.Domain.csproj", "src/Area.Template.Domain/"]

RUN dotnet nuget add source ${FEED_ENDPOINT} \
    --name JifitiDotRabbitCore
RUN dotnet restore "src/Area.Template.Api/Area.Template.Api.csproj"
COPY . .
WORKDIR "/src/src/Area.Template.Api"
RUN dotnet build "Area.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Area.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Area.Template.Api.dll"]
