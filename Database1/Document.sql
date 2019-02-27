﻿CREATE TABLE [dbo].[Document]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL, 
    [DocumentTypeId] INT NOT NULL, 
    [FilePath] NCHAR(300) NOT NULL, 
    [TopicId] INT NOT NULL, 
    CONSTRAINT [FK_Document_Topic] FOREIGN KEY ([TopicId]) REFERENCES [Topic]([Id]), 
    CONSTRAINT [FK_Document_DocumentType] FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentType]([Id])
)
