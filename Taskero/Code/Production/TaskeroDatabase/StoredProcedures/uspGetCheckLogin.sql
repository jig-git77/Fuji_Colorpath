IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCheckLogin')
	BEGIN
		DROP  Procedure [CPV].[uspGetCheckLogin]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : June 30 2008
Purpose : To Check the login.
*/
CREATE PROCEDURE [CPV].[uspGetCheckLogin]
( 
	@LoginID VARCHAR(100) , 
	@LoginPass VARCHAR(40) ,
	@AllowedIP Varchar(15)
) 
AS  
BEGIN
	BEGIN TRY
			
		/*---------- Gets The User Information ----------*/
		SELECT TaskeroUserID , LoginID , LoginPass , LastName , FirstName , Email , ISNULL(RoleID,0) AS RoleID
		, (
				CASE @AllowedIP 
				WHEN '127.0.0.1' THEN 1				
				ELSE
					(
						CASE ISNULL( AllowedIP , '' )
						WHEN '' THEN 1
						ELSE
							(
								CASE  
								WHEN @AllowedIP LIKE REPLACE( AllowedIP , '*' , '%' ) THEN 1 
								ELSE 0
								END
							)
						END
					)
				END
			)	AS IPAccess
		FROM TaskeroUser 
		WHERE LoginID = @LoginID 
		AND LoginPass = @LoginPass 
		AND ISNULL(IsActive,1) = 1	
			

		IF @@ROWCOUNT > 0
		BEGIN

			/*---------- DECLARE ----------*/
			DECLARE @USERID INT
			DECLARE @ROLEID INT
			DECLARE @BRANDID INT

			/*---------- Get RoleID and UserID ----------*/
			SELECT @USERID = TaskeroUserID , @ROLEID = RoleID , @BRANDID = ISNULL(BrandID,0)
			FROM TaskeroUser 
			WHERE LoginID = @LoginID AND LoginPass = @LoginPass AND ISNULL(IsActive,1) = 1

			/*---------- Gets User Roles ----------*/
			SELECT RoleID , RoleDesc , RoleLevel FROM Roles WHERE RoleID = @ROLEID	

			/*---------- Gets User Permissions On ROle ----------*/
			SELECT Permission.PermissionID , Permission.PermissionDesc 
			FROM Permission
			INNER JOIN RolePermission ON RolePermission.PermissionID = Permission.PermissionID AND RolePermission.RoleID = @ROLEID

			/*---------- Gets User Permissions On UserID ----------*/
			SELECT Permission.PermissionID , Permission.PermissionDesc 
			FROM Permission
			INNER JOIN UserPermission ON UserPermission.PermissionID = Permission.PermissionID AND UserPermission.TaskeroUserID = @UserID

			/*---------- Gets Company ID And Brand ID----------*/
			SELECT 
				UserCompany.CompanyID				
			FROM 
				UserCompany 
				LEFT OUTER JOIN Company ON UserCompany.CompanyID = Company.CompanyID
			WHERE TaskeroUserID = @UserID
			

			/*---------- Gets Branch ID ----------*/
			Select BranchID FROM UserBranch WHERE TaskeroUserID = @UserID

			/*---------- Gets Dongle ID ----------*/
			SELECT CPDongleID FROM UserDongle WHERE TaskeroUserID = @UserID
			
			/*---------- Gets CSS AND Logo Image File Name ----------*/
			IF @BRANDID > 0 
				BEGIN 
					
					SELECT 
						Css 
						, (CASE ISNULL( CompanyBranding.HeaderLogo , '' )  WHEN '' THEN 'Logo.gif' ELSE CompanyBranding.HeaderLogo END) AS HeaderLogo
						, (CASE ISNULL( CompanyBranding.LargeImage , '' ) WHEN '' THEN 'taskero-uni-logo-big.gif' ELSE CompanyBranding.LargeImage END)  AS LargeImage
					FROM 
						TaskeroUser
						LEFT OUTER JOIN CompanyBranding ON CompanyBranding.BrandID = TaskeroUser.BrandID
					WHERE 
						TaskeroUser.TaskeroUserID = @USERID
											
				END
			ELSE
				BEGIN
				
					SELECT 
						(CASE ISNULL( CompanyBranding.Css , '' ) WHEN '' THEN '' ELSE CompanyBranding.Css END) AS CSS
						, (CASE ISNULL( CompanyBranding.HeaderLogo , '' )  WHEN '' THEN 'Logo.gif' ELSE CompanyBranding.HeaderLogo END) AS HeaderLogo
						, (CASE ISNULL( CompanyBranding.LargeImage , '' ) WHEN '' THEN 'taskero-uni-logo-big.gif' ELSE CompanyBranding.LargeImage END)  AS LargeImage
					FROM 
						UserCompany 
						LEFT OUTER JOIN CompanyBranding ON UserCompany.CompanyID = CompanyBranding.CompanyID
						INNER JOIN Company ON Company.CompanyID = CompanyBranding.CompanyID AND Company.BrandID = CompanyBranding.BrandID
					WHERE 
						TaskeroUserID = @UserID
				
				END			
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


