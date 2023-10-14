# 5D - Permission Managment by Mauricio Gracia Gutierrez

The folder structure is:
 - WebApi - contains the .NET CORE REST API
 - WebApiTests - contains the unit tests and the integration tests
 - prepare - contains artifacts used to bootstrap the solution using docker-compose
 - web-site - contains the ReactJS web site

This architecture will be used, each box is and abstraction layer but it does not represent a container
![alt text](Architecture.png "Title")

This project uses docker-compose to build and launch the application, there are a few utilitary commands for that (docker engine needs to be running)
- ./launch
The idea is that you can choose the type of persistance that you want (Database or ElasticSearch)
here Dependency Injection is used combined with Repository Pattern to achieve that.

The `UseMemoryDB` setting in the `WebApi/appsettings.json` file can changed for that and run `./bapi`
