# Gateways Web API Project
To get more details go the API documentation /swagger 

## Development server

Run `dotnet run` for a dev server. Navigate to `http://localhost:53388/`. The app will automatically reload if you change any of the source files.

## Build

Stay up the project `Gateways.API` then run `dotnet build` to build the project.

The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

## Further help
`Add Migrations`
dotnet ef migrations add Init --project Gateways.Persistence -s Gateways.API

`Update Database`
dotnet ef database update --project Gateways.Persistence -s Gateways.API

`Remove Migrations`
dotnet ef migrations remove --project Gateways.Persistence

