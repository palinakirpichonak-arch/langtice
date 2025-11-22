FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7777
EXPOSE 7778

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationService/NotificationService.PL/NotificationService.PL.csproj", "NotificationService/NotificationService.PL/"]
RUN dotnet restore "NotificationService/NotificationService.PL/NotificationService.PL.csproj"
COPY . .
WORKDIR "/src/NotificationService/NotificationService.PL"
RUN dotnet build "./NotificationService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationService.PL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.PL.dll"]
