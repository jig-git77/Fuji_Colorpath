IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetBranchByCompanyID')
	BEGIN
		DROP  Procedure [CPV].[uspGetBranchByCompanyID]
	END

GO

/*
AUTHOR : Jitendra Makwana
Created Date : July 14 2008
Purpose : To get all the Branch List Based on the CompanyId passed with.
*/
CREATE PROCEDURE [CPV].[uspGetBranchByCompanyID]
( 
	@CompanyID INT 
) 
AS  
BEGIN
	BEGIN TRY
		
		/*---------- Gets Branch By Company Id ----------*/
		SELECT 
			CompanyName , BranchID , Branch.CompanyID , BranchName , BranchLocation , PrimaryContactName 
			, Tel_countryCode , Tel_PhoneNo , PrimaryContactEmail , PrimaryContactCell 
			, (CASE ISNULL( IsActive , 1 ) WHEN 0 THEN 'No' WHEN 1 THEN 'Yes' END) AS IsActive 
			,  (Tel_countryCode + ' ' + Tel_PhoneNo + ' ' + Tel_Extension) AS Telephone
		FROM 
			Branch 
		LEFT OUTER JOIN Company ON Branch.CompanyID = Company.CompanyID
		WHERE 
			Company.CompanyID = @CompanyID
		AND 
			ISNULL( Branch.IsActive , 1 )  = 1 
		ORDER BY
			BranchName
		
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


