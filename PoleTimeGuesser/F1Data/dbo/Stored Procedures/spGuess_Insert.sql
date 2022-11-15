CREATE PROCEDURE [dbo].[spGuess_Insert]
	@UserId INT,
	@Guess NVARCHAR(50),
	@EventId NVARCHAR(50),
	@DriverId NVARCHAR(50),
	@Difference NVARCHAR(50)

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Guesses](UserId,Guess,EventId,DriverId,[Difference])
	VALUES (@UserId,@Guess,@EventId,@DriverId,@Difference);
END
