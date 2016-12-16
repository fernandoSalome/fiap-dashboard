CREATE TABLE [dbo].[Relatorio]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[Faltas] INT NOT NULL, 
    [AderenciaComportamental] INT NOT NULL, 
    [DesempenhoNota] DECIMAL(4, 2) NOT NULL, 
    [Status] VARCHAR(20) NOT NULL, 
    [LoginId] INT NOT NULL, 
    CONSTRAINT [FK_Candidato_Relatorio] FOREIGN KEY ([Id]) REFERENCES [Candidato]([Id]),
	CONSTRAINT [FK_Relatorio_Login_Aprovacao] FOREIGN KEY ([LoginId]) REFERENCES [Login]([Id])
)


