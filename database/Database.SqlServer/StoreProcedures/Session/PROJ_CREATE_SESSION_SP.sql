/*
    Responsable : Nelson Valverde La Torre
    Date        : 23/06/2024
    Description : Create Session
*/
CREATE PROCEDURE [aud].[PROJ_CREATE_SESSION_SP]
    @ID                 VARCHAR(254),
    @USER_ID            VARCHAR(254),
    @TOKEN              VARCHAR(MAX),
    @REFRESH_TOKEN		VARCHAR(254),
    @EXPIRES     		DATETIME,
    @STATUS_ID          TINYINT,
    @CREATED            DATETIME,
    @CREATED_BY         VARCHAR(60),
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 
    AS
    SET NOCOUNT ON;

    INSERT INTO [aud].[Session](
        [Id],
        [UserId],
        [Token],
        [RefreshToken],
        [Expires],
        [StatusId],
        [Created],
        [CreatedBy],
        [LastModified],
        [LastModifiedBy]
    ) 
    VALUES(
        @ID,
        @USER_ID,
        @TOKEN,
        @REFRESH_TOKEN,
        @EXPIRES,
        @STATUS_ID,
        @CREATED,
        @CREATED_BY,
        @LAST_MODIFIED,
        @LAST_MODIFIED_BY
    )
GO