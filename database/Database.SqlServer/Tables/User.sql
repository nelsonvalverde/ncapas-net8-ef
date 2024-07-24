CREATE TABLE [dbo].[User] (
    [Id]                VARCHAR(254) NOT NULL,
    [Name]              VARCHAR(60) NOT NULL,
    [LastName]          VARCHAR(90) NOT NULL,
    [FullName]          AS ([Name] + ' ' + [LastName]),
    [PhoneNumber]       VARCHAR(32) NULL,
    [Email]             VARCHAR(120) NOT NULL,
	[PasswordHash]	    VARCHAR(254) NOT NULL,
    [Birthday]          DATE NOT NULL ,
    [EmailConfirmed]    BIT NOT NULL,
    [StatusId]          TINYINT     NOT NULL,
    [CreatedBy]         VARCHAR(60) NULL,
    [Created]           DATETIME    NOT NULL,
    [LastModifiedBy]    VARCHAR(60) NULL,
    [LastModified]      DATETIME    NOT NULL
)