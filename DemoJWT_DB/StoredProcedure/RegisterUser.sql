CREATE PROCEDURE [dbo].[RegisterUser]
	@email VARCHAR(250),
	@pwd VARCHAR(100),
	@nickname VARCHAR(50)
AS
BEGIN
	DECLARE @salt VARCHAR(100)
	SET @salt = CONCAT(NEWID(), NEWID(), NEWID())

	DECLARE @key VARCHAR(200)
	SET @key = dbo.GetSecretKey()

	DECLARE @pwd_hash VARBINARY(64)
	SET @pwd_hash = HASHBYTES('SHA2_512', CONCAT(@salt, @key, @pwd, @salt))
	
	INSERT INTO Users (Email, Pwd, Salt, Nickname)
	VALUES (@email, @pwd_hash, @salt, @nickname)
END