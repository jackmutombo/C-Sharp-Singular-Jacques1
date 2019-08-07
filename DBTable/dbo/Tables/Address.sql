CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Type] NVARCHAR(50) NOT NULL, 
    [AddressName] NVARCHAR(200) NOT NULL, 
    [Street] NVARCHAR(100) NOT NULL, 
    [Suburb] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(100) NOT NULL, 
    [PostalCode] NVARCHAR(50) NOT NULL, 
    [AuthUserId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [FK_Address_ToUser] FOREIGN KEY ([AuthUserId]) REFERENCES [User]([AuthUserId])
)
