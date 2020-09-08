## Command to create the migration

```
> dotnet ef migrations add InitialCreate -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.TopazDbContext -o MigrationsApp
```

```
> dotnet ef migrations add InitialCreate -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.AuthDbContext -o MigrationsAuth
```

## Command to apply the migration to the database

```
> dotnet ef database update -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.TopazDbContext
```

```
> dotnet ef database update -s ./Topaz.UI.Razor -p ./Topaz.Data -c Topaz.Data.AuthDbContext
```

## Command to convert CSV to JSON for import

```
> Import-CSV "filename.csv" | Select FirstName,LastName,MiddleInitial,@{Name="Age";Expression={[int32]$_.Age}},MailingAddress1,MailingAddress2,PostalCode,PhoneNumber,PhoneType | ConvertTo-Json | Add-Content -Path "filename.json"
```

## DB Migration Instructions...

1. make sure the include db file references are commented out for the web .csproj file
2. delete the databases in the web project folder
3. delete the migration folders in the data project folder
4. from the root folder run the commands to generate the migrations
5. from the root folder run the commands to create the empty database files (will create db files in web project folder)
6. move AuthDb.db to the user console
7. move TopazDb.db to the migrations console
8. run the user console
9. move AuthDb.db back to web project folder
10. make copy of TopazDb.db called TopazDb.src.db
11. run the migration console
12. move TopazDb.db back to web project folder
13. go into the TopazDb.db database and link the users to the publishers

## Links

[Configuring the web project to be deployed](https://www.hanselman.com/blog/DeployingTWOWebsitesToWindowsAzureFromOneGitRepository.aspx)

[Using Vue CLI Inside an ASP.NET Core Project](https://wildermuth.com/2019/04/08/Using-Vue-CLI-Inside-an-ASP-NET-Core-Project)
