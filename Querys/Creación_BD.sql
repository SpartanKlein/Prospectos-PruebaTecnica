

-- Tabla de Prospectos (sin cambios)
CREATE TABLE Prospectos (
    ProspectoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    PrimerApellido NVARCHAR(100) NOT NULL,
    SegundoApellido NVARCHAR(100),
    Calle NVARCHAR(200) NOT NULL,
    Numero NVARCHAR(20) NOT NULL,
    Colonia NVARCHAR(100) NOT NULL,
    CodigoPostal NVARCHAR(10) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    RFC NVARCHAR(13) NOT NULL,
    Estatus NVARCHAR(20) NOT NULL CHECK (Estatus IN ('Enviado', 'Autorizado', 'Rechazado')),
    ObservacionesRechazo NVARCHAR(MAX),
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    PromotorID INT NOT NULL
);

-- Tabla de Documentos (actualizada)
CREATE TABLE Documentos (
    DocumentoID INT IDENTITY(1,1) PRIMARY KEY,
    ProspectoID INT NOT NULL,
    NombreDocumento NVARCHAR(200) NOT NULL,
    RutaArchivo NVARCHAR(500) NOT NULL,
    NombreOriginalArchivo NVARCHAR(255) NOT NULL,
    TipoArchivo NVARCHAR(50) NOT NULL,
    TamanoArchivo BIGINT NOT NULL,
    FechaCarga DATETIME2 NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ProspectoID) REFERENCES Prospectos(ProspectoID)
);

-- Tabla de Promotores (sin cambios)
CREATE TABLE Promotores (
    PromotorID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
	Contrasena NVARCHAR(255) NOT NULL,
	TipoUsuario NVARCHAR(20) NOT NULL CHECK (TipoUsuario IN ('Supervisor', 'Promotor'))
);

-- Agregar clave foránea a la tabla Prospectos (sin cambios)
ALTER TABLE Prospectos
ADD CONSTRAINT FK_Prospectos_Promotores
FOREIGN KEY (PromotorID) REFERENCES Promotores(PromotorID);

-- Índices para mejorar el rendimiento (sin cambios)
CREATE INDEX IX_Prospectos_Estatus ON Prospectos(Estatus);
CREATE INDEX IX_Prospectos_PromotorID ON Prospectos(PromotorID);
CREATE INDEX IX_Documentos_ProspectoID ON Documentos(ProspectoID);


