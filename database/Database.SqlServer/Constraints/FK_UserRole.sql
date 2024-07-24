ALTER TABLE [dbo].[UserRole]
	ADD CONSTRAINT [FK_UserRole_User]
	FOREIGN KEY (UserId) 
	REFERENCES [dbo].[User]([Id])
GO

ALTER TABLE [dbo].[UserRole]
	ADD CONSTRAINT [FK_UserRole_Role]
	FOREIGN KEY (RoleId) 
	REFERENCES [dbo].[Role]([Id])
GO
