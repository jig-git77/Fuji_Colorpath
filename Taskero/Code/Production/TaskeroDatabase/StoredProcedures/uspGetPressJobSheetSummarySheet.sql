IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobSheetSummarySheet')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobSheetSummarySheet]
	END
GO
/*
	AUTHOR : Maulik Dhorajia
	Date : Sept/30/2008
	Description : Retrieves the information of Job Summary for DeviceType Press per sheetnumber.		
*/
CREATE Procedure [Press].[uspGetPressJobSheetSummarySheet]( @JobID INT , @DongleID VARCHAR(10) , @SheetNumber INT )AS
BEGIN
	BEGIN TRY
		
		SELECT 
			PCalc.ZoneNumber
			, MAX(Sheets.ScannedDateTime) AS ScannedDateTime
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.deltaE) ) , 0 ) AS AvgDeltaE , ISNULL( CONVERT( DECIMAL(18,2) , MAX(PCalc.deltaE) ) , 0 ) AS MaxDeltaE , ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.deltaE) ) , 0 ) AS StdDEVdeltaE		
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.Density) ) , 0 ) AS AvgDensity
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.Dotgain) ) , 0 ) AS AvgDotgain
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.PrintContrast) ) , 0 ) AS AvgPrintContrast
		FROM Press.PatchCalculations AS PCalc
		INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID 
		WHERE Sheets.JobID = @JobID 
			AND Sheets.DongleID = @DongleID AND PCalc.DongleID = @DongleID
			AND Sheets.SheetNumber = @SheetNumber
			AND Sheets.IsReady = 1 AND Sheets.SheetState = 2
		GROUP BY PCalc.ZoneNumber
		ORDER BY PCalc.ZoneNumber
		
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
