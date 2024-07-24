CREATE TABLE [dbo].[UserRole] (
    [UserId]			VARCHAR(254)    NOT NULL,
    [RoleId]            VARCHAR(254)    NOT NULL,
    [CreatedBy]         VARCHAR(60)     NOT NULL,
    [Created]           DATETIME        NOT NULL,
    [LastModifiedBy]    VARCHAR(60)     NOT NULL,
    [LastModified]      DATETIME        NOT NULL
)