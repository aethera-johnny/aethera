# Stage 1: Build the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["src/Frontend/Frontend.csproj", "src/Frontend/"]
RUN dotnet restore "src/Frontend/Frontend.csproj"

# Copy the rest of the application files
COPY . .
WORKDIR "/src/src/Frontend"
RUN dotnet publish "Frontend.csproj" -c Release -o /app/publish

# Stage 2: Serve the app using Nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy the built app from the build stage
COPY --from=build /app/publish/wwwroot .

# Copy the Nginx configuration file
COPY src/Frontend/nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80
