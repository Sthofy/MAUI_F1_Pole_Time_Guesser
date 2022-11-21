CREATE PROCEDURE [dbo].[spUsersScoreboard_GetByUserId]
	@UserId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT UserId,Score
	FROM [dbo].[UsersScoreboard]
	WHERE UserId=@UserId;
END
