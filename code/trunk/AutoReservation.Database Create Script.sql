PRINT N'Creating Tables';
PRINT N'-------------------';
PRINT N'';

PRINT N'Creating Database...';

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'AutoReservation')
DROP DATABASE AutoReservation
CREATE DATABASE AutoReservation

GO

USE [AutoReservation]


PRINT N'Creating dbo.Reservation...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Reservation')
DROP TABLE [dbo].[Reservation]
CREATE TABLE [dbo].[Reservation] (
    [Id]      INT      IDENTITY (1, 1) NOT NULL,
    [AutoId]  INT      NOT NULL,
    [KundeId] INT      NOT NULL,
    [Von]     DATETIME NOT NULL,
    [Bis]     DATETIME NOT NULL
);


PRINT N'Creating dbo.Auto...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Auto')
DROP TABLE [dbo].[Auto]
CREATE TABLE [dbo].[Auto] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Marke]      NVARCHAR (20) NOT NULL,
    [AutoKlasse] INT           NOT NULL,
    [Tagestarif] INT           NOT NULL,
    [Basistarif] INT           NULL
);


PRINT N'Creating dbo.Kunde...';

IF EXISTS(SELECT name FROM sys.tables WHERE name = 'Kunde')
DROP TABLE [dbo].[Kunde]
CREATE TABLE [dbo].[Kunde] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Nachname]     NVARCHAR (20) NOT NULL,
    [Vorname]      NVARCHAR (20) NOT NULL,
    [Geburtsdatum] DATETIME      NOT NULL
) ON [PRIMARY];


PRINT N'Creating dbo.PK_Auto...';

ALTER TABLE [dbo].[Auto]
    ADD CONSTRAINT [PK_Auto] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Kunde...';

ALTER TABLE [dbo].[Kunde]
    ADD CONSTRAINT [PK_Kunde] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.PK_Reservation...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];

PRINT N'Creating dbo.FK_Reservation_Auto...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_Reservation_Auto] FOREIGN KEY ([AutoId]) REFERENCES [dbo].[Auto] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

PRINT N'Creating dbo.FK_Reservation_Kunde...';

ALTER TABLE [dbo].[Reservation]
    ADD CONSTRAINT [FK_Reservation_Kunde] FOREIGN KEY ([KundeId]) REFERENCES [dbo].[Kunde] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

PRINT N'';
PRINT N'-------------------';
PRINT N'Script end...';