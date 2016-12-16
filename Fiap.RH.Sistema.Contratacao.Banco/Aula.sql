CREATE TABLE [dbo].[Aula]
(
    [IdFabrica] INT NOT NULL,
	[Id] INT NOT NULL IDENTITY,
    [Data] DATETIME2 NOT NULL, 
    [Horas] INT NOT NULL, 
    [Professor] NVARCHAR(100) NOT NULL, 
	CONSTRAINT [PK_Aula_Fabrica] PRIMARY KEY ([IdFabrica],[Id]),
    CONSTRAINT [FK_Aula_Fabrica] FOREIGN KEY ([IdFabrica]) REFERENCES [Fabrica]([Id])
)
