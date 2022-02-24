CREATE PROCEDURE [dbo].[GetAllUpdateChangeLogs]
		@UpdateListID INT
		AS
		BEGIN
			
			IF(@UpdateListID = 0)
			BEGIN
				SELECT * FROM UpdateChangeLogs ORDER By UpdateListID DESC
			END
			IF(@UpdateListID > 0)
			BEGIN
				SELECT * FROM UpdateChangeLogs WHERE UpdateListID=@UpdateListID
			END
			
		END