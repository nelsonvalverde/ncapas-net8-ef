ALTER TABLE [dbo].[UserClaim]
	ADD CONSTRAINT [FK_UserClaim_User]
	FOREIGN KEY (UserId) 
	REFERENCES [dbo].[User]([Id])
GO
