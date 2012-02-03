IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetAllCompany')
	BEGIN
		DROP  Procedure [CPV].[uspGetAllCompany]
	END

GO

/*  
AUTHOR : Maulik Dhorajia  
Created Date : June 30 2008  
Purpose : To get all the company Names.  
*/  
CREATE PROCEDURE [CPV].[uspGetAllCompany] 
AS    
BEGIN  
 BEGIN TRY  
  
	/*------------- Get All Company List----------------*/
  	SELECT 
		CompanyID , CompanyName 
	FROM 
		Company 
	ORDER BY 
		CompanyName  
    
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
