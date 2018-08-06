FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 52313
EXPOSE 44307

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY WooliesXTechnicalChallenge/WooliesXTechnicalChallenge.csproj WooliesXTechnicalChallenge/
RUN dotnet restore WooliesXTechnicalChallenge/WooliesXTechnicalChallenge.csproj
COPY . .
WORKDIR /src/WooliesXTechnicalChallenge
RUN dotnet build WooliesXTechnicalChallenge.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WooliesXTechnicalChallenge.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WooliesXTechnicalChallenge.dll"]
