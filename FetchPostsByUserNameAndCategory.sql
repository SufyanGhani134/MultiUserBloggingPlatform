----FETCH POST BY CATEGORY
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[PostByCategory]'))
	DROP PROCEDURE PostByCategory

GO
CREATE PROCEDURE PostByCategory
	@category VARCHAR(20)
AS
BEGIN
	SELECT * FROM Posts
	WHERE category = @category
END

----FETCH POST BY userName
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[PostByUserName]'))
	DROP PROCEDURE PostByUserName

GO
CREATE PROCEDURE PostByUserName
	@userName VARCHAR(20)
AS
BEGIN
	SELECT * FROM Posts
	WHERE userName = @userName
END

----FETCH POST BY userID
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[PostByUserID]'))
	DROP PROCEDURE PostByUserID

GO
CREATE PROCEDURE PostByUserID
	@userID INT
AS
BEGIN
	SELECT * FROM Posts
	WHERE userID = @userID
END



EXECUTE SignUp 'Sufyan', 'abc@example.com', '12345'

SELECT * FROM Users

EXECUTE AllUsers