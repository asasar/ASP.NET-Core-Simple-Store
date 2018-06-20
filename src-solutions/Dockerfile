FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app
#EXPOSE 8080

# copy csproj and restore as distinct layers
COPY *.sln .
COPY SimpleStore/*.csproj ./SimpleStore/
COPY SimpleStore.DbContext/*.csproj ./SimpleStore.DbContext/
COPY SimpleStore.Models/*.csproj ./SimpleStore.Models/
COPY SimpleStore.ViewModels/*.csproj ./SimpleStore.ViewModels/
COPY SimpleStore.Utils/*.csproj ./SimpleStore.Utils/
COPY SimpleStore.Tests/SimpleStore.Tests.csproj ./SimpleStore.Tests/
RUN dotnet restore
COPY docker-compose.dcproj .

# copy and build everything else
COPY SimpleStore/. ./SimpleStore/


COPY SimpleStore/. ./SimpleStore/
COPY SimpleStore.DbContext/. ./SimpleStore.DbContext/
COPY SimpleStore.Models/. ./SimpleStore.Models/
COPY SimpleStore.ViewModels/. ./SimpleStore.ViewModels/
COPY SimpleStore.Utils/. ./SimpleStore.Utils/

COPY  SimpleStore.Tests/. ./SimpleStore.Tests/

RUN dotnet build ./SimpleStore/SimpleStore.csproj

#FROM build AS testrunner
#WORKDIR /app/SimpleStore.Tests
#ENTRYPOINT ["dotnet", "test","--logger:trx"]

FROM build AS test
WORKDIR /app/SimpleStore.Tests
RUN dotnet test  /p:CollectCoverage=true

FROM test AS publish
WORKDIR /app/SimpleStore
RUN dotnet publish -o out -r linux-x64

FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/SimpleStore/out ./
#ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ASPNETCORE_URLS=http://*:8080
VOLUME ["/dbSimplestore"]

ENTRYPOINT ["dotnet", "SimpleStore.dll"]
