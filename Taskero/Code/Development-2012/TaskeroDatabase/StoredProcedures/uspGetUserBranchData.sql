IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetUserBranchData')
	BEGIN
		DROP  Procedure [CPV].[uspGetUserBranchData] 
	END

GO

/*  
AUTHOR : Jitendra Makwana
Created Date : July 28 2008  
Purpose : Get User Branch Data On USer ID
*/

CREATE PROCEDURE [CPV].[uspGetUserBranchData] 
(
	@TaskeroUserID INT
)
AS    
BEGIN  
	BEGIN TRY
		/*------------- Select all user Branch Data----------------*/ 
		SELECT  
			BranchID
		FROM
			UserBranch
		WHERE
			TaskeroUserID = @TaskeroUserID 
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

