FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /app

COPY *.sln ./
COPY CookingQuest.Web/*.csproj CookingQuest.Web/
COPY CookingQuest.Tests/*.csproj CookingQuest.Tests/

RUN dotnet restore

COPY . ./

RUN dotnet build --no-restore --configuration Release

# RUN dotnet test

RUN dotnet publish CookingQuest.Web --no-build --configuration Release --output /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "CookingQuest.Web.dll" ]