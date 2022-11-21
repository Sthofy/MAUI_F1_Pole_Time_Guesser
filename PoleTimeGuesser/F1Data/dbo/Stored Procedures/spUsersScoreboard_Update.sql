CREATE PROCEDURE [dbo].[spUsersScoreboard_Update]
	@UserId INT,
	@Score INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[UsersScoreboard]
	SET Score=@Score
	WHERE UserId=@UserId;
END
