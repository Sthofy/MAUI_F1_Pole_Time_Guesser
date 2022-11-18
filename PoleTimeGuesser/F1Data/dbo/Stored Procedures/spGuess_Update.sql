CREATE PROCEDURE [dbo].[spGuess_Update]
	@UserId INT,
	@Difference NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Guesses]
	SET [Difference]=@Difference
	WHERE UserId=@UserId;
END
