-----CREATE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[createCategory]'))
	DROP PROCEDURE createCategory

GO
CREATE PROCEDURE createCategory
	@category VARCHAR(20)
AS

BEGIN
	INSERT INTO Categories(category)
		VALUES(@category)
END


------READ
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[AllCategories]'))
	DROP PROCEDURE AllCategories

GO
CREATE PROCEDURE AllCategories
AS
BEGIN
	SELECT * FROM Categories;
END


-----DELETE
IF EXISTS (SELECT 1 FROM sys.procedures where OBJECT_ID = OBJECT_ID(N'[dbo].[DeleteCategory]'))
	DROP PROCEDURE DeleteCategory

GO
CREATE PROCEDURE DeleteCategory
	@category VARCHAR(20)
AS
BEGIN
	DELETE FROM Categories
	WHERE category = @category
END