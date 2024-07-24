CREATE INDEX [IDX_Session_UserId_RefreshToken]
	ON [aud].[Session] (UserId, RefreshToken)
