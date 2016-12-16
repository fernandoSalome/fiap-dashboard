CREATE TABLE [dbo].[CandidatoAula]
( 
	[IdFabrica] INT NOT NULL,
	[IdAula] INT NOT NULL,
	[IdCandidato] INT NOT NULL,
    [Presenca] BIT NOT NULL,
	CONSTRAINT [PK_FabricaAulaCandidato] PRIMARY KEY ([IdFabrica], [IdCandidato], [IdAula]),
	CONSTRAINT [FK_FabricaAula] FOREIGN KEY ([IdFabrica], [IdAula]) REFERENCES [Aula]([IdFabrica],[Id]),
	CONSTRAINT [FK_CandidatoAula_Candidato] FOREIGN KEY ([IdCandidato]) REFERENCES [Candidato]([Id])
)
