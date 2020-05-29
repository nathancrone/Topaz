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
> Import-CSV "filename.csv" | Select FirstName,LastName,MiddleInitial,@{Name="Age";Expression={[int32]\$\_.Age}},MailingAddress1,MailingAddress2,PostalCode,PhoneNumber,PhoneType | ConvertTo-Json | Add-Content -Path "filename.json"
```

## Links

[Configuring the web project to be deployed](https://www.hanselman.com/blog/DeployingTWOWebsitesToWindowsAzureFromOneGitRepository.aspx)

[Using Vue CLI Inside an ASP.NET Core Project](https://wildermuth.com/2019/04/08/Using-Vue-CLI-Inside-an-ASP-NET-Core-Project)
