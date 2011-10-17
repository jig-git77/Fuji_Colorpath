IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetRoleManagementData')
	BEGIN
		DROP  Procedure [CPV].[uspGetRoleManagementData]
	END

GO

/*  
AUTHOR : Shivpal Jhala
Created Date : July 18 2008  
Purpose : Procedure is created for the getting desired result for the Access Management [Role Management]
*/  
CREATE PROCEDURE [CPV].[uspGetRoleManagementData] 
AS    
BEGIN  
	BEGIN TRY    
		/*--- Creating the Temparory Table*/
		CREATE TABLE 
			#TmpRoleManagement
			(			
				[PermissionID] INT,
				[DisplayRow] NVARCHAR(100),
				[Country Admin] NVARCHAR(100),
				[Company Admin] NVARCHAR(100),
				[Branch Admin] NVARCHAR(100),
				[Print Buyer] NVARCHAR(100),
				[Support User] NVARCHAR(100),
				[Site User] NVARCHAR(100)
			)
		/*--- Inserting the Data into Temparory Table*/
		INSERT INTO 
			#TmpRoleManagement 
		SELECT 
			PermissionID,PermissionDesc,PermissionDesc,PermissionDesc,PermissionDesc,PermissionDesc,PermissionDesc,PermissionDesc	
		FROM 
			Permission
		/*--- Select Data from Temparory Table*/
		SELECT 
			* 
		FROM 
			#TmpRoleManagement
		/*--- Droping the Temparory Table*/
		DROP TABLE 
			#TmpRoleManagement
		/*--- Fetching the Data from RolePermission Table*/
		SELECT 
			RoleID,PermissionID 
		FROM 
			RolePermission
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

