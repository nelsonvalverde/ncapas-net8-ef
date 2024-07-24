CREATE TABLE [log].[Error]
(
	[Id]            INT             NOT NULL IDENTITY, 
    [Code]          VARCHAR(50)     NOT NULL,
    [App]           VARCHAR(50)     NULL, 
    [Type]          VARCHAR(254)     NOT NULL, 
    [Message]       VARCHAR(MAX)    NOT NULL,
    [Body]          VARCHAR(MAX)    NOT NULL,
    [Method]        VARCHAR(254)    NOT NULL,
    [Path]          VARCHAR(MAX)    NOT NULL,
    [QueryString]   VARCHAR(MAX)    NOT NULL,
    [UserAgent]     VARCHAR(MAX)    NOT NULL,
    [StackTrace]    VARCHAR(MAX)    NOT NULL,
    [Created]       DATETIME        NOT NULL,
    [CreatedBy]     VARCHAR(254)    NULL
)
