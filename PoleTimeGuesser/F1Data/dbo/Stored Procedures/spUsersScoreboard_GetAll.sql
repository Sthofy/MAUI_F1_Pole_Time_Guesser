CREATE PROCEDURE [dbo].[spUsersScoreboard_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT us.Score,u.Username
	FROM [dbo].[UsersScoreboard] us
	INNER JOIN [dbo].Users u on us.UserId=u.Id
	ORDER BY Score DESC;
END
