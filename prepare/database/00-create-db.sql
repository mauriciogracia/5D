CREATE DATABASE PermissionsDB;
GO
USE PermissionsDB;
GO

CREATE TABLE PERMISOS (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreEmpleado NVARCHAR(100) NOT NULL,
    ApellidoEmpleado NVARCHAR(100) NOT NULL,
    TipoPermisoId INT NOT NULL,
    FechaPermiso DATE NOT NULL
);

CREATE TABLE TIPO_PERMISOS (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100) NOT NULL
);

-- Add a foreign key constraint to establish the relationship
ALTER TABLE PERMISOS
ADD CONSTRAINT FK_Permisos_TipoPermiso
FOREIGN KEY (TipoPermisoId) 
REFERENCES TIPO_PERMISOS(Id);

GO