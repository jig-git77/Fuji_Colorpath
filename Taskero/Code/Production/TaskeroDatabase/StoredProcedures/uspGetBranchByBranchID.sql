IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetBranchByBranchID')
	BEGIN
		DROP  Procedure [CPV].[uspGetBranchByBranchID]
	END

GO

/*
AUTHOR : Jitendra Makwana
Created Date : July 14 2008
Purpose : To get Data of the particular Branch 
*/
CREATE PROCEDURE [CPV].[uspGetBranchByBranchID] 
( 
	@BranchID INT 
) 
AS  
BEGIN
	BEGIN TRY

		/*----------- Gets the Branch on BranchID ----------*/
		SELECT 
			BranchID , CompanyID , BranchName , BranchLocation 
			, PrimaryContactName , Tel_countryCode , Tel_PhoneNo 
			, Tel_Extension  , PrimaryContactEmail 
			, PrimaryContactCell , ISNULL( IsActive , 1 ) as IsActive 
		FROM 
			Branch 
		WHERE 
			BranchID = @BranchID
		
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