FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app

COPY ./*.csproj ./

RUN dotnet restore

COPY . ./

RUN dotnet build --no-restore --configuration Release

RUN dotnet publish --no-build --configuration Release --output out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "CookingQuest.Web.dll" ]