#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# Install NodeJs
RUN apt-get update && \
apt-get install -y wget && \
apt-get install -y gnupg2 && \
wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
apt-get install -y build-essential nodejs
# End Install
COPY ["BartenderSupportSystem/BartenderSupportSystem.Server.csproj", "BartenderSupportSystem/"]
RUN dotnet restore "BartenderSupportSystem/BartenderSupportSystem.Server.csproj"
COPY . .
WORKDIR "/src/BartenderSupportSystem"
RUN dotnet build "BartenderSupportSystem.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BartenderSupportSystem.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BartenderSupportSystem.Server.dll"]