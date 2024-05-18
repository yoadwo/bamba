# Build

# # Base image
FROM alpine as build
RUN apk add dotnet6-sdk

# # Set the working directory
WORKDIR /app

# # Copy the .csproj and restore NuGet dependencies
COPY *.csproj ./
RUN dotnet restore

# # Copy the rest of the application code
COPY . ./

# # Build the application
ENV DOTNET_ENVIRONMENT=Production
RUN dotnet clean BambaAdminAPI.csproj
RUN dotnet publish BambaAdminAPI.csproj -c Release -o /app/publish

# Run

# # Runtime image
FROM alpine
RUN apk add aspnetcore6-runtime

# # Set the working directory
WORKDIR /app

# # Copy the published output from the build stage
COPY --from=build /app/publish .

# # install packages needed by nuget NetCoreAudio to play sound
RUN apk add --no-cache bash pulseaudio alsa-utils sox alsa-plugins-pulse mpg123

# # setup env
ENV PULSE_SERVER=host.docker.internal
ENV ASPNETCORE_URLS=http://+:61839

# # run
EXPOSE 61839
ENTRYPOINT ["dotnet", "BambaAdminAPI.dll"]