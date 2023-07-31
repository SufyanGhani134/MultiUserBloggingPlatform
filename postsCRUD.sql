----CREATE
IF EXISTS (SELECT 1 FROM sys.procedures WHERE OBJECT_ID = OBJECT_ID(N'[dbo].CreatePost'))
	DROP PROCEDURE CreatePost
GO
CREATE PROCEDURE CreatePost
	@userID INT,
	@userName VARCHAR(20),
	@category VARCHAR(20),
	@Title VARCHAR(20),
	@Body VARCHAR(200),
	@PKID int = 0 output
AS
BEGIN
	INSERT INTO Posts (userID, userName, category, Title, Body, dateTime)
		VALUES (@userID, @userName, @category, @Title, @Body, GETDATE())
	SET @PKID = @@IDENTITY

	Select @PKID as PKID
END

----READ
IF EXISTS (SELECT 1 FROM sys.procedures WHERE OBJECT_ID = OBJECT_ID(N'[dbo].AllPosts'))
	DROP PROCEDURE AllPosts
GO
CREATE PROCEDURE AllPosts
AS
BEGIN
	SELECT p.postID,p.userID, p.userName, p.category, p.Title, p.Body, p.dateTime  FROM Posts p 
	LEFT JOIN Comments c ON p.postID = c.postID;
END

----UPDATE
IF EXISTS (SELECT 1 FROM sys.procedures WHERE OBJECT_ID = OBJECT_ID(N'[dbo].UpdatePost'))
	DROP PROCEDURE UpdatePost
GO
CREATE PROCEDURE UpdatePost
	@postID INT,
	@category VARCHAR(20),
	@Title VARCHAR(20),
	@Body VARCHAR(200)
AS
BEGIN
	UPDATE Posts
	SET category = @category, Title =@Title, Body=@Body
	WHERE postID = @postID
END

-----DELETE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[DeletePost]'))
	DROP PROCEDURE DeletePost

GO
CREATE PROCEDURE DeletePost
	@postID INT
AS
BEGIN
	DELETE FROM Comments
	WHERE postID = @postID
	DELETE FROM Posts
	WHERE postID = @postID
	
END