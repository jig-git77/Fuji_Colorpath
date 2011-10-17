IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspInsertRolePermission')
	BEGIN
		DROP  Procedure [CPV].[uspInsertRolePermission]
	END

GO

/*  
AUTHOR : Shivpal Jhala.
Created Date : July 18 2008  
Purpose : updating the RolePermission Table as per the Role Management
*/  
CREATE PROCEDURE [CPV].[uspInsertRolePermission]
( 
	@RoleManagement VARCHAR(8000),
	@Result INT OUTPUT
) 
AS    
BEGIN  
	BEGIN TRY  
		SET @Result = 1
		/*---------- Truncating the RolePermission Table ----------*/  
		
		TRUNCATE TABLE 
			RolePermission

		/*---------- Insert Process for RolePermission Table ----------*/  
		DECLARE @x XML
		SELECT @x = @RoleManagement
		INSERT INTO 
		RolePermission
		SELECT    
		x.value('@RoleID[1]','VARCHAR(10)') AS RoleID,
		x.value('@PermissionID[1]','VARCHAR(10)') AS PermissionID
		FROM 
		@x.nodes('/RolePermissions/Permission') e(x)   
		SET @Result = 2

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
  
  

