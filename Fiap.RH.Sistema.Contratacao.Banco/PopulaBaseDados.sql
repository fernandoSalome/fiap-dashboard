SET IDENTITY_INSERT [dbo].[Unidade] ON
INSERT INTO [dbo].[Unidade] ([Id], [Nome]) VALUES (1, N'FIAP Aclimação')
INSERT INTO [dbo].[Unidade] ([Id], [Nome]) VALUES (2, N'FIAP Vila Olímpia')
INSERT INTO [dbo].[Unidade] ([Id], [Nome]) VALUES (3, N'FIAP Paulista')
INSERT INTO [dbo].[Unidade] ([Id], [Nome]) VALUES (4, N'FIAP Alphaville')
INSERT INTO [dbo].[Unidade] ([Id], [Nome]) VALUES (5, N'COPI')
SET IDENTITY_INSERT [dbo].[Unidade] OFF

SET IDENTITY_INSERT [dbo].[PerfilProfissional] ON
INSERT INTO [dbo].[PerfilProfissional] ([Id], [Descricao]) VALUES (1, N'Front End')
INSERT INTO [dbo].[PerfilProfissional] ([Id], [Descricao]) VALUES (2, N'Back End')
INSERT INTO [dbo].[PerfilProfissional] ([Id], [Descricao]) VALUES (3, N'Full Stack')
INSERT INTO [dbo].[PerfilProfissional] ([Id], [Descricao]) VALUES (4, N'Designer')
SET IDENTITY_INSERT [dbo].[PerfilProfissional] OFF

SET IDENTITY_INSERT [dbo].[Login] ON
INSERT INTO [dbo].[Login] ([Id], [Nome], [Email], [Senha]) VALUES (1, N'Adm', N'adm@fiap.com.br', N'adm')
INSERT INTO [dbo].[Login] ([Id], [Nome], [Email], [Senha]) VALUES (2, N'Thiago Schetini', N'thiagoschetini@gmail.com', N'1234')
SET IDENTITY_INSERT [dbo].[Login] OFF

SET IDENTITY_INSERT [dbo].[Habilidade] ON
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (1, N'Autodesenvolvimento')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (2, N'Automotivação')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (3, N'Capacidade de Trabalhar Sob Pressão')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (4, N'Criatividade/Inovação')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (5, N'Empreendedorismo')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (6, N'Liderança')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (7, N'Negociação')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (8, N'Organização')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (9, N'Orientação ao Resultado')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (10, N'Planejamento')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (11, N'Relacionamento Interpessoal')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (12, N'Tomada de Decisão')
INSERT INTO [dbo].[Habilidade] ([Id], [Nome]) VALUES (13, N'Visão Sistêmica')
SET IDENTITY_INSERT [dbo].[Habilidade] OFF

SET IDENTITY_INSERT [dbo].[Fabrica] ON
INSERT INTO [dbo].[Fabrica] ([Id], [Nome], [NotaCorte]) VALUES (10, N'Fábrica Desenvolvedores 2016', CAST(6.00 AS Decimal(4, 2)))
INSERT INTO [dbo].[Fabrica] ([Id], [Nome], [NotaCorte]) VALUES (20, N'Fábrica de Designers 2016', CAST(7.50 AS Decimal(4, 2)))
INSERT INTO [dbo].[Fabrica] ([Id], [Nome], [NotaCorte]) VALUES (30, N'Fábrica de Disrupção 2017', CAST(8.00 AS Decimal(4, 2)))
SET IDENTITY_INSERT [dbo].[Fabrica] OFF

SET IDENTITY_INSERT [dbo].[Curso] ON
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (1, N'Análise e Desenvolvimento de Sistemas')
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (2, N'Sistemas de Informação')
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (3, N'Sistemas para Internet')
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (4, N'Banco de Dados - BI & Big Data')
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (5, N'Redes de Computadores')
INSERT INTO [dbo].[Curso] ([Id], [Nome]) VALUES (6, N'Engenharia da Computação')
SET IDENTITY_INSERT [dbo].[Curso] OFF

