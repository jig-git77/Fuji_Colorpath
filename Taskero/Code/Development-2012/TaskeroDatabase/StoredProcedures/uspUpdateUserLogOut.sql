IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspUpdateUserLogOut')
	BEGIN
		DROP  Procedure [CPV].[uspUpdateUserLogOut]
	END

GO

/*    
AUTHOR : Jitendra Makwana  
LAST UPDATED DATE : July 22 , 2008  
PURPOSE :- Update user logout time on logging out of user
*/    
CREATE PROCEDURE [CPV].[uspUpdateUserLogOut]
(   
		@TaskeroUserID INT ,
		@IPAddress VARCHAR(15)
) 
AS     
BEGIN    
		BEGIN TRY  
				/*---------- CHECK THERE IS ANY ENTRY EXIST FOR THE USER, IF SO THEN UPDATE USER LOGOUT TIME ----------- */
				IF EXISTS
				(
					SELECT 
						  MAX(ID) 
					FROM 
						  UserLog 
					WHERE 
						  TaskeroUserID = @TaskeroUserID
					AND
						  IPAddress = @IPAddress
				)
				BEGIN
						/*---------- DECLARE VARIABLE TO STORE LAST ID OF THE USER LOG DATA----------- */
						DECLARE @USERLOG_ID INT

						/*---------- GET LAST RECORD OF THE USER LOGIN ----------- */
						SELECT @USERLOG_ID = MAX(ID) FROM UserLog WHERE TaskeroUserID = @TaskeroUserID AND IPAddress = @IPAddress

						/*---------- UPDATE LOGOUT TIME OF THE USER ----------- */
						UPDATE
							  UserLog
						SET
							  LogoutTime = GETDATE()
						WHERE 
							  ID = @USERLOG_ID
				END
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

