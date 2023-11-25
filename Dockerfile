#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80
#EXPOSE 443
#Expose 5000


# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
# Copy the solution file and restore dependencies
COPY TestForum.sln .
COPY TestForum.API/TestForum.API.csproj ./TestForum.API/
COPY TestForum.Data/TestForum.Data.csproj ./TestForum.Data/
COPY TestForum.API/key /app/key

RUN dotnet restore

#RUN dotnet tool install --global dotnet-ef
# Copy the source code and build the application
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish
  
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY TestForum.API/key /app/key
ENTRYPOINT ["dotnet", "TestForum.API.dll"] 