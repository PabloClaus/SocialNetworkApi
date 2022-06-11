# Social Network API

Social network for developers (NET Dev position Test Task)

**Main functions:**

- Users are able to sign-up (self register).
- Each user should specify Email, FirstName, LastName and password. Optionally users may specify Birthday and Gender.
- Users are able to sign-in (by providing email and password).
- Users are able to update their profile while being logged in (all fields except email).
- Users are able to list and get profiles of other users while being logged in (Gender and Rol optional filters).
- API for Admin user, that allows to delete profiles of specific users.

**Other features**

- Swagger UI support.
- Input validation.
- Filter results option.
- API methods XML documentation.
- Json Web Token Authentication.
- Basic Test Cases with xUnit.

**Technologies**
- .NET 6
- Entity Framework Core 6
- SQL Server Local DB 15.0.4
- Microsoft.AspNetCore.Authentication.JwtBearer 6.0.5
- Swashbuckle.AspNetCore 6.2.3


**Requirements**

- Visual Studio 2022 IDE Community
- SQL Server (LocalDB) with local user permissions
- .NET 6
- Web Browser

## **How to use it**

**Database Creation**

- Open SQL Server (LocalDb) and create a new database: SocialNetworkApiDatabase

If you decide to use another database, change the name on appsettings.json.

**Application execution**

1. Download the project.
2. Open the project in Visual Studio IDE.
3. Open "Package Manager Console". Select "SocialNetworkApi.Core" project and execute the command: 
`update-database`

You should see something like this in SQL Server:

![image](https://user-images.githubusercontent.com/14250936/173121654-4b556230-4e7c-491b-b583-50f2753b78b5.png)

Initial users are created in ther first execution: 9 users and 1 admin (mail: admin@mail.com, pass: admin)

User's password follows the rule: FirstName's first character + LastName (For example: Pablo Claus's password is pclaus)

4. Execute the project on IIS.

You will see Swagger open

![image](https://user-images.githubusercontent.com/14250936/173207776-40021b51-7b61-4658-a578-44eb909c433e.png)

