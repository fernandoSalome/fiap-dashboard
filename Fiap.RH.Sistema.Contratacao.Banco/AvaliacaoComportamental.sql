CREATE TABLE [dbo].[AvaliacaoComportamental]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [JobCoach] DECIMAL(4, 2) NOT NULL, 
    [Video] DECIMAL(4, 2) NOT NULL, 
    [Redacao] DECIMAL(4, 2) NOT NULL, 
    [Linkedin] DECIMAL(4, 2) NOT NULL, 
    [Media] DECIMAL(4, 2) NOT NULL, 
    [AderenciaJobCoach] DECIMAL(4, 2) NOT NULL, 
    CONSTRAINT [FK_AvaliacaoComportamental_Candidato] FOREIGN KEY ([Id]) REFERENCES [Candidato]([Id])
)
