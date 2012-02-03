IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetRoles')
	BEGIN
		DROP  Procedure [CPV].[uspGetRoles]
	END

GO

/*        
AUTHOR : Shivpal Jhala        
Created Date : July 11, 2008        
Purpose : To Retrieve the Roles.        
*/        
CREATE PROCEDURE [CPV].[uspGetRoles]
(  
	@TaskeroUserID INT
)  
AS          
BEGIN        
 BEGIN TRY        
  /*------------- Get Roles List----------------*/      
   DECLARE @RoleID int 
	DECLARE @RLEVEL INT
	SELECT @RoleID = RoleID from TaskeroUser where TaskeroUserID = @TaskeroUserID
	IF  @RoleID <> 0  
	BEGIN
		SELECT @RLEVEL = RoleLevel from Roles where RoleID = @RoleID
		SELECT RoleID,RoleDesc,RoleLevel FROM Roles WHERE RoleLevel > @RLEVEL ORDER BY RoleLevel	
	END
	BEGIN       
		SELECT @RLEVEL = RoleLevel from Roles where RoleID = (SELECT DISTINCT MIN(RoleID) FROM RolePermission WHERE PermissionID in ( SELECT PermissionID FROM UserPermission WHERE TaskeroUserID = 83 ) ) 
		SELECT RoleID,RoleDesc,RoleLevel FROM Roles WHERE RoleLevel > @RLEVEL ORDER BY RoleLevel			
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

