FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build "/src/PersonalFinance.Api/PersonalFinance.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/PersonalFinance.Api/PersonalFinance.Api.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=publish /app/out ./
EXPOSE 8080
RUN addgroup -g 11011 appgroup && adduser -u 11011 appuser -G appgroup -D
RUN chown appuser:appgroup /app
USER appuser:appgroup
ENV COMPlus_EnableDiagnostics=0
ENTRYPOINT ["dotnet", "PersonalFinance.Api.dll"]