# Step 1: Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file to the container
COPY ["Zafaty.Server.csproj", "./"]

# Restore the project's dependencies
RUN dotnet restore "Zafaty.Server.csproj"

# Copy the rest of the source code into the container
COPY . .

# Set the working directory to the project folder
WORKDIR "/src"

# Build the project in release mode and specify the output folder
RUN dotnet build "Zafaty.Server.csproj" -c Release -o /app/build

# Step 2: Publish the app to prepare it for deployment
FROM build AS publish
RUN dotnet publish "Zafaty.Server.csproj" -c Release -o /app/publish

# Step 3: Use the official .NET ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published app from the build stage
COPY --from=publish /app/publish .

# Expose port 80 for the application
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Zafaty.Server.dll"]
