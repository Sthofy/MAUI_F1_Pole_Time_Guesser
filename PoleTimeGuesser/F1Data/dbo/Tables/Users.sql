CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(MAX) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [StoredSalt] VARBINARY(50) NOT NULL, 
    [Email] NVARCHAR(MAX) NOT NULL, 
    [AvatarSourceName] NVARCHAR(MAX) NOT NULL
)
