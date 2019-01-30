CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] NCHAR(50) NOT NULL, 
    [Password] NCHAR(50) NOT NULL, 
    [Role] NCHAR(50) NOT NULL
)