SET IDENTITY_INSERT [dbo].[Candidato] ON
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (1, N'Pedro Soares', 1, N'pedro@gmail.com', NULL, CAST(11999774321 AS Decimal(18, 0)), 1, 1, 1, CAST(8.67 AS Decimal(4, 2)), 1, CAST(77834 AS Decimal(10, 0)), 1, 10, 1, 1)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (2, N'Felipe Nery', 1, N'felip3@gmail.com', NULL, CAST(11987641234 AS Decimal(18, 0)), 1, 1, 1, CAST(9.10 AS Decimal(4, 2)), 1, CAST(88345 AS Decimal(10, 0)), 1, 10, 3, 2)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (3, N'Sema Silva', 0, N'selma@uol.com.br', CAST(1150778432 AS Decimal(18, 0)), CAST(11943286921 AS Decimal(18, 0)), 1, 1, 1, CAST(7.80 AS Decimal(4, 2)), 1, CAST(55543 AS Decimal(10, 0)), 2, 10, 2, 3)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (4, N'Jorge Silas', 1, N'jjorge@terra.com.br', NULL, NULL, 1, 0, 0, CAST(0.00 AS Decimal(4, 2)), 0, CAST(65437 AS Decimal(10, 0)), 3, 10, 1, 1)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (5, N'Washington Reis', 1, N'wwusa@gmail.com', NULL, NULL, 1, 1, 1, CAST(4.10 AS Decimal(4, 2)), 0, CAST(65431 AS Decimal(10, 0)), 1, 10, 1, 6)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (6, N'Cecilia Araújo', 0, N'ceceline@yahoo.com', NULL, NULL, 1, 1, 1, CAST(3.25 AS Decimal(4, 2)), 0, CAST(12345 AS Decimal(10, 0)), 2, 10, 2, 4)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (7, N'Thamiris Rodrigues', 0, N'thammy@gmail.com', NULL, CAST(11987653456 AS Decimal(18, 0)), 1, 1, 0, CAST(0.00 AS Decimal(4, 2)), 0, CAST(98763 AS Decimal(10, 0)), 3, 10, 1, 1)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (8, N'Kleberson Teodoro', 1, N'kleberson@gmail.com', NULL, NULL, 1, 1, 1, CAST(6.90 AS Decimal(4, 2)), 1, CAST(11155 AS Decimal(10, 0)), 1, 10, 3, 4)
INSERT INTO [dbo].[Candidato] ([Id], [Nome], [Publico], [Email], [Residencial], [Celular], [Cv], [Confirmacao], [ProvaTecnica], [Nota], [Aprovado], [Rm], [IdUnidade], [IdFabrica], [IdPerfilProfissional], [IdCurso]) VALUES (9, N'Roosevelt Watson', 1, N'roose@gmail.com', NULL, NULL, 1, 1, 1, CAST(6.76 AS Decimal(4, 2)), 1, CAST(44444 AS Decimal(10, 0)), 1, 10, 1, 1)
SET IDENTITY_INSERT [dbo].[Candidato] OFF

