CREATE PROCEDURE [dbo].[spGuess_Update]
	@Id INT,
	@Difference NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Guesses]
	SET [Difference]=@Difference
	WHERE Id=@Id;
END
