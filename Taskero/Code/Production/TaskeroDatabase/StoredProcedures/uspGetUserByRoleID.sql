IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetUserByRoleID')
	BEGIN
		DROP  Procedure [CPV].[uspGetUserByRoleID]
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 28 2008  
Purpose : To Get all the user list by role id.
*/
CREATE PROCEDURE [CPV].[uspGetUserByRoleID] 
(
	@RoleID INT
)
AS    
BEGIN  
	BEGIN TRY
			/*------------- Get User List ON User Role ----------------*/  	
			SELECT 
					TaskeroUser.TaskeroUserID , TaskeroUser.LoginID 
			FROM 
					TaskeroUser 			
			LEFT OUTER JOIN 
					Roles ON Roles.RoleID = TaskeroUser.RoleID
			WHERE 
					TaskeroUser.RoleID = @RoleID
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

