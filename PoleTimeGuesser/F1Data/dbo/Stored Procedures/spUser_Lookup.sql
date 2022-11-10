CREATE PROCEDURE [dbo].[spUser_Lookup]
	@Username NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id,Username,[Password],StoredSalt,Email,AvatarSourceName
	FROM [dbo].[Users]
	WHERE Username=@Username;
END
