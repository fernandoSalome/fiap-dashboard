CREATE TABLE [dbo].[Candidato]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] VARCHAR(100) NOT NULL, 
    [Publico] BIT NOT NULL, 
    [Email] VARCHAR(100) NOT NULL, 
    [Residencial] NUMERIC NULL, 
    [Celular] NUMERIC NULL, 
    [Cv] BIT NOT NULL, 
    [Confirmacao] BIT NOT NULL, 
    [ProvaTecnica] BIT NOT NULL, 
    [Nota] NUMERIC(4, 2) NOT NULL, 
    [Aprovado] BIT NOT NULL, 
    [Rm] NUMERIC(10) NULL, 
    [IdUnidade] INT NOT NULL, 
    [IdFabrica] INT NOT NULL, 
    [IdPerfilProfissional] INT NOT NULL, 
    [IdCurso] INT NOT NULL, 
    CONSTRAINT [FK_Candidato_Unidade] FOREIGN KEY ([IdUnidade]) REFERENCES [Unidade]([Id]),
	CONSTRAINT [FK_Candidato_Fabrica] FOREIGN KEY ([IdFabrica]) REFERENCES [Fabrica]([Id]), 
	CONSTRAINT [FK_Candidato_Perfil] FOREIGN KEY ([IdPerfilProfissional]) REFERENCES [PerfilProfissional]([Id]),
	CONSTRAINT [FK_Candidato_Curso] FOREIGN KEY ([IdCurso]) REFERENCES [Curso]([Id])
)
