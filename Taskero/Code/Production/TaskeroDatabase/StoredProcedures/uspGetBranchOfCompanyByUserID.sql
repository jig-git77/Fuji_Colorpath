IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetBranchOfCompanyByUserID')
	BEGIN
		DROP  Procedure [CPV].[uspGetBranchOfCompanyByUserID] 
	END

GO

/*    
AUTHOR : Tushar Patel    
Created Date : July 17 2008    
Purpose : To Get Branch of Company by UserID    
*/    
CREATE PROCEDURE [CPV].[uspGetBranchOfCompanyByUserID]     
(     
	@TaskeroUserID INT,      
	@CompanyID INT,  
	@RoleID TINYINT    
)     
AS     
BEGIN    
 BEGIN TRY    
    
    SELECT @RoleID = RoleLevel from TaskeroUser,Roles where TaskeroUserID = @TaskeroUserID AND TaskeroUser.RoleID = Roles.RoleID 
  /*----------- Get the Branch of Company by UserID ----------*/    
  if @RoleID= 1 -- if Role is 1 (Taskero Admin we need to show all the branches)    
	  BEGIN    
		SELECT     
			BranchID , BranchName      
		FROM     
			Branch    
		WHERE  
			CompanyID = @CompanyID 
			--and  ISNULL(IsActive,1)	= 1
	   ORDER BY     
			BranchName      
	  END    
  ELSE    
	  BEGIN    

		SELECT
			Branch.BranchID , BranchName 
		FROM
			Branch
			INNER JOIN UserBranch ON UserBranch.BranchID = Branch.BranchID
		WHERE
			UserBranch.TaskeroUserID = @TaskeroUserID
			AND Branch.CompanyID = @CompanyID
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

