# Library Management System

This is a Library Management System application that provides the main functions such as:
* user sign up, 
* user login, 
* upload user picture,
* add user information, 
* update user information, 
* delete all the user information with admin rights.
The System uses authentication and authorization with Json Web Token. 
The purpose of this application was to make an exam task, but the system can be modified and applied in the future.

## Usage/Examples
### API Functionalities:

Visitor API
![VisitorApi](https://github.com/iXsas/LibraryMSv3/blob/master/LoginIrSignup.jpg?raw=true)

Visitor API SignUp
![VisitorApiSignup](https://github.com/iXsas/LibraryMSv3/blob/master/SignupLaukas.jpg?raw=true)

Authorization API
![Authorize]( https://github.com/iXsas/LibraryMSv3/blob/master/AuthorizationEndpoint.jpg?raw=true)

User API
![UserAPI](https://github.com/iXsas/LibraryMSv3/blob/master/User_endpoints.jpg?raw=true)

Management API
![ManagementAPI](https://github.com/iXsas/LibraryMSv3/blob/master/management_endpoints.jpg?raw=true)

Management API Delete User
![ManagementApiDeleteUser](https://github.com/iXsas/LibraryMSv3/blob/master/User_delete_endpoints.jpg?raw=true)

### Code Architecture
![Code Architecture](https://github.com/iXsas/LibraryMSv3/blob/master/Project_view.jpg?raw=true)

## Running database
1. Setup <b>appsettings.json</b> file by adding a connection string to your database, now it is left "DESKTOP-94B63OJ".
  ```raw
 "ConnectionStrings": {
    "SqlServer": "Data Source=DESKTOP-94B63OJ\\SQLEXPRESS;Initial Catalog=LibraryMS;Integrated Security=True;TrustServerCertificate=True"
  ```
2. Run the following commands in the Package Manager Console (View -> Other windows -> Package Manager Console ) to create the required tables in the database:
  
  ```dotnet
add-migration InitialCreate
update-database
  ```

## Running Tests
To run tests, need to test them through Microsoft visual studio xUnit test part

## License
This is free and unencumbered software released into the public domain.

## Contact
- Romanas Kabula - [ixsas](https://github.com/ixsas) - romas.kabula@gmail.com
- Project Link: https://github.com/iXsas/LibraryMSv3.git

