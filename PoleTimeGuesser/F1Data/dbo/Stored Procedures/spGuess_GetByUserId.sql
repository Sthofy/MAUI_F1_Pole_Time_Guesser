CREATE PROCEDURE [dbo].[spGuess_GetByUserId]
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,Guess,EventId,DriverId,[Difference]
	FROM [dbo].[Guesses]
	WHERE UserId = @UserId;
END
