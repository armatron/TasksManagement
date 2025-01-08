#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TasksManagement.Api/TasksManagement.Api.csproj", "src/TasksManagement.Api/"]
COPY ["src/TasksManagement.Application/TasksManagement.Application.csproj", "src/TasksManagement.Application/"]
COPY ["src/TasksManagement.Domain/TasksManagement.Domain.csproj", "src/TasksManagement.Domain/"]
COPY ["src/TasksManagement.Infrastructure/TasksManagement.Infrastructure.csproj", "src/TasksManagement.Infrastructure/"]
RUN dotnet restore "./src/TasksManagement.Api/TasksManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/TasksManagement.Api"
RUN dotnet build "./TasksManagement.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TasksManagement.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TasksManagement.Api.dll"]