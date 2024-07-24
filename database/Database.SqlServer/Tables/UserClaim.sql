CREATE TABLE [dbo].[UserClaim] (
    [Id]                VARCHAR(254)    NOT NULL,
    [UserId]            VARCHAR(254)    NOT NULL,
	[Type]				VARCHAR(80)     NOT NULL,
	[Value]				VARCHAR(60)     NOT NULL,
    [StatusId]          TINYINT         NOT NULL,
    [CreatedBy]         VARCHAR(60)     NOT NULL,
    [Created]           DATETIME        NOT NULL,
    [LastModifiedBy]    VARCHAR(60)     NOT NULL,
    [LastModified]      DATETIME        NOT NULL
)