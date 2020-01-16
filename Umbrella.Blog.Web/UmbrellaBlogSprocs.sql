USE UmbrellaBlog
GO


-----------POSTSTAGS TABLE SPROCS-----------------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetPostsTags')
	DROP PROCEDURE GetPostsTags
GO

CREATE PROCEDURE GetPostsTags (
	@PostId INT
	)
	as
BEGIN	
	SELECT PostsTags.TagId, TagText 
	FROM PostsTags
	INNER JOIN Tags ON PostsTags.TagId = Tags.TagId
	WHERE PostId = @PostId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsTagsInsert')
	DROP PROCEDURE PostsTagsInsert
GO

CREATE PROCEDURE PostsTagsInsert (
	@TagId INT,
	@PostId INT
	)
	as
BEGIN	
	IF not exists (SELECT PostTagId FROM PostsTags WHERE PostId = @PostId AND TagId = @TagId)
	INSERT INTO PostsTags (TagId, PostId)
	VALUES (@TagId, @PostId)
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsTagsDelete')
	DROP PROCEDURE PostsTagsDelete
GO

CREATE PROCEDURE PostsTagsDelete (
	@TagId INT,
	@PostId INT
	)
	as
BEGIN	
	IF exists (SELECT PostTagId FROM PostsTags WHERE PostId = @PostId AND TagId = @TagId)
	DELETE
	FROM PostsTags
	WHERE PostId = @PostId AND TagId = @TagId 
END
GO


-----------POSTS TABLE SPROCS------------------------------------


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'GetTopPosts')
	DROP PROCEDURE GetTopPosts
GO

CREATE PROCEDURE GetTopPosts AS
BEGIN
	SELECT TOP 3 
	PostTitle,PostText,PostDate from dbo.Posts
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsSelectAll')
	DROP PROCEDURE PostsSelectAll
GO
CREATE PROCEDURE PostsSelectAll AS
BEGIN 
	SELECT *	
	FROM Posts 
	ORDER BY PostDate DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsSelectbyFilter')
	DROP PROCEDURE PostsSelectbyFilter
GO
CREATE PROCEDURE PostsSelectbyFilter AS
BEGIN 
	SELECT *	
	FROM Posts 
	ORDER BY PostDate DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsSelectById')
	DROP PROCEDURE PostsSelectById
GO

CREATE PROCEDURE PostsSelectById(
	@PostId int
) AS
BEGIN
	SELECT *	
	FROM Posts
	WHERE PostId = @PostId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsSelectByCategory')
	DROP PROCEDURE PostsSelectByCategory
GO

CREATE PROCEDURE PostsSelectByCategory(
	@CategoryId int
) AS
BEGIN
	SELECT *	
	FROM Posts
	WHERE CategoryId = @CategoryId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsSelectByTag')
	DROP PROCEDURE PostsSelectByTag
GO

CREATE PROCEDURE PostsSelectByTag(
	@TagId int
) AS
BEGIN
	SELECT *	
	FROM PostsTags
	INNER JOIN Posts ON PostsTags.PostId = Posts.PostId
	WHERE TagId = @TagId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsInsert')
	DROP PROCEDURE PostsInsert
GO

CREATE PROCEDURE PostsInsert (
	@PostText NVARCHAR(max),
	@PostTitle NVARCHAR(50) = NULL, 
	@Approved BIT = 0,
	@PostDate DATETIME2,
	@ExpirationDate DATETIME2 = NULL,
	@CategoryId INT = 1,
	@UserId NVARCHAR(128),
	@PostId INT OUTPUT
	)
	as
BEGIN	
	INSERT INTO Posts (PostText, PostTitle, Approved, PostDate, ExpirationDate, CategoryId, UserId)
	VALUES (@PostText, @PostTitle, @Approved, @PostDate, @ExpirationDate, @CategoryId, @UserId)

	SET @PostId = @@IDENTITY;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsUpdate')
	DROP PROCEDURE PostsUpdate
GO

CREATE PROCEDURE PostsUpdate (
	@PostId INT,
	@PostTitle NVARCHAR(50) = NULL,
	@PostText NVARCHAR(max),
	@Approved BIT,
	@PostDate DATETIME2,
	@ExpirationDate DATETIME2,
	@CategoryId INT,
	@UserId NVARCHAR(128)
	)
	as
BEGIN
	UPDATE Posts SET
	PostTitle = @PostTitle,
	PostText = @PostText,
	Approved = @Approved,
	PostDate = @PostDate,
	ExpirationDate = @ExpirationDate,
	CategoryId = @CategoryId,
	UserId = @UserId
WHERE PostId = @PostId
END
GO
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsApprove')
	DROP PROCEDURE PostsApprove
GO

CREATE PROCEDURE PostsApprove (
	@PostId INT
	
	)
	as
BEGIN
	UPDATE Posts SET
	Approved = 1
WHERE PostId = @PostId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'PostsDelete')
	DROP PROCEDURE PostsDelete
GO

CREATE PROCEDURE PostsDelete(
	@PostId int
) AS
BEGIN
	DELETE FROM PostsTags
	WHERE PostId = @PostId;
	DELETE FROM Posts 
	WHERE PostId = @PostId;	
END
GO


