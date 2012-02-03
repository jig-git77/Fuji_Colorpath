IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCompanyByUserID')
	BEGIN
		DROP  Procedure [CPV].[uspGetCompanyByUserID]
	END

GO

/*      
AUTHOR : Amit Patel      
Created Date : July 14 2008      
Purpose : To get the company Names by User ID.      
*/      
CREATE PROCEDURE [CPV].[uspGetCompanyByUserID] 
( 
	@TaskeroUserID INT ,
	@RoleID TINYINT 
)
AS         
BEGIN      
 BEGIN TRY      
      
      SELECT @RoleID = RoleLevel from TaskeroUser,Roles where TaskeroUserID = @TaskeroUserID AND TaskeroUser.RoleID = Roles.RoleID 
  /*------ Get Company By User ID -----*/ 
	IF @RoleID = 1       
	BEGIN
		SELECT     
			Company.CompanyID , Company.CompanyName     
		FROM     
			Company
		Order BY 
			Company.CompanyName
		END
	ELSE
	BEGIN
		SELECT 
			Company.CompanyID , CompanyName 
		FROM Company 
		INNER JOIN UserCompany ON UserCompany.CompanyID = Company.CompanyID
		WHERE 
			UserCompany.TaskeroUserID = @TaskeroUserID
		ORDER BY 
			CompanyName
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
  
  