
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/21/2013 01:46:26
-- Generated from EDMX file: C:\Users\Компьютер\Documents\Visual Studio 2012\Projects\FootballSchool\FootballSchool\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [fsc];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GameEvents_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_GameEvents_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_GameEvents_Players]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_Players];
GO
IF OBJECT_ID(N'[dbo].[FK_Games_Referee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Games_Referee];
GO
IF OBJECT_ID(N'[dbo].[FK_Games_Team1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Games_Team1];
GO
IF OBJECT_ID(N'[dbo].[FK_Games_Teams]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Games_Teams];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerInTeam_Players]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlayerInTeam] DROP CONSTRAINT [FK_PlayerInTeam_Players];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerInTeam_Team]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlayerInTeam] DROP CONSTRAINT [FK_PlayerInTeam_Team];
GO
IF OBJECT_ID(N'[dbo].[FK_Players_PlayerPositions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players] DROP CONSTRAINT [FK_Players_PlayerPositions];
GO
IF OBJECT_ID(N'[dbo].[FK_Teams_Coaches]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_Teams_Coaches];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Coaches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coaches];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[GameEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameEvents];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO
IF OBJECT_ID(N'[dbo].[PlayerInTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerInTeam];
GO
IF OBJECT_ID(N'[dbo].[PlayerPositions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlayerPositions];
GO
IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Referee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Referee];
GO
IF OBJECT_ID(N'[dbo].[Teams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Coaches'
CREATE TABLE [dbo].[Coaches] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'GameEvents'
CREATE TABLE [dbo].[GameEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameID] int  NOT NULL,
    [PlayerID] int  NOT NULL,
    [EventID] int  NOT NULL,
    [Time] datetime  NOT NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Team1ID] int  NOT NULL,
    [Team2ID] int  NOT NULL,
    [Stadium] nvarchar(50)  NULL,
    [Type] bit  NOT NULL,
    [RefereeID] int  NOT NULL
);
GO

-- Creating table 'PlayerInTeam'
CREATE TABLE [dbo].[PlayerInTeam] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PlayerID] int  NOT NULL,
    [TeamID] int  NOT NULL
);
GO

-- Creating table 'PlayerPositions'
CREATE TABLE [dbo].[PlayerPositions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Position] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [Passport] nchar(10)  NOT NULL,
    [IsMain] bit  NOT NULL,
    [PositionID] int  NOT NULL
);
GO

-- Creating table 'Referee'
CREATE TABLE [dbo].[Referee] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Qualify] nvarchar(50)  NULL
);
GO

-- Creating table 'Teams'
CREATE TABLE [dbo].[Teams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [CoachID] int  NOT NULL,
    [City] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Coaches'
ALTER TABLE [dbo].[Coaches]
ADD CONSTRAINT [PK_Coaches]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GameEvents'
ALTER TABLE [dbo].[GameEvents]
ADD CONSTRAINT [PK_GameEvents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlayerInTeam'
ALTER TABLE [dbo].[PlayerInTeam]
ADD CONSTRAINT [PK_PlayerInTeam]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PlayerPositions'
ALTER TABLE [dbo].[PlayerPositions]
ADD CONSTRAINT [PK_PlayerPositions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Referee'
ALTER TABLE [dbo].[Referee]
ADD CONSTRAINT [PK_Referee]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [PK_Teams]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CoachID] in table 'Teams'
ALTER TABLE [dbo].[Teams]
ADD CONSTRAINT [FK_Teams_Coaches]
    FOREIGN KEY ([CoachID])
    REFERENCES [dbo].[Coaches]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Teams_Coaches'
CREATE INDEX [IX_FK_Teams_Coaches]
ON [dbo].[Teams]
    ([CoachID]);
GO

-- Creating foreign key on [EventID] in table 'GameEvents'
ALTER TABLE [dbo].[GameEvents]
ADD CONSTRAINT [FK_GameEvents_Event]
    FOREIGN KEY ([EventID])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameEvents_Event'
CREATE INDEX [IX_FK_GameEvents_Event]
ON [dbo].[GameEvents]
    ([EventID]);
GO

-- Creating foreign key on [GameID] in table 'GameEvents'
ALTER TABLE [dbo].[GameEvents]
ADD CONSTRAINT [FK_GameEvents_Game]
    FOREIGN KEY ([GameID])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameEvents_Game'
CREATE INDEX [IX_FK_GameEvents_Game]
ON [dbo].[GameEvents]
    ([GameID]);
GO

-- Creating foreign key on [PlayerID] in table 'GameEvents'
ALTER TABLE [dbo].[GameEvents]
ADD CONSTRAINT [FK_GameEvents_Players]
    FOREIGN KEY ([PlayerID])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameEvents_Players'
CREATE INDEX [IX_FK_GameEvents_Players]
ON [dbo].[GameEvents]
    ([PlayerID]);
GO

-- Creating foreign key on [RefereeID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Games_Referee]
    FOREIGN KEY ([RefereeID])
    REFERENCES [dbo].[Referee]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Games_Referee'
CREATE INDEX [IX_FK_Games_Referee]
ON [dbo].[Games]
    ([RefereeID]);
GO

-- Creating foreign key on [Team1ID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Games_Team1]
    FOREIGN KEY ([Team1ID])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Games_Team1'
CREATE INDEX [IX_FK_Games_Team1]
ON [dbo].[Games]
    ([Team1ID]);
GO

-- Creating foreign key on [Team2ID] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [FK_Games_Teams]
    FOREIGN KEY ([Team2ID])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Games_Teams'
CREATE INDEX [IX_FK_Games_Teams]
ON [dbo].[Games]
    ([Team2ID]);
GO

-- Creating foreign key on [PlayerID] in table 'PlayerInTeam'
ALTER TABLE [dbo].[PlayerInTeam]
ADD CONSTRAINT [FK_PlayerInTeam_Players]
    FOREIGN KEY ([PlayerID])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerInTeam_Players'
CREATE INDEX [IX_FK_PlayerInTeam_Players]
ON [dbo].[PlayerInTeam]
    ([PlayerID]);
GO

-- Creating foreign key on [TeamID] in table 'PlayerInTeam'
ALTER TABLE [dbo].[PlayerInTeam]
ADD CONSTRAINT [FK_PlayerInTeam_Team]
    FOREIGN KEY ([TeamID])
    REFERENCES [dbo].[Teams]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerInTeam_Team'
CREATE INDEX [IX_FK_PlayerInTeam_Team]
ON [dbo].[PlayerInTeam]
    ([TeamID]);
GO

-- Creating foreign key on [PositionID] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_Players_PlayerPositions]
    FOREIGN KEY ([PositionID])
    REFERENCES [dbo].[PlayerPositions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Players_PlayerPositions'
CREATE INDEX [IX_FK_Players_PlayerPositions]
ON [dbo].[Players]
    ([PositionID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------