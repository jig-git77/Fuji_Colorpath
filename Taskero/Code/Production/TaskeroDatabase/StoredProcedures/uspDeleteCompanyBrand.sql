IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspDeleteCompanyBrand')
	BEGIN
		DROP  Procedure  [CPV].[uspDeleteCompanyBrand]
	END

GO

/*
AUTHOR : Amit Patel
Created Date : Aug 18 2008
Purpose : Delete an Existing Branding Data.
*/ 
CREATE PROCEDURE [CPV].[uspDeleteCompanyBrand] 
( 
	@BrandID INT,
	@Result int output  
) 
AS   
BEGIN  
	BEGIN TRY  

		/*---------- Delete an Existing Device ----------*/
  		DELETE FROM
			CompanyBranding		
		WHERE
			[BrandID] = @BrandID
		SET @Result = 1

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

