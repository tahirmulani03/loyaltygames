#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EDG.LoyaltyGames.APIS/EDG.LoyaltyGames.APIS.csproj", "EDG.LoyaltyGames.APIS/"]
COPY ["EDG.LoyaltyGames.Core/EDG.LoyaltyGames.Core.csproj", "EDG.LoyaltyGames.Core/"]
COPY ["EDG.LoyaltyGames.Infrastructure/EDG.LoyaltyGames.Infrastructure.csproj", "EDG.LoyaltyGames.Infrastructure/"]
COPY ["EDG.LoyaltyGames.Services/EDG.LoyaltyGames.Services.csproj", "EDG.LoyaltyGames.Services/"]
RUN dotnet restore "EDG.LoyaltyGames.APIS/EDG.LoyaltyGames.APIS.csproj"
COPY . .
WORKDIR "/src/EDG.LoyaltyGames.APIS"
RUN dotnet build "EDG.LoyaltyGames.APIS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EDG.LoyaltyGames.APIS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EDG.LoyaltyGames.APIS.dll"]

