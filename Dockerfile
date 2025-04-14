# Step 1: Use the official .NET image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY ["Zafaty.Server/Zafaty.Server.csproj", "Zafaty.Server/"]
RUN dotnet restore "Zafaty.Server/Zafaty.Server.csproj"

# Copy the rest of the files
COPY . .

# Build the project
WORKDIR "/src/Zafaty.Server"
RUN dotnet build "Zafaty.Server.csproj" -c Release -o /app/build

# Step 2: Publish the app to be ready for deployment
FROM build AS publish
RUN dotnet publish "Zafaty.Server.csproj" -c Release -o /app/publish

# Step 3: Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port
EXPOSE 80

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "Zafaty.Server.dll"]
