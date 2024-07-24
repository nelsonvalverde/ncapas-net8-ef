/***********************************************************************************
-> Autor			: N.VALVERDE
-> Fecha creación	: 23/06/2024  
-> Descripción		: Insert Job logs
***********************************************************************************/  
CREATE PROCEDURE [log].[PROJ_CREATE_JOB_LOG]
	@INSTANCE_ID			VARCHAR(80),
	@TRIGGER_GROUP			VARCHAR(100),
	@TRIGGER_NAME			VARCHAR(100),
	@JOB_NAME				VARCHAR(100),
	@JOB_FULL_PATH			VARCHAR(254),
	@JOB_CRON_EXPRESSION	VARCHAR(50),
	@JOB_REGISTERED			DATETIME,
	@STATUS_ID				VARCHAR(80),
	@JOB_ERROR_TYPE			VARCHAR(254) = NULL,
	@JOB_ERROR_MESSAGE		VARCHAR(MAX) = NULL,
	@JOB_ERROR_STACKTRACE	VARCHAR(MAX) = NULL
	
AS
	SET NOCOUNT ON

	DECLARE @ID TINYINT = (SELECT COUNT(1) + 1 FROM log.Job (NOLOCK) WHERE InstanceId = @INSTANCE_ID)

	INSERT INTO [log].[Job]
	(
		InstanceId,
		Id,
		TriggerGroup,
		TriggerName,
		JobName,
		JobFullPath,
		JobCronExpression,
		JobRegistered,
		StatusId,
		JobErrorType,
		JobErrorMessage,
		JobErrorStacktrace
	)
	VALUES
	(
		@INSTANCE_ID,
		@ID,
		@TRIGGER_GROUP,
		@TRIGGER_NAME,
		@JOB_NAME,
		@JOB_FULL_PATH,
		@JOB_CRON_EXPRESSION,
		@JOB_REGISTERED,
		@STATUS_ID,
		@JOB_ERROR_TYPE,
		@JOB_ERROR_MESSAGE,
		@JOB_ERROR_STACKTRACE
	)

