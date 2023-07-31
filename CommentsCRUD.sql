-----CREATE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[AddComment]'))
	DROP PROCEDURE AddComment

GO
CREATE PROCEDURE AddComment
	@userID INT,
	@userName VARCHAR(20),
	@postID INT,
	@comment VARCHAR(200),
	@PKID int = 0 output
AS

BEGIN
	INSERT INTO Comments(userID, userName, postID , comment, dateTime)
		VALUES(@userID, @userName, @postID, @comment, GETDATE())

		SET @PKID = @@IDENTITY

	Select @PKID as PKID
END

------READ
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[AllComments]'))
	DROP PROCEDURE AllComments

GO
CREATE PROCEDURE AllComments
AS
BEGIN
	SELECT * FROM Comments;
END



------UPDATE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[UpdateComment]'))
	DROP PROCEDURE UpdateComment

GO
CREATE PROCEDURE UpdateComment
	@commentID INT,
	@comment VARCHAR(200)
AS
BEGIN
	UPDATE Comments
	SET comment = @comment
	WHERE commentID = @commentID
END

-----DELETE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[DeleteComment]'))
	DROP PROCEDURE DeleteComment

GO
CREATE PROCEDURE DeleteComment
	@commentID INT
AS
BEGIN
	DELETE FROM Comments
	WHERE commentID = @commentID
END