IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetTaskeroUserPassingRoleID')
	BEGIN
		DROP  Procedure [CPV].[uspGetTaskeroUserPassingRoleID]
	END

GO

/*        
AUTHOR : Jitendra Makwana
LAST UPDATED DATE : august 18 , 2008        
PURPOSE :- Get All Support User
*/        
CREATE PROCEDURE [CPV].[uspGetTaskeroUserPassingRoleID]    
(   
	@TaskeroUserID INT , 
	@RoleID INT 
)         
AS         
BEGIN       
 
BEGIN TRY   
	DECLARE @UserRoleID INT
	SELECT 
		@UserRoleID = RoleID 
	FROM
		TaskeroUser 
	WHERE 
		TaskeroUserID = @TaskeroUserID 
	/*--------- Gets the role for the current user ----------*/
	IF  @UserRoleID = 1    /*--------- @RoleID = 1 = Tasker Admin ----------*/
		 BEGIN    
		  
		  SELECT 
			DISTINCT TaskeroUser.LoginID
				, TaskeroUser.LastName
				, TaskeroUser.FirstName
				, TaskeroUser.Email
				, TaskeroUser.LastModifiedDate
				, TaskeroUser.IsActive  
				,(SELECT [CPV].[funGetAllCompanyNameByUserID](UserCompany.TaskeroUserID))As CompanyName
				,(SELECT [CPV].[funGetAllBranchNameByUserID](UserCompany.TaskeroUserID))As BranchName
		  FROM TaskeroUser
			LEFT OUTER JOIN Roles ON ISNULL(TaskeroUser.RoleID,0) = Roles.RoleID
			LEFT OUTER JOIN UserBranch ON TaskeroUser.TaskeroUserID = UserBranch.TaskeroUserID
			LEFT OUTER JOIN UserDongle ON TaskeroUser.TaskeroUserID = UserDongle.TaskeroUserID
			LEFT OUTER JOIN UserCompany ON TaskeroUser.TaskeroUserID = UserCompany.TaskeroUserID
			LEFT OUTER JOIN Company ON Company.CompanyID = UserCompany.CompanyID
			LEFT OUTER JOIN Branch ON Branch.BranchID = UserBranch.BranchID
			LEFT OUTER JOIN CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID  
			WHERE 
				TaskeroUser.RoleID = 6
			
		  ORDER BY 
			TaskeroUser.LoginID,TaskeroUser.FirstName,TaskeroUser.LastName
		 
		 END    
	 ELSE /*--------- All Support Users By Access----------*/
	 BEGIN    
	  SELECT distinct 
			TaskeroUser.LoginID
			, TaskeroUser.LastName
			, TaskeroUser.FirstName
			, TaskeroUser.Email
			, TaskeroUser.LastModifiedDate
			, TaskeroUser.IsActive
			,(SELECT [CPV].[funGetAllCompanyNameByUserID](UserCompany.TaskeroUserID))As CompanyName
			,(SELECT [CPV].[funGetAllBranchNameByUserID](UserCompany.TaskeroUserID))As BranchName
	  FROM 
		TaskeroUser  
		LEFT OUTER JOIN Roles ON ISNULL(TaskeroUser.RoleID,0) = Roles.RoleID
		LEFT OUTER JOIN UserBranch ON TaskeroUser.TaskeroUserID = UserBranch.TaskeroUserID
		LEFT OUTER JOIN UserDongle ON TaskeroUser.TaskeroUserID = UserDongle.TaskeroUserID
		LEFT OUTER JOIN UserCompany ON TaskeroUser.TaskeroUserID = UserCompany.TaskeroUserID
		LEFT OUTER JOIN Company ON Company.CompanyID = UserCompany.CompanyID
		LEFT OUTER JOIN Branch ON Branch.BranchID = UserBranch.BranchID
		LEFT OUTER JOIN CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID  
	  WHERE 
		TaskeroUser.RoleID = @RoleID
	  AND 
		TaskeroUser.TaskeroUserID in    
		  (    
			SELECT 
				DISTINCT TaskeroUserID   
			FROM 
				UserDongle    
			WHERE 
				CPDongleID in ( 
								SELECT 
										CPDongleID 
								FROM 
									UserDongle 
								WHERE 
									TaskeroUserID = @TaskeroUserID 
								)       
		  )    
	  AND ISNULL(Branch.IsActive,1) = 1 
	  AND isnull(CPDongle.IsUsed,1) = 1     
	  ORDER BY TaskeroUser.LoginID,TaskeroUser.FirstName,TaskeroUser.LastName    
	 
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