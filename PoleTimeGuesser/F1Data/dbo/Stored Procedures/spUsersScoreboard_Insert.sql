CREATE PROCEDURE [dbo].[spUsersScoreboard_Insert]
	@UserId INT,
	@Score INT
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[UsersScoreboard](UserId,Score)
	VALUES (@UserId,@Score);
END
