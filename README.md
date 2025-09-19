![.NET](https://img.shields.io/badge/.NET-8.0.19-blue?logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Ready-blue?logo=docker)
![Platform](https://img.shields.io/badge/Platform-Linux%2FWindows-lightgrey?logo=linux&logoColor=white)
![License](https://img.shields.io/badge/License-GPLv3-yellow?logo=open-source-initiative)
# Carlos Pizza
Carlos Pizza is an ASP.NET Core 8.0 web application built for the CO5227 Web Application Development module, with some additional content from self-learning.  
The project demonstrates the use of modern web technologies including Entity Framework Core, Identity for authentication, and Docker for containerization.

The application allows users to:
- Browse and order pizzas
- Checkout with prices stored in a SQL Server database
- Log in with role-based accounts (Admin, Member)

## Features & Technologies Used
- Razor Pages: using embedded **C#** for the logic.
- Consistent Design: used across all pages with robust **CSS** styling.
- Publishing & Deployment: live on a student web server via **FTP**.
- Mapping SQL to objects: modelled with the **Entity Framework** ORM.
- Reading and Writing to the DB: with both **SQL** and **LINQ** queries
- Account system: enabled by the **Identity** API.
- Containerisation: supplied by **Docker**.
- Reproducible documentation: report written in **LaTeX**

## Docker Compose
I'd strongly recommend you use docker compose for personal hosting, including the necessary Microsoft SQL Server instance. 
Otherwise, you'll need to set up an SQL server, manage dependencies and configure the software manually.
```yaml
version: "3.9"

services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: carlos-db
    environment:
      SA_PASSWORD: "YourStrong!Passw0rd"   # replace with dev-only password
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - mssql_data:/var/opt/mssql

  web:
    image: ghcr.io/richardh05/carlos-pizza:latest
    container_name: carlos-pizza
    depends_on:
      - db
    environment:
      CONNECTIONSTRINGS__CARLOSDB: "Server=db;Database=CarlosDB;User ID=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
      ADMIN__EMAIL: "admin@carlospizza.local"
      ADMIN__PASSWORD: "SuperSecret123"
      ENABLE__DEMOUSER: true
      ASPNETCORE_URLS: "http://+:8080"
    ports:
      - "8080:8080"

volumes:
  mssql_data:
```

### Tip: getting your connection string
```bash
# enter the docker container
docker exec -it carlos-db bash
# run sqlcmd to set sa's password to "YourStrong!Passw0rd"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd"
```

## Requirements
### Arch Linux
```bash
sudo pacman -S dotnet-runtime-8.0 dotnet-sdk-8.0 aspnet-runtime-8.0
```

### Manual / Other
Download a [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download). 
These [package managers](https://learn.microsoft.com/en-us/dotnet/core/install/linux?WT.mc_id=dotnet-35129-website) are supported.

## Build
Clone and enter the repo.
```bash
git clone https://github.com/richardh05/Carlos-Pizza.git
cd Carlos-Pizza
```

### Secrets
Carlos-Pizza requires you set a connection string, admin email, admin password and enable/disable the demo user.
There are several ways to do this:
1. Environment variables, either exported or in the shell session:
```bash
CONNECTIONSTRINGS__CARLOSDB="Data Source=...;Password=..." \
ADMIN__EMAIL=admin@carlospizza.local \
ADMIN__PASSWORD=SuperSecret123 \
ENABLE__DEMOUSER=true \
dotnet run
```
Tip: you can [set these in Rider](https://www.jetbrains.com/help/rider/Run_Debug_Configuration.html#envvars-progargs) for easier running in the future.

2. Using dotnet-cli secrets:
```bash
dotnet user-secrets set "ConnectionStrings:CarlosDB" "Data Source=...;Password=..."
dotnet user-secrets set "Admin:Email" "admin@carlospizza.local"
dotnet user-secrets set "Admin:Password" "SuperSecret123"
dotnet user-secrets set "Enable:DemoUser" "true"
```
3. In `appsettings.json` or `appsettings.Development.json` in the project root:

```json
{
  "ConnectionStrings": {
    "CarlosDB": "Data Source=...;Password=..."
  },
  "Admin": {
    "Email": "admin@site.com",
    "Password": "Secret123"
  },
  "Enable": {
    "DemoUser": true
  }
}
```
4. In the Docker Compose as environment variables, which has already been covered.

Tip: you may need to add `TrustServerCertificate=True` to the end of your connection string.

### Running
Because this project depends on EF Core, you need to apply the migrations before running.
```bash
# install the tool
dotnet tool install --global dotnet-ef
# generate a new migration (avoid, probably not necessary)
dotnet ef migrations add InitialCreate
# apply migration
dotnet ef database update
```

```bash
dotnet run
```
## Usage


## Development
### Structure
```shell
├── Areas/Identity #pages related to account (Identity) management
├── Data
├── Migrations
├── Models
├── obj
├── Pages
├── Properties
├── Report
└── wwwroot
```

