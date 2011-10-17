IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspDeleteCompany')
	BEGIN
		DROP  Procedure  [CPV].uspDeleteCompany
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 15 2008
Purpose : Delete a Company.
*/ 
CREATE PROCEDURE [CPV].[uspDeleteCompany] 
( 
	@CompanyID INT 
) 
AS   
BEGIN  
	BEGIN TRY  

		/*---------- Delete an Existing Company ----------*/
  		DELETE FROM 
			Company
		WHERE 
			[CompanyID] = @CompanyID

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

