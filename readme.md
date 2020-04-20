Command to create the migration

> dotnet ef migrations add InitialCreate -s ./Topaz.UI.Razor -p ./Topaz.Data

Command to apply the migration to the database

> dotnet ef database update -s ./Topaz.UI.Razor -p ./Topaz.Data
