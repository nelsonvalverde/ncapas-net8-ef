/*
    Responsable : Nelson Valverde La Torre
    Date        : 23/06/2024
    Description : Update Session by Refresh token code
*/
CREATE PROCEDURE [aud].[PROJ_UPDATE_SESSION_BY_REFRESH_TOKEN_SP]
    @REFRESH_TOKEN		VARCHAR(254),
    @STATUS_ID          TINYINT,
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 
    AS
    SET NOCOUNT ON;

    UPDATE  [aud].[Session]
    SET     [RefreshToken]      = @REFRESH_TOKEN,
            [StatusId]          = @STATUS_ID,
            [LastModified]      = @LAST_MODIFIED,
            [LastModifiedBy]    = @LAST_MODIFIED_BY
    WHERE   RefreshToken        = @REFRESH_TOKEN
GO