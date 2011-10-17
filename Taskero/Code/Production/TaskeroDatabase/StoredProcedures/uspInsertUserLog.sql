IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertUserLog')
	BEGIN
		DROP  Procedure [CPV].[uspInsertUserLog]
	END

GO

/*    
AUTHOR : Jitendra Makwana  
LAST UPDATED DATE : July 22 , 2008  
PURPOSE :- Creates a login information for user
*/    
CREATE PROCEDURE [CPV].[uspInsertUserLog]
(   
		@ID INT = NULL ,   
		@TaskeroUserID INT ,   
		@IPAddress VARCHAR(15),   
		@LoginTime DATETIME  
) 
AS     
BEGIN    
		BEGIN TRY  
				INSERT INTO UserLog
				(
				  [TaskeroUserID]
				, [IPAddress]   
				, [LoginTime]   
				)   
				VALUES   
				(
				  @TaskeroUserID
				, @IPAddress
				, GETDATE()
				)      
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

