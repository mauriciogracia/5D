using Xunit;
using WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Persistance;

public class PermisosTests
{
    [Fact]
    public void TestGetPermissions()
    {
        // Arrange
        var mockPersist = new Mock<IPersist>();
        // Create a list of sample permissions
        var samplePermissions = new List<Permiso>
        {
            new Permiso { Id = 1, NombreEmpleado = "Employee1", ApellidoEmpleado = "Last1", TipoPermiso = 1, FechaPermiso = new DateTime(2023, 10, 1) },
            new Permiso { Id = 2, NombreEmpleado = "Employee2", ApellidoEmpleado = "Last2", TipoPermiso = 2, FechaPermiso = new DateTime(2023, 10, 2) }
        };
        mockPersist.Setup(p => p.GetPermissions()).Returns(samplePermissions);

        var controller = new PermisosController(mockPersist.Object);

        // Act
        var result = controller.Get();

        // Assert
        Assert.IsType<List<Permiso>>(result);
        Assert.Equal(2, ((List<Permiso>)result).Count);
    }
}
