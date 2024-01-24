USE [$DatabaseName$]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER TABLE Book
ADD BookTitle AS (Json_Value(BookInfo, '$.BookTitle')) PERSISTED;
Go
ALTER TABLE Book
ADD BookDescription AS (Json_Value(BookInfo, '$.BookDescription')) PERSISTED;
Go
ALTER TABLE Book
ADD Author AS (Json_Value(BookInfo, '$.Author')) PERSISTED;
Go
ALTER TABLE Book
ADD PublishDate AS (Json_Value(BookInfo, '$.PublishDate')) PERSISTED;
Go

CREATE PROCEDURE [dbo].[BooksSearch]
	 @SearchKey nvarchar(500),	 
	 @PageNumber int = 1,
	 @PageSize int = 10
AS
BEGIN	
	SET NOCOUNT ON;

	IF @PageSize is NULL or @PageSize <= 0 SET @PageSize = 10;
    IF @PageNumber is NULL or @PageNumber <= 0 SET @PageNumber = 1;

WITH PaginatedResults AS (
    SELECT 
        [BookId] Id,
        BookData.Title AS [Title],
        BookData.Description AS [Description],
        BookData.Author AS [Author],
        BookData.PublishDate AS [PublishDate],
        BookData.Cover AS [CoverBase64],
        [LastModified],
        ROW_NUMBER() OVER (ORDER BY [LastModified] DESC) AS RowNum
    FROM 
        [ElmTestDb].[dbo].[Book]

	CROSS APPLY OpenJson(BookInfo) 
		WITH  (
		  Title nvarchar(250) '$.BookTitle',
		  Description nvarchar(1000) '$.BookDescription',
		  Author nvarchar(250) '$.Author',
		  PublishDate nvarchar(250) '$.PublishDate',
		  Cover nvarchar(max) '$.CoverBase64'
		)
		AS BookData

		Where 
		    (
                @SearchKey IS NULL OR @SearchKey = ''
            )
            OR
            (
                [Book].BookTitle LIKE '%' + @SearchKey + '%'
                OR
                [Book].BookDescription LIKE '%' + @SearchKey + '%'
                OR
                [Book].Author LIKE '%' + @SearchKey + '%'
                OR
                [Book].PublishDate LIKE '%' + @SearchKey + '%'
            )
)
SELECT 
    [Id],
    [Title],
    [Description],
    [Author],
    [PublishDate],
    [CoverBase64],
    [LastModified]    
FROM 
    PaginatedResults
WHERE 
    RowNum BETWEEN (@PageNumber - 1) * @PageSize + 1 AND @PageNumber * @PageSize

END
