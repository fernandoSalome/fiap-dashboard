﻿CREATE TABLE [dbo].[Login]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] VARCHAR(100) NOT NULL, 
    [Email] VARCHAR(100) NOT NULL, 
    [Senha] VARCHAR(100) NOT NULL
)
