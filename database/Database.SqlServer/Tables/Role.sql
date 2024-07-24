CREATE TABLE [dbo].[Role] (
    [Id]                VARCHAR(254)    NOT NULL,
    [Name]              VARCHAR(60)     NOT NULL,
    [Description]       VARCHAR(254)    NOT NULL,
    [StatusId]          TINYINT         NOT NULL,
    [CreatedBy]         VARCHAR(254)    NOT NULL,
    [Created]           DATETIME        NOT NULL,
    [LastModifiedBy]    VARCHAR(254)    NOT NULL,
    [LastModified]      DATETIME        NOT NULL
)