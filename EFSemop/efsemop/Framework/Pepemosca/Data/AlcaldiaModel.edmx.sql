
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/08/2016 20:31:24
-- Generated from EDMX file: C:\Users\Luis Alberto\Source\Repos\basicefmodelsemop\EFSemop\efsemop\Framework\Pepemosca\Data\AlcaldiaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AlcaldiaModel];
GO
IF SCHEMA_ID(N'core') IS NULL EXECUTE(N'CREATE SCHEMA [core]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[core].[FK_DireccionProyectoBase]', 'F') IS NOT NULL
    ALTER TABLE [core].[Proyectos] DROP CONSTRAINT [FK_DireccionProyectoBase];
GO
IF OBJECT_ID(N'[core].[FK_Responsable_inherits_Persona]', 'F') IS NOT NULL
    ALTER TABLE [core].[Personas_Responsable] DROP CONSTRAINT [FK_Responsable_inherits_Persona];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[core].[Personas]', 'U') IS NOT NULL
    DROP TABLE [core].[Personas];
GO
IF OBJECT_ID(N'[core].[SubAlcaldias]', 'U') IS NOT NULL
    DROP TABLE [core].[SubAlcaldias];
GO
IF OBJECT_ID(N'[core].[Direcciones]', 'U') IS NOT NULL
    DROP TABLE [core].[Direcciones];
GO
IF OBJECT_ID(N'[core].[Proyectos]', 'U') IS NOT NULL
    DROP TABLE [core].[Proyectos];
GO
IF OBJECT_ID(N'[core].[Personas_Responsable]', 'U') IS NOT NULL
    DROP TABLE [core].[Personas_Responsable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Personas'
CREATE TABLE [core].[Personas] (
    [IdPersona] int IDENTITY(1,1) NOT NULL,
    [Nombres] nvarchar(50)  NOT NULL,
    [Apellidos] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'SubAlcaldias'
CREATE TABLE [core].[SubAlcaldias] (
    [IdSubAlcaldia] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Direccion] nvarchar(500)  NOT NULL,
    [Zona] nvarchar(200)  NOT NULL,
    [Telefono] varchar(20)  NULL,
    [NombreSubAlcalde] nvarchar(80)  NULL
);
GO

-- Creating table 'Direcciones'
CREATE TABLE [core].[Direcciones] (
    [IdDireccion] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [CodigoInterno] varchar(50)  NOT NULL,
    [Ubicacion] nvarchar(500)  NULL
);
GO

-- Creating table 'Proyectos'
CREATE TABLE [core].[Proyectos] (
    [IdProyecto] int IDENTITY(1,1) NOT NULL,
    [IdDireccion] int  NOT NULL,
    [Nombre] nvarchar(200)  NOT NULL,
    [Descripcion] nvarchar(500)  NULL,
    [FechaCreacion] datetime  NOT NULL,
    [CostoEstimadoBs] decimal(12,3)  NULL
);
GO

-- Creating table 'Personas_Responsable'
CREATE TABLE [core].[Personas_Responsable] (
    [FechaAsignacion] datetime  NOT NULL,
    [IdPersona] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdPersona] in table 'Personas'
ALTER TABLE [core].[Personas]
ADD CONSTRAINT [PK_Personas]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- Creating primary key on [IdSubAlcaldia] in table 'SubAlcaldias'
ALTER TABLE [core].[SubAlcaldias]
ADD CONSTRAINT [PK_SubAlcaldias]
    PRIMARY KEY CLUSTERED ([IdSubAlcaldia] ASC);
GO

-- Creating primary key on [IdDireccion] in table 'Direcciones'
ALTER TABLE [core].[Direcciones]
ADD CONSTRAINT [PK_Direcciones]
    PRIMARY KEY CLUSTERED ([IdDireccion] ASC);
GO

-- Creating primary key on [IdProyecto] in table 'Proyectos'
ALTER TABLE [core].[Proyectos]
ADD CONSTRAINT [PK_Proyectos]
    PRIMARY KEY CLUSTERED ([IdProyecto] ASC);
GO

-- Creating primary key on [IdPersona] in table 'Personas_Responsable'
ALTER TABLE [core].[Personas_Responsable]
ADD CONSTRAINT [PK_Personas_Responsable]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdDireccion] in table 'Proyectos'
ALTER TABLE [core].[Proyectos]
ADD CONSTRAINT [FK_DireccionProyectoBase]
    FOREIGN KEY ([IdDireccion])
    REFERENCES [core].[Direcciones]
        ([IdDireccion])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DireccionProyectoBase'
CREATE INDEX [IX_FK_DireccionProyectoBase]
ON [core].[Proyectos]
    ([IdDireccion]);
GO

-- Creating foreign key on [IdPersona] in table 'Personas_Responsable'
ALTER TABLE [core].[Personas_Responsable]
ADD CONSTRAINT [FK_Responsable_inherits_Persona]
    FOREIGN KEY ([IdPersona])
    REFERENCES [core].[Personas]
        ([IdPersona])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------