INSERT INTO [dbo].[Relatorio] ([Id], [Faltas], [AderenciaComportamental], [DesempenhoNota], [Status], [LoginId]) VALUES (1, 0, 68, CAST(6.00 AS Decimal(4, 2)), N'Indefinido', 1)
INSERT INTO [dbo].[Relatorio] ([Id], [Faltas], [AderenciaComportamental], [DesempenhoNota], [Status], [LoginId]) VALUES (2, 0, 72, CAST(7.12 AS Decimal(4, 2)), N'Aprovado', 2)
INSERT INTO [dbo].[Relatorio] ([Id], [Faltas], [AderenciaComportamental], [DesempenhoNota], [Status], [LoginId]) VALUES (3, 0, 56, CAST(5.12 AS Decimal(4, 2)), N'Reprovado', 2)
INSERT INTO [dbo].[Relatorio] ([Id], [Faltas], [AderenciaComportamental], [DesempenhoNota], [Status], [LoginId]) VALUES (8, 0, 91, CAST(8.50 AS Decimal(4, 2)), N'Aprovado', 2)
INSERT INTO [dbo].[Relatorio] ([Id], [Faltas], [AderenciaComportamental], [DesempenhoNota], [Status], [LoginId]) VALUES (9, 0, 77, CAST(6.95 AS Decimal(4, 2)), N'Indefinido', 1)

SET IDENTITY_INSERT [dbo].[RelatorioHabilidade] ON
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 4, 1)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 3, 2)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 7, 3)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 13, 4)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 11, 5)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 2, 6)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (1, 9, 7)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (2, 3, 8)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (2, 1, 9)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (2, 10, 10)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (3, 11, 11)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (3, 12, 12)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (8, 13, 13)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (8, 12, 14)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (8, 5, 15)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (8, 1, 16)
INSERT INTO [dbo].[RelatorioHabilidade] ([IdRelatorio], [IdHabilidade], [IdOrdem]) VALUES (9, 5, 17)
SET IDENTITY_INSERT [dbo].[RelatorioHabilidade] OFF

INSERT INTO [dbo].[AvaliacaoComportamental] ([Id], [JobCoach], [Video], [Redacao], [Linkedin], [Media], [AderenciaJobCoach]) VALUES (1, CAST(6.00 AS Decimal(4, 2)), CAST(7.00 AS Decimal(4, 2)), CAST(5.50 AS Decimal(4, 2)), CAST(8.10 AS Decimal(4, 2)), CAST(7.00 AS Decimal(4, 2)), CAST(7.40 AS Decimal(4, 2)))
INSERT INTO [dbo].[AvaliacaoComportamental] ([Id], [JobCoach], [Video], [Redacao], [Linkedin], [Media], [AderenciaJobCoach]) VALUES (2, CAST(7.00 AS Decimal(4, 2)), CAST(5.00 AS Decimal(4, 2)), CAST(3.90 AS Decimal(4, 2)), CAST(9.10 AS Decimal(4, 2)), CAST(7.00 AS Decimal(4, 2)), CAST(6.50 AS Decimal(4, 2)))
INSERT INTO [dbo].[AvaliacaoComportamental] ([Id], [JobCoach], [Video], [Redacao], [Linkedin], [Media], [AderenciaJobCoach]) VALUES (3, CAST(8.10 AS Decimal(4, 2)), CAST(8.34 AS Decimal(4, 2)), CAST(8.45 AS Decimal(4, 2)), CAST(4.00 AS Decimal(4, 2)), CAST(8.20 AS Decimal(4, 2)), CAST(7.90 AS Decimal(4, 2)))
INSERT INTO [dbo].[AvaliacaoComportamental] ([Id], [JobCoach], [Video], [Redacao], [Linkedin], [Media], [AderenciaJobCoach]) VALUES (8, CAST(10.00 AS Decimal(4, 2)), CAST(9.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)), CAST(5.00 AS Decimal(4, 2)), CAST(8.00 AS Decimal(4, 2)), CAST(7.50 AS Decimal(4, 2)))
INSERT INTO [dbo].[AvaliacaoComportamental] ([Id], [JobCoach], [Video], [Redacao], [Linkedin], [Media], [AderenciaJobCoach]) VALUES (9, CAST(7.75 AS Decimal(4, 2)), CAST(7.50 AS Decimal(4, 2)), CAST(7.00 AS Decimal(4, 2)), CAST(5.40 AS Decimal(4, 2)), CAST(7.00 AS Decimal(4, 2)), CAST(6.00 AS Decimal(4, 2)))