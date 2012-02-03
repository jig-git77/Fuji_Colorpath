IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertErrorLog')
	BEGIN
		DROP  Procedure [CPV].[uspInsertErrorLog]
	END

GO

/*      
AUTHOR : Maulik Dhorajia     
Created Date : July 15, 2008      
Purpose : Inserts the Error Log.      
*/      
CREATE PROCEDURE [CPV].[uspInsertErrorLog]
( 
	@TimeStamp DATETIME , 
	@ExType VARCHAR(50) , 
	@ExMessage VARCHAR(500) , 
	@ExStackTracke VARCHAR(4000) , 
	@TaskeroUserID INT 
) 
AS 
BEGIN
	
		INSERT INTO ErrorLog
		(
			[TimeStamp] 
			, [ExType] 
			, [ExMessage] 
			, [ExStackTracke] 
			, [TaskeroUserID]
		) 
		VALUES 
		(
			GETDATE() 
			, @ExType 
			, @ExMessage 
			, @ExStackTracke 
			, @TaskeroUserID
		)   
	
END

