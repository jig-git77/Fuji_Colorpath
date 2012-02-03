IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetCheckDuplicateCompany')
	BEGIN
		DROP  Procedure [CPV].[uspGetCheckDuplicateCompany]
	END

GO

/*
AUTHOR : Tushar Poshiya
Created Date : July 14 2008
Purpose : To check the duplication company Names.
*/
CREATE PROCEDURE [CPV].[uspGetCheckDuplicateCompany] 
( 
	@CompanyID INT , 
	@CompanyName NVARCHAR(100) 
)
AS  
BEGIN
	BEGIN TRY
		
		/*---------- Checks Duplicate Company Name ----------*/
		SELECT 
			CompanyName 
		FROM 
			[Company] 
		WHERE [CompanyName] = @CompanyName 
				AND [CompanyID] <> @CompanyID

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
