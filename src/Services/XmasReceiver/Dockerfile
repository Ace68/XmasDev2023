FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "XmasReceiver/XmasReceiver.csproj"
COPY . .
WORKDIR "/src/XmasReceiver"
RUN dotnet build "XmasReceiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XmasReceiver.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XmasReceiver.dll"]