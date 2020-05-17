
# citel-test
ASP.Net Core Application with MVC, WebAPI, EntityFramework, MySQL
- Create MySQL database and Migrations table:
	
	`` mysql> CREATE DATABASE citel; ``

	 `` mysql> USE citel; ``

	`` mysql> CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );``
	
- To access Back-End WebAPI project: 
	 ```
	$ cd ./citel-api
	$ dotnet restore
	$ dotnet build
	$ dotnet ef database update
	$ dotnet run  
	```
> Swagger runs on [http://localhost:5000/index.html](http://localhost:5000/index.html)
 
 - To access Front-End project:
	 ```
	 $ cd ./citel-app
	 $ dotnet restore
	 $ dotnet build
	 $ dotnet run
	 ```
> Front-End runs on [http://localhost:3000](http://localhost:3000)
