IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAssignSupportUserOfBranchID')
	BEGIN
		DROP  Procedure [CPV].[uspGetAssignSupportUserOfBranchID]
	END

GO

/*
AUTHOR : Tushar Patel
Created Date : July 25 2008
Purpose : To get all the branch names per CompanyID.
*/
CREATE PROCEDURE [CPV].[uspGetAssignSupportUserOfBranchID]
(
	@TaskeroUserID INT,    
	@RoleID TINYINT ,  
	@BranchID INT
) 
AS  
BEGIN
	BEGIN TRY		
	IF @RoleID  = 1 -- IF Role is 1 (Taskero Admin we need to show all the SupportUsers)  
	BEGIN
		SELECT 
			TaskeroUser.TaskeroUserID
			, TaskeroUser.LoginID
			, TaskeroUser.FirstName
			, TaskeroUser.LastName
			, TaskeroUser.Email
		FROM
			TaskeroUser 	
		ORDER BY 
			LoginID
	END
	ELSE
	BEGIN
		SELECT 
			UserBranch.TaskeroUserID
			, TaskeroUser.LoginID
			, TaskeroUser.FirstName
			, TaskeroUser.LastName
			, TaskeroUser.Email
		FROM
			UserBranch 
			INNER JOIN TaskeroUser ON UserBranch.TaskeroUserID = TaskeroUser.TaskeroUserID
		WHERE 
			UserBranch.BranchID = @BranchID
		ORDER BY 
			LoginID
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