----------------------------------------------------------------
-----------------CATEGORIES TABLE SPROCS----------------------------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoriesSelectAll')
	DROP PROCEDURE CategoriesSelectAll
GO

CREATE PROCEDURE CategoriesSelectAll AS
BEGIN 
	SELECT *
	FROM Categories 
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoriesSelectById')
	DROP PROCEDURE CategoriesSelectById
GO

CREATE PROCEDURE CategoriesSelectById(
	@CategoryId int
) AS
BEGIN
	SELECT *
	FROM Categories
	WHERE CategoryId = @CategoryId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoriesInsert')
	DROP PROCEDURE CategoriesInsert
GO

CREATE PROCEDURE CategoriesInsert (
	@CategoryName VARCHAR(30),
	@CategoryId INT OUT
	)
	as
BEGIN	
	IF not exists (SELECT * FROM Categories WHERE CategoryName = @CategoryName)
	BEGIN
	INSERT INTO Categories (CategoryName)
	VALUES (@CategoryName)
	END

	SET @CategoryId = @@IDENTITY;
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoriesUpdate')
	DROP PROCEDURE CategoriesUpdate
GO

CREATE PROCEDURE CategoriesUpdate (
	@CategoryId INT,
	@CategoryName VARCHAR(30)
	)
	as
BEGIN
	UPDATE Categories SET
	CategoryName = @CategoryName
WHERE CategoryId = @CategoryId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'CategoriesDelete')
	DROP PROCEDURE CategoriesDelete
GO

CREATE PROCEDURE CategoriesDelete(
	@CategoryId int
) AS
BEGIN
update dbo.posts set CategoryId = 1 
where CategoryId = @CategoryId;

	DELETE FROM Categories WHERE 
	CategoryId = @CategoryId;
END
GO

--------------------------------------------------------------
-----------------TAGS TABLE SPROCS----------------------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsSelectAll')
	DROP PROCEDURE TagsSelectAll
GO

CREATE PROCEDURE TagsSelectAll AS
BEGIN 
	SELECT *
	FROM Tags 
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsByPostId')
	DROP PROCEDURE TagsByPostId
GO

CREATE PROCEDURE TagsByPostId (
	@PostId INT
	)
	as
BEGIN	
	SELECT PostsTags.TagId, TagText 
	FROM PostsTags
	INNER JOIN Tags ON PostsTags.TagId = Tags.TagId
	WHERE PostId = @PostId
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsSelectById')
	DROP PROCEDURE TagsSelectById
GO

CREATE PROCEDURE TagsSelectById(
	@TagId int
) AS
BEGIN
	SELECT *
	FROM Tags
	WHERE TagId = @TagId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsInsert')
	DROP PROCEDURE TagsInsert
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE TagsInsert (
	@TagText VARCHAR(30),
	@TagId INT OUT
	)
	as
BEGIN	

	IF NOT EXISTS (SELECT * FROM Tags WHERE TagText = @TagText)
	BEGIN
	INSERT INTO Tags (TagText)
	VALUES (@TagText)
	SET @TagId = @@IDENTITY
	RETURN @TagId
	END

	ELSE
	SET @TagId = (SELECT TOP 1 TagId FROM Tags WHERE TagText = @TagText)
	RETURN @TagId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsUpdate')
	DROP PROCEDURE TagsUpdate
GO

CREATE PROCEDURE TagsUpdate (
	@TagId INT,
	@TagText VARCHAR(30)
	)
	as
BEGIN	
	UPDATE Tags SET
	TagText = @TagText
WHERE TagId = @TagId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'TagsDelete')
	DROP PROCEDURE TagsDelete
GO

CREATE PROCEDURE TagsDelete(
	@TagId int
) AS
BEGIN
	DELETE FROM Tags 
	WHERE TagId = @TagId;

	DELETE FROM PostsTags
	WHERE TagId = @TagId
END
GO
-------------Menu Item CRUD-------------

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MenuItemGetALL')
	DROP PROCEDURE MenuItemGetALL
GO
CREATE PROCEDURE MenuItemGetALL AS
BEGIN 
	SELECT MenuItemID,MenuText,MenuLink	
	FROM dbo.MenuItems 

END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MenuItemInsert')
	DROP PROCEDURE MenuItemInsert
GO

CREATE PROCEDURE MenuItemInsert (
	@MenuText NVARCHAR(50),
	@MenuLink NVARCHAR(Max)

	)
	as
BEGIN	
	INSERT INTO dbo.MenuItems(MenuLink, MenuText)
	VALUES (@MenuLink, @MenuText)

END
GO
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MenuItemDelete')
	DROP PROCEDURE MenuItemDelete
GO

CREATE PROCEDURE MenuItemDelete (
	@MenuItemID INT

	)
	as
BEGIN	
	Delete from dbo.MenuItems WHERE MenuItemID = @MenuItemID;

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'MenuItemUpdate')
	DROP PROCEDURE MenuItemUpdate
GO

CREATE PROCEDURE MenuItemUpdate (
	@MenuItemID INT,
	@MenuItemText VARCHAR(50),
	@MenuItemLink VARCHAR(MAX)

	)
	as
BEGIN	
	UPDATE dbo.MenuItems SET
	MenuText = @MenuItemText,
	MenuLink = @MenuItemLink
WHERE MenuItemID = @MenuItemID;

END
GO