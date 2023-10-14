using Xunit;
using WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Persistance.EntityFramework;

public class PermisosTests
{
    [Fact]
    public void TestGetPermissions()
    {
        // Arrange
        var mockPersist = new Mock<IRepository<Permission>>();
        // Create a list of sample permissions
        var samplePermissions = new List<Permission>
        {
            new Permission { Id = 1, NombreEmpleado = "Employee1", ApellidoEmpleado = "Last1", TipoPermisoId = 1, FechaPermiso = new DateTime(2023, 10, 1) },
            new Permission { Id = 2, NombreEmpleado = "Employee2", ApellidoEmpleado = "Last2", TipoPermisoId = 2, FechaPermiso = new DateTime(2023, 10, 2) }
        };
        mockPersist.Setup(p => p.GetAll()).Returns(samplePermissions);

        var controller = new PermissionsController(mockPersist.Object);

        // Act
        var result = controller.Get();

        // Assert
        Assert.IsType<List<Permission>>(result);
        Assert.Equal(2, ((List<Permission>)result).Count);
    }

    

    [Fact]
    public void IntegrationTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApiDbContext>()
           .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
           .Options;

        var DbContext = new ApiDbContext(options);
        var controller = new PermissionsController(new PermissionRepository(DbContext));
        

        // Create a new permission to add
        var newPermission = new Permission
        {
            NombreEmpleado = "NewEmployee",
            ApellidoEmpleado = "NewLastName",
            TipoPermisoId = 3,
            FechaPermiso = new DateTime(2023, 10, 3)
        };

        // Act: Add the new permission using the controller
        var addActionResult = controller.Post(newPermission) as CreatedAtRouteResult;

        // Assert
        // Verify that the permission was added successfully
        Assert.NotNull(addActionResult);
        Assert.Equal("GetPermissions", addActionResult.RouteName);

        // Act: Retrieve permissions
        List<Permission> result = controller.Get().ToList();

        // Assert
        // Your assertions here based on the result
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);

        // Act: Modify the permission
        newPermission.NombreEmpleado = "Abcdef";
        newPermission.ApellidoEmpleado = "xyzzz";
        
        addActionResult = controller.Put(newPermission.Id, newPermission) as CreatedAtRouteResult;

        // Act: Retrieve permissions
        result = controller.Get().ToList();
        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
        Permission mod = result.Find(p => p.Id == newPermission.Id);
        
        //Check that the properties where changed
        Assert.Equal(mod.NombreEmpleado, newPermission.NombreEmpleado);
        Assert.Equal(mod.ApellidoEmpleado, newPermission.ApellidoEmpleado);
    }
}
