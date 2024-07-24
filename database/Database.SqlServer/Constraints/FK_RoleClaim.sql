ALTER TABLE [dbo].[RoleClaim]
	ADD CONSTRAINT [FK_RoleClaim_Role]
	FOREIGN KEY (RoleId) 
	REFERENCES [dbo].[Role]([Id])
GO

