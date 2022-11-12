CREATE PROCEDURE [dbo].[spUsers_GetByUsername]
	@Username VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Username
	FROM [dbo].[Users]
	WHERE Username=@Username;
END
