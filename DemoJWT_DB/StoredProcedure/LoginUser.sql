CREATE PROCEDURE [dbo].[LoginUser]
	@email VARCHAR(250),
	@pwd VARCHAR(100)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = (SELECT salt FROM Users WHERE Email = @email)

	DECLARE @key VARCHAR(200)
	SET @key = dbo.GetSecretKey()

	DECLARE @pwd_hash VARBINARY(64)
	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @key, @pwd, @salt))

	SELECT Id, Email, IsAdmin, Nickname 
	FROM Users
	WHERE Email = @email AND Pwd = @pwd_hash
END