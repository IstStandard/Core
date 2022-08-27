FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /opt/build

COPY Core/ Core/
COPY StandardShared StandardShared/

RUN apt update && apt install python3 -y
WORKDIR Core
COPY shared_path_fix.py .

RUN python3 shared_path_fix.py Core.sln ./Core/Core.csproj ../StandardShared/StandardShared/StandardShared.csproj
RUN dotnet restore Core.sln

RUN mkdir /opt/app
RUN dotnet publish Core.sln -c Release -o /opt/app/

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /opt/app
COPY --from=build-env /opt/build/ .
WORKDIR /opt/app/Core/bin/Release/net6.0

EXPOSE 7001

ENTRYPOINT ["dotnet", "Core.dll"]
