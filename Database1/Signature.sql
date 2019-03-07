CREATE TABLE [dbo].[Signature]
(
	[UserId] INT NOT NULL, 
    [TopicId] INT NOT NULL, 
    [Date] DATE NOT NULL
	CONSTRAINT PK_Person PRIMARY KEY ([UserId],[TopicId]), 
    CONSTRAINT [FK_Signature_User] FOREIGN KEY ([UserId]) REFERENCES [Account]([Id]), 
    CONSTRAINT [FK_Signature_Topic] FOREIGN KEY ([TopicId]) REFERENCES [Topic]([Id])
)
