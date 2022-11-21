CREATE PROCEDURE [dbo].[spGuess_UpdateDiff]
	@UserId INT,
	@EventId NVARCHAR(50),
	@Difference NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Guesses]
	SET [Difference]=@Difference
	WHERE UserId=@UserId;
END
