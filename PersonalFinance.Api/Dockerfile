FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine

WORKDIR /app
EXPOSE 80
COPY ["PersonalFinance.Api/bin/Debug/net7.0", "/app"]

ENTRYPOINT ["dotnet", "PersonalFinance.Api.dll"]