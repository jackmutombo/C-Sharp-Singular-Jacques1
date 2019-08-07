CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL IDENTITY, 
    [AuthUserId] NVARCHAR(128) NOT NULL PRIMARY KEY , 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(256) NOT NULL, 
    [CellPhone] VARCHAR(20) NOT NULL, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
