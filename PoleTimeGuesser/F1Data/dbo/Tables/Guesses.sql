CREATE TABLE [dbo].[Guesses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [Guess] NVARCHAR(50) NOT NULL , 
    [EventId] NVARCHAR(50) NOT NULL, 
    [DriverId] NVARCHAR(50) NOT NULL, 
    [Difference] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Guesses_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) 
)
