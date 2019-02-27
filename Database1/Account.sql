CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [Username] NCHAR(50) NOT NULL, 
    [Password] NCHAR(50) NOT NULL, 
    [RoleId] INT NOT NULL, 
    CONSTRAINT [FK_Account_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
)
