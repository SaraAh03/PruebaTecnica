CREATE DATABASE AsoConstructoras;
GO

USE AsoConstructoras;
GO

CREATE TABLE Proyectos
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(50) UNIQUE NOT NULL,
    Nombre NVARCHAR(255) NOT NULL,
    Direccion NVARCHAR(255) NOT NULL,
    Constructora NVARCHAR(100) NOT NULL,
    Contacto NVARCHAR(100) NOT NULL
);

CREATE TABLE PersonasInteresadas
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Correo NVARCHAR(255) NOT NULL,
    FechaNacimiento DATE, -- Nuevo campo de fecha de nacimiento
    ProyectoId INT FOREIGN KEY REFERENCES Proyectos(Id) -- Clave foránea
);
