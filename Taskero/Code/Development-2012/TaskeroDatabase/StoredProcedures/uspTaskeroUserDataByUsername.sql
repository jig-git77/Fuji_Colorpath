IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspTaskeroUserDataByUsername')
	BEGIN
		DROP  Procedure [CPV].[uspTaskeroUserDataByUsername]
	END

GO

/*          
AUTHOR : Shivpal Jhala          
Created Date : July 16, 2008          
Purpose : To Retrieve the User Data by Username and its Details Tables Data.          
*/          
CREATE PROCEDURE [CPV].[uspTaskeroUserDataByUsername]    
(  
	@LoginID NVARCHAR(100)
)  
AS   
BEGIN      
	BEGIN TRY        
		--***Retieving the data from TaskerUser Table**--
		SELECT 
			* 
		FROM 
			TaskeroUser 
		WHERE 
			LoginID = @LoginID

		--***Retieving the data from UserCompany Table**--
		SELECT 
			CompanyID 
		FROM 
			UserCompany ,TaskeroUser
		WHERE 
			TaskeroUser.TaskeroUserID = UserCompany.TaskeroUserID
			and TaskeroUser.LoginID = @LoginID
		
		--***Retieving the data from UserBranch Table**--
		SELECT 
			BranchID 
		FROM 
			UserBranch ,TaskeroUser
		WHERE 
			TaskeroUser.TaskeroUserID = UserBranch.TaskeroUserID
			and TaskeroUser.LoginID = @LoginID

		--***Retieving the data from UserDongle Table**--
		SELECT 
			CPDongleID 
		FROM 
			UserDongle ,TaskeroUser
		WHERE 
			TaskeroUser.TaskeroUserID = UserDongle.TaskeroUserID
			and TaskeroUser.LoginID = @LoginID

		--***Retieving the data from UserPermission Table**--
		SELECT 
			PermissionID 
		FROM 
			UserPermission ,TaskeroUser
		WHERE 
			TaskeroUser.TaskeroUserID = UserPermission.TaskeroUserID
			and TaskeroUser.LoginID = @LoginID
		
		--***Retieving the data from UserReport Table**--
		SELECT 
			ReportID 
		FROM 
			UserReport ,TaskeroUser
		WHERE 
			TaskeroUser.TaskeroUserID = UserReport.TaskeroUserID
			and TaskeroUser.LoginID = @LoginID
			
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




