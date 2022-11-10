CREATE PROCEDURE [dbo].[spUser_Update]
	@Id INT,
	@Username NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@StoredSalt VARBINARY(50),
	@Email NVARCHAR(MAX),
	@AvatarSourceName NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[Users]
	SET Username=@Username,[Password]=@Password,StoredSalt=@StoredSalt,Email=@Email,AvatarSourceName=@AvatarSourceName
	WHERE Id=@Id;
END
