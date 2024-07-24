CREATE TABLE [aud].[Session]
(
	[Id]				VARCHAR(254)	NOT NULL,
	[UserId]			VARCHAR(254)	NOT NULL,
	[Token]				VARCHAR(MAX)	NOT NULL,
	[RefreshToken]		VARCHAR(254)	NOT NULL,
	[Expires]			DATETIME		NOT NULL,
    [StatusId]          TINYINT			NOT NULL,
    [CreatedBy]         VARCHAR(60)		NULL,
    [Created]           DATETIME		NOT NULL,
    [LastModifiedBy]    VARCHAR(60)		NULL,
    [LastModified]      DATETIME		NOT NULL
)
