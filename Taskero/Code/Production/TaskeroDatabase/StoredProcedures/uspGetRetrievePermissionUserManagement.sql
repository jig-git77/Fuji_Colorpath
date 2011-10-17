IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetRetrievePermissionUserManagement')
	BEGIN
		DROP  Procedure [CPV].[uspGetRetrievePermissionUserManagement]
	END

GO

/*            
AUTHOR : Shivpal Jhala            
Created Date : July 14, 2008            
Purpose : To Retrieve the Roles.            
*/            
CREATE PROCEDURE [CPV].[uspGetRetrievePermissionUserManagement]     
(    
	@RoleID TINYINT = 0
)    
AS      
BEGIN        
 BEGIN TRY   
 if @RoleID = 1  
 BEGIN  
  SELECT     
   Permission.PermissionID , Permission.PermissionDesc    
  FROM     
   Permission  
  ORDER BY       
   Permission.PermissionID
 END  
 ELSE  
 BEGIN         
   SELECT     
    Permission.PermissionID , Permission.PermissionDesc    
   FROM     
    RolePermission , Permission    
   WHERE     
    RolePermission.PermissionID = Permission.PermissionID    
    and RolePermission.RoleID = @RoleID    
   ORDER BY       
    Permission.PermissionID
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
