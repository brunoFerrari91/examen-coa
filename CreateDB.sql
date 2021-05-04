CREATE DATABASE ExamenDB
GO
USE ExamenDB
GO
CREATE TABLE Usuarios(
IdUsuario int NOT NULL PRIMARY KEY IDENTITY(1,1),
UserName nvarchar(50) NOT NULL,
Nombre nvarchar(50) NOT NULL ,
Email nvarchar(50) NOT NULL ,
Telefono nvarchar(20) NULL
)

