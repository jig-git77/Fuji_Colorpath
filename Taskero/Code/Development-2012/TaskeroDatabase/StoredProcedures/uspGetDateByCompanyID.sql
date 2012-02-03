IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetDateByCompanyID')
	BEGIN
		DROP  Procedure [CPV].[uspGetDateByCompanyID]
	END

GO

/*
AUTHOR : Maulik Dhorajia
Created Date : Oct 03 2008
Purpose : To get the Minimum and Maximum date by Branch ID .
*/
CREATE PROCEDURE [CPV].[uspGetDateByCompanyID] 
( 
	@CompanyID INT 
) 	
AS     
BEGIN
	BEGIN TRY
		
		/*----- Get The Dates Per Branch -----*/
		SELECT 
			CONVERT(VARCHAR, MIN(CPMeasurement.CreatedDT), 101) AS MINDATE
			, CONVERT(VARCHAR, MAX(CPMeasurement.CreatedDT), 101) AS MAXDATE 
		FROM 
			CPMeasurement 
			INNER JOIN CPDongle ON CPMeasurement.CPDongleID = CPDongle.CPDongleID 
			INNER JOIN Branch ON Branch.BranchID = CPDongle.BranchID
			INNER JOIN Company ON Company.CompanyID= Branch.CompanyID
		WHERE
			Company.CompanyID  = @CompanyID	

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

