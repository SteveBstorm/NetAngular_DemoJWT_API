CREATE PROCEDURE [dbo].[SwitchRole]
	@id INT
AS
BEGIN
	DECLARE @isAdmin BIT
	SELECT @isAdmin = IsAdmin FROM Users WHERE Id = @id
	
	IF @isAdmin = 1
	BEGIN
		UPDATE Users SET IsAdmin = 0 WHERE Id = @id
	END
	ELSE
	BEGIN
		UPDATE Users SET IsAdmin = 1 WHERE Id = @id
	END

END

