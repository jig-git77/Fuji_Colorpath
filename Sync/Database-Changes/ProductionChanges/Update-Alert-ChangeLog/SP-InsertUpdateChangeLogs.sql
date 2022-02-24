CREATE PROCEDURE [dbo].[InsertUpdateChangeLogs]
		@UpdateListID INT,
		@HeaderLine NVARCHAR(100),
		@SubHeaderLine  NVARCHAR(100),
		@Description  TEXT,
		@EntryDate DATETIME,
		@ExpirationDate DATETIME,
		@IsActive BIT,
		@IsAlert BIT
	AS
	BEGIN
		
		
	IF(@UpdateListID = 0)
	BEGIN
		INSERT INTO UpdateChangeLogs(HeaderLine,SubHeaderLine,Description,EntryDate,ExpirationDate,IsActive,IsAlert)
		VALUES(@HeaderLine,@SubHeaderLine,@Description,@EntryDate,@ExpirationDate,@IsActive,@IsAlert)
		IF(@IsAlert = 1) --Update user table when new entry happen.
		BEGIN
			UPDATE CP.[User] SET IsAlertPopup=1
		END
		ELSE
		BEGIN
			UPDATE CP.[User] SET IsUpdateAlert=1
		END
	END

	IF(@UpdateListID > 0)
	BEGIN
			
				UPDATE UpdateChangeLogs
				SET 
					HeaderLine=@HeaderLine,
					SubHeaderLine=@SubHeaderLine,
					Description=@Description,
					EntryDate=@EntryDate,
					ExpirationDate=@ExpirationDate,
					IsActive=@IsActive,
					IsAlert=@IsAlert
				WHERE UpdateListID=@UpdateListID
			
	END
		
END