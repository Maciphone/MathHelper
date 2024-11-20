

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ./localhost-cert.pem /https/localhost-cert.pem
COPY ./localhost-key.pem /https/localhost-key.pem


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MathHelperr.csproj ./
RUN dotnet restore "MathHelperr.csproj"

COPY . .
WORKDIR "/src/."
RUN dotnet build "MathHelperr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MathHelperr.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MathHelperr.dll", "--urls", "https://+:443;http://+:80"]