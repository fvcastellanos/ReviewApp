FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /review

# copy csproj and restore as distinct layers
COPY ./ ./
RUN dotnet restore

# copy everything else and build
COPY ReviewApp/ ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /opt/cavitos/review
COPY --from=build /review/out ./

RUN useradd -m review \
    && chown -R review:review /opt/cavitos/review

USER review

EXPOSE 5000
ENTRYPOINT ["dotnet", "ReviewApp.dll"]
