IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllBranchWithCompanyName')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllBranchWithCompanyName]
	END

GO

/*    
AUTHOR : Jitendra Makwana
Created Date : July 15 2008    
Purpose : To get all the Branch List.    
*/    
CREATE PROCEDURE [CPV].[uspGetAllBranchWithCompanyName]
(
	@TaskeroUserID INT,
	@RoleID INT
)
AS      
BEGIN    
	BEGIN TRY
		IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)  
		BEGIN 
			/*---------- Gets All Branch ----------*/
			SELECT 
				Company.CompanyID , CompanyName , BranchID , Branch.CompanyID , BranchName , BranchLocation , PrimaryContactName 
				, Tel_countryCode ,Tel_PhoneNo , PrimaryContactEmail , PrimaryContactCell 
				, (CASE ISNULL( IsActive , 1 ) WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) AS IsActive 
				, ISNULL((Tel_countryCode + ' ' + Tel_PhoneNo + ' ' + Tel_Extension),' ') AS Telephone
			FROM 
				Branch 
			    LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
			ORDER BY
				BranchName
		END
		ELSE
		BEGIN
			/*---------- Gets All Branch By UserID----------*/
			SELECT 
				Company.CompanyID , CompanyName , Branch.BranchID , Branch.CompanyID , BranchName , BranchLocation , PrimaryContactName 
				, Tel_countryCode , Tel_PhoneNo , PrimaryContactEmail , PrimaryContactCell 
				, (CASE ISNULL( IsActive , 1 ) WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) AS IsActive 
				,  (Tel_countryCode + ' ' + Tel_PhoneNo + ' ' + Tel_Extension) AS Telephone
			FROM 
				Branch
				LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
				LEFT OUTER JOIN UserBranch ON Branch.BranchID = UserBranch.BranchID
			WHERE 
				Branch.BranchID = UserBranch.BranchID  
				AND UserBranch.TaskeroUserID = @TaskeroUserID
				-- AND ISNULL( Branch.IsActive , 1 )  = 1 
			--	Company.CompanyID IN
			--	(
			--		SELECT     
			--			Company.CompanyID 
			--		FROM     
			--			Company , UserCompany    
			--		WHERE     
			--			UserCompany.CompanyID = Company.CompanyID    
			--			and UserCompany.TaskeroUserID = 85
			--	)
			ORDER BY
				BranchName
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




