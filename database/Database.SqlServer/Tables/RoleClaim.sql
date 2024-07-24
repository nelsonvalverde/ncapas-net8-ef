CREATE TABLE [dbo].[RoleClaim]
(
    [Id]                VARCHAR(254)    NOT NULL,
    [RoleId]            VARCHAR(254)    NOT NULL,
	[Type]				VARCHAR(80)     NOT NULL,
	[Value]				VARCHAR(60)     NOT NULL,
    [StatusId]          TINYINT         NOT NULL,
    [CreatedBy]         VARCHAR(254)    NOT NULL,
    [Created]           DATETIME        NOT NULL,
    [LastModifiedBy]    VARCHAR(254)    NOT NULL,
    [LastModified]      DATETIME        NOT NULL
)