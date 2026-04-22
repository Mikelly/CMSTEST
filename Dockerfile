FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["CMS/CMS.csproj", "CMS/"]
RUN dotnet restore "CMS/CMS.csproj"
COPY . .
WORKDIR "/src/CMS"
RUN dotnet publish "CMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMS.dll"]
