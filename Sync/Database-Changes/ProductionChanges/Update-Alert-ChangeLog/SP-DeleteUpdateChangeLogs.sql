	CREATE PROCEDURE [dbo].[DeleteUpdateChangeLogs]
	@UpdateListID INT
	AS
	BEGIN
		
		DELETE FROM UpdateChangeLogs WHERE UpdateListID=@UpdateListID
		
	END
