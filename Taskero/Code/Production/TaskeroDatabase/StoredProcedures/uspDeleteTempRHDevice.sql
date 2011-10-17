IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspDeleteTempRHDevice')
	BEGIN
		DROP  Procedure [CPV].[uspDeleteTempRHDevice]     
	END

GO

/*    
AUTHOR : Shivpal Jhala    
LAST UPDATED DATE : July 22 , 2008    
PURPOSE :- Updating Data for TempRHDevice 
*/    
CREATE PROCEDURE [CPV].[uspDeleteTempRHDevice]     
(
	@TempRHDeviceID	smallint,     	
	@Result int=0 output     
) 
AS     
BEGIN    
	BEGIN TRY    
		SET @Result = 1    
		--************************  Inserting into TempRHDevice table  ************************--    
		DELETE FROM  
			TempRHDevice
		WHERE
			TempRHDeviceID = @TempRHDeviceID
		--*** setting the @Result =2 for the successfully Update of data***--    
		SET @Result = 2    
		
	END TRY    
	BEGIN CATCH    
		--*** Error Handling***--    
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

