IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetBranchOfCompanyByUserIDAndCompanyID')
	BEGIN
		DROP  Procedure  [CPV].[uspGetBranchOfCompanyByUserIDAndCompanyID]
	END

GO

/*    
AUTHOR : Jitendra Makwana
Created Date : August 18 2008    
Purpose : To Get Branch of Company by UserID    
*/    
CREATE PROCEDURE [CPV].[uspGetBranchOfCompanyByUserIDAndCompanyID]     
(     
	@TaskeroUserID INT,      
	@CompanyID NVARCHAR(4000),  
	@RoleID TINYINT    
)     
AS     
BEGIN    
	BEGIN TRY    
		/*---------- SELECT ROLE LEVEL FOR THE USER ----------*/  
		SELECT 
			@RoleID = RoleLevel 
		FROM 
			TaskeroUser,Roles 
		WHERE 
			TaskeroUserID = @TaskeroUserID 
		AND 
			TaskeroUser.RoleID = Roles.RoleID 

		/*---------- ASSIGN SELECTED IDs TO XML ----------*/  
		DECLARE @xmlCompany XML    
		SET @xmlCompany = '<CompanyIDs><CompanyID>'+ REPLACE( @CompanyID ,',','</CompanyID><CompanyID>') +'</CompanyID></CompanyIDs>'

		/*----------- Get the Branch of Company by UserID ----------*/    
		IF @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)    
		BEGIN    
			SELECT     
				BranchID , BranchName      
			FROM     
				Branch    
			WHERE  
				CompanyID 
			IN
			(
				SELECT     
				   xmlCompany.value('.', 'INT') AS ID     
				 FROM     
				   @xmlCompany.nodes('/CompanyIDs/CompanyID') d(xmlCompany)
			)

			ORDER BY     
				BranchName      
		END    
		ELSE    
		BEGIN    
			SELECT     
				Branch.BranchID , Branch.BranchName      
			FROM     
				Branch , UserBranch     
			WHERE    
				Branch.BranchID = UserBranch.BranchID    
			AND 
				UserBranch.TaskeroUserID = @TaskeroUserID 
			AND 
				ISNULL(Branch.IsActive,1)	= 1   

			ORDER BY     
				Branch.BranchName      
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