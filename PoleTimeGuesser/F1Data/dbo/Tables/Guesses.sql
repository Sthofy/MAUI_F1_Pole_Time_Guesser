CREATE TABLE [dbo].[Guesses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [Guess] NVARCHAR(50) NOT NULL , 
    [EventId] INT NOT NULL, 
    CONSTRAINT [FK_Guesses_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) 
)
