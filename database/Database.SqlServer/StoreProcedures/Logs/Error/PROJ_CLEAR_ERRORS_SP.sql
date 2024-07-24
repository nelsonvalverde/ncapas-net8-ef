/***********************************************************************************
-> Autor			: N.VALVERDE
-> Fecha creación	: 23/06/2024  
-> Descripción		: CLEAR ERRORS
***********************************************************************************/  
CREATE PROCEDURE [log].[PROJ_CLEAR_ERRORS_SP]
AS
	SET NOCOUNT ON

	TRUNCATE TABLE log.Error
	