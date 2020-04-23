Command to create the migration

> dotnet ef migrations add InitialCreate -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.TopazDbContext -o MigrationsApp
> dotnet ef migrations add InitialCreate -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.AuthDbContext -o MigrationsAuth

Command to apply the migration to the database

> dotnet ef database update -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.TopazDbContext
> dotnet ef database update -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.AuthDbContext
