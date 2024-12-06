CREATE TABLE [dbo].[Processes] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [NPU]              CHAR (25)      NOT NULL,
    [RegistrationDate] DATETIME       DEFAULT (getdate()) NOT NULL,
    [ViewDate]         DATETIME       NULL,
    [State]            CHAR (2)       NOT NULL,
    [City]             NVARCHAR (100) NOT NULL,
    [CityCode]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
