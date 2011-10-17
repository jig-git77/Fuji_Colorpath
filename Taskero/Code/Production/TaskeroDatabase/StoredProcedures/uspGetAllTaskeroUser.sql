IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllTaskeroUser')
	BEGIN
		DROP  Procedure  [CPV].[uspGetAllTaskeroUser]
	END

GO

/*========== INSERT ==========*/        
/*        
AUTHOR : Amit Patel
LAST UPDATED DATE : July 15 , 2008        
PURPOSE :- Get All User        
*/        
CREATE PROCEDURE [CPV].[uspGetAllTaskeroUser]    
(   
 @TaskeroUserID int    
)         
AS         
BEGIN       
 
BEGIN TRY   
	/*--------- Gets the role for the current user ----------*/
	DECLARE @RoleID int 
	SELECT 
			@RoleID = RoleLevel 
	FROM TaskeroUser , Roles 
	WHERE 
		TaskeroUserID = @TaskeroUserID 
		AND ISNULL(TaskeroUser.RoleID,0) = Roles.RoleID 

	 IF  @RoleID = 1    /*--------- @RoleID = 1 = Tasker Admin ----------*/
		 BEGIN    
		  
		  SELECT 
			DISTINCT TaskeroUser.LoginID
				, TaskeroUser.LastName
				, TaskeroUser.FirstName
				, TaskeroUser.Email
				, TaskeroUser.LastModifiedDate
				, TaskeroUser.IsActive  
		  FROM TaskeroUser
			LEFT OUTER JOIN Roles ON ISNULL(TaskeroUser.RoleID,0) = Roles.RoleID
			--LEFT OUTER JOIN UserBranch ON TaskeroUser.TaskeroUserID = UserBranch.TaskeroUserID
			--LEFT OUTER JOIN UserDongle ON TaskeroUser.TaskeroUserID = UserDongle.TaskeroUserID
			--LEFT OUTER JOIN Branch ON Branch.BranchID = UserBranch.BranchID
			--LEFT OUTER JOIN CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID  
			WHERE 
			(	
				Roles.RoleLevel > 1 
				OR ISNULL(TaskeroUser.RoleID,0) = 0 
			)
			--AND isnull(Branch.IsActive,1) = 1 AND isnull(CPDongle.IsUsed,1) = 1     
		  ORDER BY 
			TaskeroUser.LoginID,TaskeroUser.FirstName,TaskeroUser.LastName    
		 
		 END    
	 ELSE /*--------- All Other Users ----------*/
	 BEGIN    
	  SELECT 
			TaskeroUser.LoginID
			, TaskeroUser.LastName
			, TaskeroUser.FirstName
			, TaskeroUser.Email
			, TaskeroUser.LastModifiedDate
			, TaskeroUser.IsActive  
	  FROM 
		TaskeroUser  
		LEFT OUTER JOIN Roles ON ISNULL(TaskeroUser.RoleID,0) = Roles.RoleID
		LEFT OUTER JOIN UserBranch ON TaskeroUser.TaskeroUserID = UserBranch.TaskeroUserID
		LEFT OUTER JOIN UserDongle ON TaskeroUser.TaskeroUserID = UserDongle.TaskeroUserID
		LEFT OUTER JOIN Branch ON Branch.BranchID = UserBranch.BranchID
		LEFT OUTER JOIN CPDongle ON CPDongle.CPDongleID = UserDongle.CPDongleID  
	  WHERE 
		(
			Roles.RoleLevel > @RoleID 
			OR ISNULL(TaskeroUser.RoleID,0) = 0 
		)  
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




