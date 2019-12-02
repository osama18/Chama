FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Chama.Web.SubscribtionV1/Chama.Web.SubscribtionV1.csproj", "Chama.Web.SubscribtionV1/"]
RUN dotnet restore "Chama.Web.SubscribtionV1/Chama.Web.SubscribtionV1.csproj"
COPY . .
WORKDIR "/src/Chama.Web.SubscribtionV1"
RUN dotnet build "Chama.Web.SubscribtionV1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chama.Web.SubscribtionV1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chama.Web.SubscribtionV1.dll"]