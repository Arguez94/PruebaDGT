
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/27/2021 01:52:59
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DgtDataBase];
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
IF OBJECT_ID(N'[dbo].[FK_ConductorInfraccion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Infraccion] DROP CONSTRAINT [FK_ConductorInfraccion];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculoInfraccion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Infraccion] DROP CONSTRAINT [FK_VehiculoInfraccion];
GO
IF OBJECT_ID(N'[dbo].[FK_TipoInfraccionInfraccion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Infraccion] DROP CONSTRAINT [FK_TipoInfraccionInfraccion];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Conductor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conductor];
GO
IF OBJECT_ID(N'[dbo].[Vehiculo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehiculo];
GO
IF OBJECT_ID(N'[dbo].[TipoInfraccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoInfraccion];
GO
IF OBJECT_ID(N'[dbo].[Infraccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Infraccion];
GO
IF OBJECT_ID(N'[dbo].[ConductorVehiculo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConductorVehiculo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Conductor'
CREATE TABLE [dbo].[Conductor] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DNI] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Apellidos] nvarchar(max)  NOT NULL,
    [Puntos] int  NOT NULL
);
GO

-- Creating table 'Vehiculo'
CREATE TABLE [dbo].[Vehiculo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Matricula] nvarchar(max)  NOT NULL,
    [Marca] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TipoInfraccion'
CREATE TABLE [dbo].[TipoInfraccion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Identificador] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [PuntosDescuento] int  NOT NULL
);
GO

-- Creating table 'Infraccion'
CREATE TABLE [dbo].[Infraccion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Identificador] nvarchar(max)  NOT NULL,
    [FechaYHora] datetime  NOT NULL,
    [ConductorId] int  NOT NULL,
    [VehiculoId] int  NOT NULL,
    [TipoInfraccion_Id] int  NOT NULL
);
GO

-- Creating table 'ConductorVehiculo'
CREATE TABLE [dbo].[ConductorVehiculo] (
    [Conductor_Id] int  NOT NULL,
    [Vehiculo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Conductor'
ALTER TABLE [dbo].[Conductor]
ADD CONSTRAINT [PK_Conductor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vehiculo'
ALTER TABLE [dbo].[Vehiculo]
ADD CONSTRAINT [PK_Vehiculo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TipoInfraccion'
ALTER TABLE [dbo].[TipoInfraccion]
ADD CONSTRAINT [PK_TipoInfraccion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Infraccion'
ALTER TABLE [dbo].[Infraccion]
ADD CONSTRAINT [PK_Infraccion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Conductor_Id], [Vehiculo_Id] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [PK_ConductorVehiculo]
    PRIMARY KEY CLUSTERED ([Conductor_Id], [Vehiculo_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Conductor_Id] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [FK_ConductorVehiculo_Conductor]
    FOREIGN KEY ([Conductor_Id])
    REFERENCES [dbo].[Conductor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Vehiculo_Id] in table 'ConductorVehiculo'
ALTER TABLE [dbo].[ConductorVehiculo]
ADD CONSTRAINT [FK_ConductorVehiculo_Vehiculo]
    FOREIGN KEY ([Vehiculo_Id])
    REFERENCES [dbo].[Vehiculo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConductorVehiculo_Vehiculo'
CREATE INDEX [IX_FK_ConductorVehiculo_Vehiculo]
ON [dbo].[ConductorVehiculo]
    ([Vehiculo_Id]);
GO

-- Creating foreign key on [ConductorId] in table 'Infraccion'
ALTER TABLE [dbo].[Infraccion]
ADD CONSTRAINT [FK_ConductorInfraccion]
    FOREIGN KEY ([ConductorId])
    REFERENCES [dbo].[Conductor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConductorInfraccion'
CREATE INDEX [IX_FK_ConductorInfraccion]
ON [dbo].[Infraccion]
    ([ConductorId]);
GO

-- Creating foreign key on [VehiculoId] in table 'Infraccion'
ALTER TABLE [dbo].[Infraccion]
ADD CONSTRAINT [FK_VehiculoInfraccion]
    FOREIGN KEY ([VehiculoId])
    REFERENCES [dbo].[Vehiculo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculoInfraccion'
CREATE INDEX [IX_FK_VehiculoInfraccion]
ON [dbo].[Infraccion]
    ([VehiculoId]);
GO

-- Creating foreign key on [TipoInfraccion_Id] in table 'Infraccion'
ALTER TABLE [dbo].[Infraccion]
ADD CONSTRAINT [FK_TipoInfraccionInfraccion]
    FOREIGN KEY ([TipoInfraccion_Id])
    REFERENCES [dbo].[TipoInfraccion]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoInfraccionInfraccion'
CREATE INDEX [IX_FK_TipoInfraccionInfraccion]
ON [dbo].[Infraccion]
    ([TipoInfraccion_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------