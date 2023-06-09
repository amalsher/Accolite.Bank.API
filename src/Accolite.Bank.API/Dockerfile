#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Accolite.Bank.API/Accolite.Bank.API.csproj", "Accolite.Bank.API/"]
COPY ["Accolite.Bank.API/Accolite.Bank.Data.MsSql.csproj", "Accolite.Bank.Data.MsSql/"]
RUN dotnet restore "Accolite.Bank.API/Accolite.Bank.API.csproj"
COPY . .
WORKDIR "/src/Accolite.Bank.API"
RUN dotnet build "Accolite.Bank.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accolite.Bank.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accolite.Bank.API.dll"]