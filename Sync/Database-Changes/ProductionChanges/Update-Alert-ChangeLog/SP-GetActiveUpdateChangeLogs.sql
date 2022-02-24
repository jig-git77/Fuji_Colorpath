CREATE PROCEDURE [dbo].[GetActiveUpdateChangeLogs]
		@ShowActiveUpdate BIT,
		@ShowActiveAlert BIT
	AS
	BEGIN
		
		IF(@ShowActiveUpdate = 1)
		BEGIN
			SELECT * FROM UpdateChangeLogs WHERE IsActive=1 AND IsAlert=0 AND CONVERT(DATE,ExpirationDate) >= CONVERT(DATE,GETDATE()) ORDER BY EntryDate DESC
		END
		IF(@ShowActiveAlert = 1)
		BEGIN
			SELECT * FROM UpdateChangeLogs WHERE IsActive=1 AND IsAlert=1 AND CONVERT(DATE,ExpirationDate) >= CONVERT(DATE,GETDATE()) ORDER BY EntryDate DESC
		END
	
	END

