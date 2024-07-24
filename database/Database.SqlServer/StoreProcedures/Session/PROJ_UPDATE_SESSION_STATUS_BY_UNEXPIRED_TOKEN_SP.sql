/*
    Responsable : Nelson Valverde La Torre
    Date        : 23/06/2024
    Description : Update Status by unexpired token
*/
CREATE PROCEDURE [aud].[PROJ_UPDATE_SESSION_STATUS_BY_UNEXPIRED_TOKEN_SP]
    @STATUS_ID          TINYINT,
    @LAST_MODIFIED      DATETIME,
    @LAST_MODIFIED_BY   VARCHAR(60) 

    AS
    SET NOCOUNT ON;

    UPDATE  [aud].[Session]
    SET     [StatusId]          = @STATUS_ID,
            [LastModified]      = @LAST_MODIFIED,
            [LastModifiedBy]    = @LAST_MODIFIED_BY
    WHERE   UserId              = @LAST_MODIFIED_BY
    AND     Expires             < @LAST_MODIFIED
GO