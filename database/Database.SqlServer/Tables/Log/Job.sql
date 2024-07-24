CREATE TABLE [log].[Job]
(
    [InstanceId]            VARCHAR(80)     NOT NULL,
	[Id]                    TINYINT         NOT NULL,
    [TriggerGroup]          VARCHAR(100)    NOT NULL, 
    [TriggerName]           VARCHAR(100)    NOT NULL,
    [JobName]               VARCHAR(100)    NOT NULL,
    [JobFullPath]           VARCHAR(254)    NOT NULL,
    [JobCronExpression]     VARCHAR(50)     NOT NULL,
    [JobRegistered]         DATETIME        NOT NULL,
    [StatusId]              VARCHAR(80)     NOT NULL,
    [JobErrorType]          VARCHAR(254)    NULL,
    [JobErrorMessage]       VARCHAR(MAX)    NULL,
    [JobErrorStacktrace]    VARCHAR(MAX)    NULL,
)
