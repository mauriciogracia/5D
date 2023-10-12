using Xunit;
using WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

public class PermisosTests
{
    private readonly ApiDbContext _context;

    public PermisosTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        _context = new ApiDbContext(options);
    }

    [Fact]
    public void TestGetPermissions()
    {
        // Arrange
        var mockPersist = new Mock<IPersistPermissions>();
        // Create a list of sample permissions
        var samplePermissions = new List<Permission>
        {
            new Permission { Id = 1, NombreEmpleado = "Employee1", ApellidoEmpleado = "Last1", TipoPermisoId = 1, FechaPermiso = new DateTime(2023, 10, 1) },
            new Permission { Id = 2, NombreEmpleado = "Employee2", ApellidoEmpleado = "Last2", TipoPermisoId = 2, FechaPermiso = new DateTime(2023, 10, 2) }
        };
        mockPersist.Setup(p => p.GetPermissions()).Returns(samplePermissions);

        var controller = new PermissionController(mockPersist.Object);

        // Act
        var result = controller.Get();

        // Assert
        Assert.IsType<List<Permission>>(result);
        Assert.Equal(2, ((List<Permission>)result).Count);
    }

    

    [Fact]
    public void Int_Test_GetPermissions()
    {
        // Arrange
        var controller = new PermissionController(new PersistPermissionsEF(_context));

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
        var result = controller.Get();

        // Assert
        // Your assertions here based on the result
        Assert.NotNull(result);
        Assert.IsType <List<Permission>>(result); // Replace SomeExpectedType with the actual return type.
        Assert.Equal(1, ((List<Permission>)result).Count);
    }
}
