-----CREATE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[SignUp]'))
	DROP PROCEDURE SignUp

GO
CREATE PROCEDURE SignUp
	@userName VARCHAR(20),
	@email VARCHAR(50),
	@password VARCHAR (50),
	@PKID int = 0 output
AS

BEGIN
	INSERT INTO Users(userName, email , password)
		VALUES(@userName, @email, @password)
	
	SET @PKID = @@IDENTITY

	Select @PKID as PKID
END

------READ
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[AllUsers]'))
	DROP PROCEDURE AllUsers

GO
CREATE PROCEDURE AllUsers
AS
BEGIN
	SELECT userID, userName, email, password FROM users;
END



------UPDATE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[UpdateUser]'))
	DROP PROCEDURE UpdateUser

GO
CREATE PROCEDURE UpdateUser
	@userID INT,
	@userName VARCHAR(20),
	@email VARCHAR(50),
	@password VARCHAR(50)
AS
BEGIN
	UPDATE Users
	SET userName = @userName, email = @email, password = @password
	WHERE userID = @userID
END

-----DELETE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[DeleteUser]'))
	DROP PROCEDURE DeleteUser

GO
CREATE PROCEDURE DeleteUser
	@userID INT
AS
BEGIN
	DELETE FROM Users
	WHERE userID = @userID
END