IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTaskeroUserAndDetails')
	BEGIN
		DROP  Procedure [CPV].[uspGetTaskeroUserAndDetails]
	END

GO

/*      
AUTHOR : Shivpal Jhala      
LAST UPDATED DATE : July 23 , 2008      
PURPOSE :- Fetching the Users Data with its Details with 4 Selects
*/      
CREATE PROCEDURE [CPV].[uspGetTaskeroUserAndDetails]  
( 
	@TaskeroUserID int  
)       
AS      
BEGIN      
	BEGIN TRY  
		/*==Sellect query for fetching the TaskeroUser Data==*/		
		SELECT 
			TaskeroUser.LoginID , TaskeroUser.FirstName , TaskeroUser.LastName , TaskeroUser.Email , TaskeroUser.LoginPass
			, ( case TaskeroUser.IsActive WHEN 1 THEN 'YES' ELSE 'NO' END ) as IsActive
			, RoleID , Customer , AllowedIP , ISNULL(TaskeroUser.BrandID,0) as BrandID , ISNULL( BrandName , 'Default' ) AS BrandName
			, EmailWhenYellow , EmailWhenRed
		FROM 
			TaskeroUser	
			LEFT OUTER JOIN CompanyBranding ON CompanyBranding.BrandID = TaskeroUser.BrandID
		WHERE
			TaskeroUserID = @TaskeroUserID

		/*==Sellect query for fetching the UserCompany Data==*/
		SELECT 
			Company.CompanyName 
		FROM 
			UserCompany ,  Company 
		WHERE 
			UserCompany.CompanyID = Company.CompanyID 
			AND UserCompany.TaskeroUserID = @TaskeroUserID
		ORDER BY 
			Company.CompanyName
		
		/*==Sellect query for fetching the UserBranch Data==*/
		SELECT 
			Branch.BranchName 
		FROM 
			UserBranch , Branch 
		WHERE 
			UserBranch.BranchID = Branch.BranchID
			AND ISNULL(Branch.IsActive , 1) = 1
			AND UserBranch.TaskeroUserID = @TaskeroUserID
		ORDER BY
			Branch.BranchName 

		/*==Sellect query for fetching the UserDongle Data==*/
		SELECT  
			CPDongle.DongleDescription , CASE ISNULL(DongleDescription,'') WHEN '' THEN CPSerialNUM ELSE (CPSerialNUM+' - '+DongleDescription) END AS DongleSerial
		FROM  
			UserDongle ,  CPDongle 
		WHERE  
			UserDongle.CPDongleID = CPDongle.CPDongleID 
			AND ISNULL( CPDongle.IsUsed , 1 ) = 1
			AND UserDongle.TaskeroUserID = @TaskeroUserID
		ORDER BY
			CPDongle.DongleDescription
		
		/*==Sellect query for fetching the Permission Data==*/
		DECLARE @RID INT
		SET @RID = 0
		SELECT @RID = RoleID FROM TaskeroUser WHERE TaskeroUserID = @TaskeroUserID
		print @RID
		IF @RID = 0
		BEGIN
			
			SELECT 
				Permission.PermissionDesc 
			FROM 
				UserPermission , Permission
			WHERE
				UserPermission.PermissionID = Permission.PermissionID
				AND UserPermission.TaskeroUserID = @TaskeroUserID
			ORDER BY
				PermissionDesc
		END
		ELSE
		BEGIN
			SELECT 
				Permission.PermissionDesc 
			FROM 
				RolePermission , Permission
			WHERE
				RolePermission.PermissionID = Permission.PermissionID
				AND RolePermission.RoleID = @RID
			ORDER BY
				Permission.PermissionDesc 		
		END
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

