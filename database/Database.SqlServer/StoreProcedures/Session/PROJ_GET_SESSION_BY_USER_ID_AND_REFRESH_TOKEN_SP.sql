/*
    Responsable : Nelson Valverde La Torre
    Date        : 23/06/2024
    Description : Get session by UserId and RefreshToken
*/
CREATE PROCEDURE [aud].[PROJ_GET_SESSION_BY_USER_ID_AND_REFRESH_TOKEN_SP]
    @USER_ID        VARCHAR(254),
    @REFRESH_TOKEN  VARCHAR(254)
    AS
    SET NOCOUNT ON;

    SELECT  
            [Id]                AS  SESSION_ID,
            [UserId]            AS  SESSION_USER_ID,
            [Token]             AS  SESSION_TOKEN,
            [RefreshToken]      AS  SESSION_REFRESH_TOKEN,
            [Expires]           AS  SESSION_EXPIRES,
            [StatusId]          AS  SESSION_STATUS_ID,
            [Created]           AS  SESSION_CREATED,
            [CreatedBy]         AS  SESSION_CREATED_BY,
            [LastModified]      AS  SESSION_LAST_MODIFIED,
            [LastModifiedBy]    AS  SESSION_LAST_MODIFIED_BY
    FROM    [aud].[Session]     WITH(NOLOCK, INDEX=IDX_Session_UserId_RefreshToken)
    WHERE   [UserId]            = @USER_ID
    AND     [RefreshToken]      = @REFRESH_TOKEN
     
GO