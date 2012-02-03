IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspDeleteDevice')
	BEGIN
		DROP  Procedure  [CPV].uspDeleteDevice
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 17 2008
Purpose : Delete an Existing Device.
*/ 
CREATE PROCEDURE [CPV].[uspDeleteDevice] 
( 
	@CPDeviceID INT
) 
AS   
BEGIN  
	BEGIN TRY  

		/*---------- Delete an Existing Device ----------*/
  		DELETE FROM
			CPDevice		
		WHERE
			[CPDeviceID] = @CPDeviceID

 	END TRY  
	BEGIN CATCH  
			DECLARE @ERROR_NUMBER INT   
			DECLARE @ERROR_SEVERITY INT  
			DECLARE @ERROR_STATE INT   
			DECLARE @ERROR_PROCEDURE NVARCHAR(252)   
			DECLARE @ERROR_LINE INT   
			DECLARE @ERROR_MESSAGE NVARCHAR(4000)   
			SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()   
			EXEC CPV.uspInsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE   
	END CATCH  
END

