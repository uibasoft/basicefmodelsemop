
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/31/2016 00:15:43
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

-- Creating primary key on [IdPersona] in table 'Personas_Responsable'
ALTER TABLE [core].[Personas_Responsable]
ADD CONSTRAINT [PK_Personas_Responsable]
    PRIMARY KEY CLUSTERED ([IdPersona] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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