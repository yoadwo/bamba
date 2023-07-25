# Base image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

# Set the working directory
WORKDIR /app

# Copy the .csproj and restore NuGet dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

ENV DOTNET_ENVIRONMENT=Production

# # Build the application
RUN dotnet clean BambaAdminAPI.csproj
RUN dotnet publish BambaAdminAPI.csproj -c Release -o /app/publish


# # Runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

# # Set the working directory
WORKDIR /app

# # Copy the published output from the build-env
COPY --from=build-env /app/publish .

# # install packages needed by nuget NetCoreAudio to play sound
RUN apt-get update
RUN apt-get install mpg123 -y


# # Run the application
ENV ASPNETCORE_URLS=http://+:61839
ENTRYPOINT ["dotnet", "BambaAdminAPI.dll"]
