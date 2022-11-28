CREATE PROCEDURE [dbo].[spUser_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,Username,[Password],Email,AvatarSourceName
	FROM [dbo].[Users]
	WHERE Id=@Id;
END
