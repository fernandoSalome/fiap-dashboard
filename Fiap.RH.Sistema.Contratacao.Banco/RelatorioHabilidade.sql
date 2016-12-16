CREATE TABLE [dbo].[RelatorioHabilidade]
(
	[IdRelatorio] INT NOT NULL, 
    [IdHabilidade] INT NOT NULL,
	[IdOrdem] INT NOT NULL IDENTITY, 
    CONSTRAINT [PK_RelatorioHabilidade] PRIMARY KEY ([IdRelatorio], [IdOrdem]),
	CONSTRAINT [FK_RelatorioHabilidade_Relatorio] FOREIGN KEY ([IdRelatorio]) REFERENCES [Relatorio]([Id]),
	CONSTRAINT [FK_RelatorioHabilidade_Habilidade] FOREIGN KEY ([IdHabilidade]) REFERENCES [Habilidade]([Id]) 
)