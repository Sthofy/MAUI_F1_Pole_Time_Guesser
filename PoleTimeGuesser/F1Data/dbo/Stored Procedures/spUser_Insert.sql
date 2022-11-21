CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id INT,
	@Username NVARCHAR(MAX),
	@Password NVARCHAR(MAX),
	@StoredSalt VARBINARY(50),
	@Email NVARCHAR(MAX),
	@AvatarSourceName NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Users](Username,[Password],StoredSalt,Email,AvatarSourceName)
	VALUES (@Username,@Password,@StoredSalt,@Email,@AvatarSourceName);

	SELECT SCOPE_IDENTITY();
END

