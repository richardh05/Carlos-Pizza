## Running
I'd recommend you use docker compose for personal hosting, including the necessary SQL Server instance:
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
    build:
      context: .
      dockerfile: Dockerfile
    container_name: carlos-pizza
    depends_on:
      - db
    environment:
      ConnectionStrings__CarlosDB: "Data Source=...;Password=..."
      ADMIN_EMAIL: admin@carlospizza.local
      ADMIN_PASSWORD: "SuperSecret123"
      ASPNETCORE_URLS: "http://+:8080"
    ports:
      - "8080:8080"
      - "8081:8081"

volumes:
  mssql_data:
```

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
### Running from the shell
Set the connection string:
```bash
dotnet user-secrets set "ConnectionStrings:CarlosDB" "Data Source=...;Password=..."
```

Then launch with a default admin account, which you can change once launched:
```bash
ADMIN_EMAIL=admin@carlospizza.local \
ADMIN_PASSWORD=SuperSecret123 \
CREATE_DEMO_USER=true \
dotnet run
```
Tip: you can export these variables or [set them in Rider](https://www.jetbrains.com/help/rider/Run_Debug_Configuration.html#envvars-progargs) for easier running in the future.