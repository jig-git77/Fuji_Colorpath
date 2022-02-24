	CREATE PROCEDURE [dbo].[UserUpdateChangeLogsStatus]
			@UserID INT,
			@IsUpdateAlert BIT,
			@IsAlertPopup BIT
		AS
		BEGIN
			
			UPDATE CP.[User]
			SET 
			IsUpdateAlert=@IsUpdateAlert,
			IsAlertPopup=@IsAlertPopup
			WHERE UserID=@UserID
		
		END