/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Insert static data for Role table
DELETE FROM [dbo].[Role]
INSERT INTO [dbo].[Role]
VALUES(1, 'Educator')
INSERT INTO [dbo].[Role]
VALUES(2, 'Staff')
GO

-- Insert static date for the DocumentType table
DELETE FROM [dbo].[DocumentType]
INSERT INTO [dbo].[DocumentType]
VALUES(1, 'PDF')
INSERT INTO [dbo].[DocumentType]
VALUES(2, 'Video')
GO

-- An initial admin account used only for development
INSERT INTO [dbo].[Account]([Username], [Password], [RoleId])
VALUES('Admin', 'Admin', 1)
GO
