IF NOT EXISTS(SELECT 1 FROM [dbo].[Role] WHERE Id = '0d53c6f7-1b1e-4180-b113-153f1190b604')
BEGIN
	INSERT INTO [dbo].[Role](Id, [Name], [Description] ,[StatusId], Created, CreatedBy, LastModified, LastModifiedBy)
	VALUES('0d53c6f7-1b1e-4180-b113-153f1190b604', 'Usuario', 'Permisos mínimos' ,1, GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM');
END

IF NOT EXISTS(SELECT 1 FROM dbo.[Role] WHERE Id = '11dcc63c-7903-4777-bcfe-1cb29aaf9631')
BEGIN
	INSERT INTO [dbo].[Role](Id, [Name], [Description], [StatusId], Created, CreatedBy, LastModified, LastModifiedBy)
	VALUES('11dcc63c-7903-4777-bcfe-1cb29aaf9631', 'Administrador', 'Permisos Administrativos' ,1, GETDATE(), 'SYSTEM', GETDATE(), 'SYSTEM');
END

IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE Id = '12edc7bb-efa6-4155-9352-cf5977e9dbc1')
BEGIN
	INSERT INTO [dbo].[User](Id, [Name], [LastName], [PhoneNumber], [Email], [PasswordHash], [Birthday], [EmailConfirmed], [StatusId], [CreatedBy], [Created], [LastModifiedBy], [LastModified])
	VALUES('12edc7bb-efa6-4155-9352-cf5977e9dbc1', 'Nelson H.', 'Valverde La Torre' ,'912345678', 'nelson.h@email.com', '+Eqq30u+QWR8S7tzIr4KAKrr+xA8Rf8dVC9q50lC2KZpltQZPO7i6XJzZ2/RWxJj', GETDATE(), 0, 1, 'SYSTEM', GETDATE(), 'SYSTEM',GETDATE());
END

IF NOT EXISTS(SELECT 1 FROM dbo.[UserRole] WHERE UserId = '12edc7bb-efa6-4155-9352-cf5977e9dbc1')
BEGIN
	INSERT INTO [dbo].[UserRole](UserId, RoleId, [CreatedBy], [Created], [LastModifiedBy], [LastModified])
	VALUES('12edc7bb-efa6-4155-9352-cf5977e9dbc1', '11dcc63c-7903-4777-bcfe-1cb29aaf9631', 'SYSTEM', GETDATE(), 'SYSTEM',GETDATE());
END

IF NOT EXISTS(SELECT 1 FROM dbo.[UserClaim] WHERE UserId = '12edc7bb-efa6-4155-9352-cf5977e9dbc1')
BEGIN
	INSERT INTO [dbo].[UserClaim](Id, [UserId], [Type], [Value], [StatusId], [CreatedBy], [Created], [LastModifiedBy], [LastModified])
	VALUES	('9f0f3bf8-a1c3-4e54-ba59-b53d49da8f75', '12edc7bb-efa6-4155-9352-cf5977e9dbc1', 'ShowMenuWelcome', 'false', 1, 'SYSTEM', GETDATE(), 'SYSTEM',GETDATE()),
			('262a9144-dcf7-4e5c-9646-f5d886a3fb2b', '12edc7bb-efa6-4155-9352-cf5977e9dbc1', 'ShowMenuUserConfig', 'false', 1, 'SYSTEM', GETDATE(), 'SYSTEM',GETDATE())
END

IF NOT EXISTS(SELECT 1 FROM dbo.[RoleClaim] WHERE RoleId = '11dcc63c-7903-4777-bcfe-1cb29aaf9631')
BEGIN
	INSERT INTO [dbo].[RoleClaim](Id, [RoleId], [Type], [Value], [StatusId] ,[CreatedBy], [Created], [LastModifiedBy], [LastModified])
	VALUES('e00088c0-592d-47cc-8863-25c111689359', '11dcc63c-7903-4777-bcfe-1cb29aaf9631', 'ShowAllMenu', 'true', 1, 'SYSTEM', GETDATE(), 'SYSTEM',GETDATE());
END