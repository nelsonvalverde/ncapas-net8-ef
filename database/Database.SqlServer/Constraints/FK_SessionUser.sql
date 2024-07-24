ALTER TABLE [aud].[Session]
	ADD CONSTRAINT [FK_Session_User]
	FOREIGN KEY (UserId) 
	REFERENCES [dbo].[User]([Id])
GO

