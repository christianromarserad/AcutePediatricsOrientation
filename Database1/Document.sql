CREATE TABLE [dbo].[Document]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL, 
    [Type] NCHAR(50) NOT NULL, 
    [FilePath] NCHAR(50) NOT NULL, 
    [TopicId] INT NOT NULL, 
    CONSTRAINT [FK_Document_Topic] FOREIGN KEY ([TopicId]) REFERENCES [Topic]([Id])
)
