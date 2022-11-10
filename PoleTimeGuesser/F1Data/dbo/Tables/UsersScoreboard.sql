CREATE TABLE [dbo].[UsersScoreboard]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [Score] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_UsersScoreboard_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
