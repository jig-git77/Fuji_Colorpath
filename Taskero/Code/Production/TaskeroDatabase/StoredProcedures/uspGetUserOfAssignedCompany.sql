IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetUserOfAssignedCompany')
	BEGIN
		DROP  Procedure [CPV].[uspGetUserOfAssignedCompany]
	END

GO

/*
AUTHOR : Jitendra Makwana
Created Date : July 29 2008  
Purpose : To Get users with assigned company
*/

CREATE PROCEDURE [CPV].[uspGetUserOfAssignedCompany] 
(
	@CompanyID INT,
	@RoleID INT
)
AS    
BEGIN  
	BEGIN TRY
			SELECT 
					TaskeroUser.TaskeroUserID , TaskeroUser.LoginID
			FROM 
					TaskeroUser
			LEFT OUTER JOIN 
					Roles ON Roles.RoleID = TaskeroUser.RoleID
			LEFT OUTER JOIN 
					UserCompany ON UserCompany.TaskeroUserID = TaskeroUser.TaskeroUserID
			LEFT OUTER JOIN 
					Company ON Company.CompanyID = UserCompany.CompanyID
			WHERE 
					TaskeroUser.RoleID = @RoleID 
			AND 
					Company.CompanyID = @CompanyID
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