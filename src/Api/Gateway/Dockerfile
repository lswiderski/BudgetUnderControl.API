FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Api/Gateway/BUC.Gateway/BUC.Gateway.csproj", "BUC.Gateway/"]
RUN dotnet restore "src/Api/Gateway/BUC.Gateway/BUC.Gateway.csproj"
COPY . .
WORKDIR "/src/BUC.Gateway"
RUN dotnet build "BUC.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BUC.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BUC.Gateway.dll"]
