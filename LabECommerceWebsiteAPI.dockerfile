FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Lab E-Commerce Website API/Lab E-Commerce Website API.csproj", "Lab E-Commerce Website API/"]
RUN dotnet restore "Lab E-Commerce Website API/Lab E-Commerce Website API.csproj"
COPY . .
WORKDIR "/src/Lab E-Commerce Website API"
RUN dotnet build "Lab E-Commerce Website API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lab E-Commerce Website API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lab E-Commerce Website API.dll"]