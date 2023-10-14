# 5D - Permission Managment by Mauricio Gracia Gutierrez

The folder structure is:
 - `WebApi` contains the .NET CORE REST API
 - `WebApiTests` contains the unit tests and the integration tests
 - `prepare` contains artifacts used to bootstrap the solution using docker-compose
 - `web-site` contains the ReactJS web site

This architecture will be used, each box is and abstraction layer but it does not represent a container
![alt text](Architecture.png "Title")

With this architecture you can choose the type of persistance strategy that you want :`Database` or `ElasticSearch` to achieve that 
`Dependency Injection` is used combined with `Repository Pattern`. To control that there is `UseMemoryDB` setting in the `WebApi/appsettings.json` file can changed for that and run `./bapi`

This project uses docker-compose to build and launch the application, there are a few utilitary commands for that (docker engine needs to be running)
- `./launch` used to prepare and launch all the containers at once
- `./stop` all the containers will be stopped

Since rebuilding all containers is not time effective for development I created 3 utilitary commands:
- `./bdba` build the database container
- `./bapi` build the API container
- `./bweb` build the ReactJS container

The ./launch command will pull the images, create the containers and setup the database tables and relationships
