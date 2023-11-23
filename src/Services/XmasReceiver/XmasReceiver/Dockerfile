#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XmasReceiver/XmasReceiver.csproj", "XmasReceiver/"]
COPY ["XmasReceiver.Facade/XmasReceiver.Facade.csproj", "XmasReceiver.Facade/"]
COPY ["XmasReceiver.Infrastructures/XmasReceiver.Infrastructures.csproj", "XmasReceiver.Infrastructures/"]
COPY ["XmasReceiver.Domain/XmasReceiver.Domain.csproj", "XmasReceiver.Domain/"]
COPY ["XmasReceiver.Messages/XmasReceiver.Messages.csproj", "XmasReceiver.Messages/"]
COPY ["XmasReceiver.Shared/XmasReceiver.Shared.csproj", "XmasReceiver.Shared/"]
COPY ["XmasReceiver.ReadModel/XmasReceiver.ReadModel.csproj", "XmasReceiver.ReadModel/"]
RUN dotnet restore "./XmasReceiver/./XmasReceiver.csproj"
COPY . .
WORKDIR "/src/XmasReceiver"
RUN dotnet build "./XmasReceiver.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./XmasReceiver.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XmasReceiver.dll"]