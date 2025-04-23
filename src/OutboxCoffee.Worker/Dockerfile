FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "OutboxCoffee.Worker/OutboxCoffee.Worker.csproj"
RUN dotnet publish "OutboxCoffee.Worker/OutboxCoffee.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OutboxCoffee.Worker.dll"]
