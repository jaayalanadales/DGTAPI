
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/14/2021 23:41:13
-- Generated from EDMX file: C:\Users\josan\source\repos\DGTApp\DGTApp\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DGT];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ConductorVehiculo_Conductor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConductorVehiculo] DROP CONSTRAINT [FK_ConductorVehiculo_Conductor];
GO
IF OBJECT_ID(N'[dbo].[FK_ConductorVehiculo_Vehiculo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConductorVehiculo] DROP CONSTRAINT [FK_ConductorVehiculo_Vehiculo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Conductores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conductores];
GO
IF OBJECT_ID(N'[dbo].[Vehiculos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehiculos];
GO
IF OBJECT_ID(N'[dbo].[TiposInfraccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TiposInfraccion];
GO
IF OBJECT_ID(N'[dbo].[Infracciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Infracciones];
GO
IF OBJECT_ID(N'[dbo].[ConductorVehiculo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConductorVehiculo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Conductores'
CREATE TABLE [dbo].[Conductores] (
    [Dni] nvarchar(50)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellidos] nvarchar(max)  NOT NULL,
    [Puntos] int  NOT NULL
);
GO

-- Creating table 'Vehiculos'
CREATE TABLE [dbo].[Vehiculos] (
    [Matricula] nvarchar(50)  NOT NULL,
    [Marca] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TiposInfraccion'
CREATE TABLE [dbo].[TiposInfraccion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Puntos] int  NOT NULL
);
GO

-- Creating table 'Infracciones'
CREATE TABLE [dbo].[Infracciones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Dni] nvarchar(max)  NOT NULL,
    [Tipo] int  NOT NULL,
    [Fecha] datetime  NOT NULL
);
GO

-- Creating table 'ConductorVehiculo'
CREATE TABLE [dbo].[ConductorVehiculo] (
    [Conductor_Dni] nvarchar(50)  NOT NULL,
    [Vehiculo_Matricula] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Dni] in table 'Conductores'
ALTER TABLE [dbo].[Conductores]
ADD CONSTRAINT [PK_Conductores]
    PRIMARY KEY CLUSTERED ([Dni] ASC);
GO

-- Creating primary key on [Matricula] in table 'Vehiculos'
ALTER TABLE [dbo].[Vehiculos]
ADD CONSTRAINT [PK_Vehiculos]
    PRIMARY KEY CLUSTERED ([Matricula] ASC);
GO

-- Creating primary key on [Id] in table 'TiposInfraccion'
ALTER TABLE [dbo].[TiposInfraccion]
ADD CONSTRAINT [PK_TiposInfraccion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Infracciones'
ALTER TABLE [dbo].[Infracciones]
ADD CONSTRAINT [PK_Infracciones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Conductor_Dni], [Vehiculo_Matricula] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [PK_ConductorVehiculo]
    PRIMARY KEY CLUSTERED ([Conductor_Dni], [Vehiculo_Matricula] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Conductor_Dni] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [FK_ConductorVehiculo_Conductor]
    FOREIGN KEY ([Conductor_Dni])
    REFERENCES [dbo].[Conductores]
        ([Dni])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Vehiculo_Matricula] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [FK_ConductorVehiculo_Vehiculo]
    FOREIGN KEY ([Vehiculo_Matricula])
    REFERENCES [dbo].[Vehiculos]
        ([Matricula])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConductorVehiculo_Vehiculo'
CREATE INDEX [IX_FK_ConductorVehiculo_Vehiculo]
ON [dbo].[ConductorVehiculo]
    ([Vehiculo_Matricula]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------