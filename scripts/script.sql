CREATE TABLE [dbo].[User] (
    [Id]                INT IDENTITY(1,1),
    [Name]              VARCHAR(60),
    [LastName]          VARCHAR(90),
    [FullName]          AS ([Name] + ' ' + [LastName]),
	[Password]			VARCHAR(254),
    [Birthday]          DATE,
    [StatusId]          TINYINT,
    [CreatedBy]         VARCHAR(60),
    [Created]           DATETIME,
    [LastModifiedBy]    VARCHAR(60),
    [LastModified]      DATETIME
)
GO

CREATE PROCEDURE [dbo].[PROJ_CREATE_USER_SP]
    @NAME               VARCHAR(60),
    @LAST_NAME          VARCHAR(60),
    @BIRTHDAY           DATE,
	@PASSWORD			VARCHAR(254),
    @STATUS_ID          TINYINT,
    @CREATED            DATETIME,
    @CREATED_BY         VARCHAR(60),
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 

    AS
    SET NOCOUNT ON;

    INSERT INTO [dbo].[User](
        [Name],
        [LastName],
        [Birthday],
        [StatusId],
		[Password],
        [Created],
        [CreatedBy],
        [LastModified],
        [LastModifiedBy]
    ) 
    VALUES(
        @NAME,
        @LAST_NAME,
        @BIRTHDAY,
        @STATUS_ID,
		@PASSWORD,
        @CREATED,
        @CREATED_BY,
        @LAST_MODIFIED,
        @LAST_MODIFIED_BY
    )
GO

CREATE PROCEDURE [dbo].[PROJ_UPDATE_USER_STATUS_SP]
    @ID                 INT,
    @STATUS_ID          TINYINT,
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 

    AS
    SET NOCOUNT ON;

    UPDATE  [dbo].[User]
    SET     [StatusId] = @STATUS_ID,
            [LastModified] = @LAST_MODIFIED,
            [LastModifiedBy] = @LAST_MODIFIED_BY
    WHERE   [Id] = @ID
     
GO  


CREATE PROCEDURE [dbo].[PROJ_FILTER_USER_SP]
    @FILTER VARCHAR(254) = NULL
    AS
    SET NOCOUNT ON;

    SELECT  
            [Id]                AS  USER_ID,
            [Name]              AS  USER_NAME,
            [LastName]          AS  USER_LAST_NAME,
            [Birthday]          AS  USER_BIRTHDAY,
            [Created]           AS  USER_CREATED,
            [CreatedBy]         AS  USER_CREATED_BY,
            [LastModified]      AS  USER_LAST_MODIFIED,
            [LastModifiedBy]    AS  USER_LAST_MODIFIED_BY
    FROM    [dbo].[User] (NOLOCK)
    WHERE   [FullName] LIKE '%'+@FILTER+'%'
     
GO  

CREATE PROCEDURE [dbo].[PROJ_PAGE_USER_SP]
    @FILTER VARCHAR(254) = NULL,
    @PAGE_NUMBER    INT,
    @PAGE_SIZE      INT,
    @TOTAL_RECORD   INT OUTPUT
    AS
    SET NOCOUNT ON;

    WITH USER_PAGE_CTE AS (
        SELECT  [Id],
                [Name],
                [LastName],
                [Birthday],
                [Created],
                [CreatedBy],
                [LastModified],
                [LastModifiedBy]
        FROM    [dbo].[User] (NOLOCK)
        WHERE   [FullName] LIKE '%'+@FILTER+'%'
    )
    SELECT      [Id]                AS  USER_ID,
                [Name]              AS  USER_NAME,
                [LastName]          AS  USER_LAST_NAME,
                [Birthday]          AS  USER_BIRTHDAY,
                [Created]           AS  USER_CREATED,
                [CreatedBy]         AS  USER_CREATED_BY,
                [LastModified]      AS  USER_LAST_MODIFIED,
                [LastModifiedBy]    AS  USER_LAST_MODIFIED_BY
    FROM        USER_PAGE_CTE TU (NOLOCK)
    ORDER BY    TU.Created
    OFFSET      (@PAGE_NUMBER - 1) * @PAGE_SIZE ROWS
    FETCH NEXT  @PAGE_SIZE ROWS ONLY
    
    SET @TOTAL_RECORD = (SELECT COUNT(1) FROM USER_PAGE_CTE (NOLOCK));

GO  


CREATE PROCEDURE [dbo].[PROJ_UPDATE_USER_SP]
    @ID                 INT,
    @NAME               VARCHAR(60),
    @LAST_NAME          VARCHAR(60),
    @BIRTHDAY           DATE,
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 

    AS
    SET NOCOUNT ON;

    UPDATE  [dbo].[User]
    SET     [Name] = @NAME,
            [LastName] = @LAST_NAME,
            [Birthday] = @BIRTHDAY,
            [LastModified] = @LAST_MODIFIED,
            [LastModifiedBy] = @LAST_MODIFIED_BY
    WHERE   [Id] = @ID
     
GO  
