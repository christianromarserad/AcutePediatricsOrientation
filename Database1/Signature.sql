CREATE TABLE [dbo].[Signature]
(
	[UserId] INT NOT NULL, 
    [TopicId] INT NOT NULL, 
    [Date] DATE NOT NULL
	CONSTRAINT PK_Person PRIMARY KEY ([UserId],[TopicId])
)
