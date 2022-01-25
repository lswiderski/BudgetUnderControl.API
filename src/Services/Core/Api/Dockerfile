FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build BudgetUnderControl.API/BudgetUnderControl.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish BudgetUnderControl.API/BudgetUnderControl.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT Docker
ENTRYPOINT dotnet BudgetUnderControl.API.dll

