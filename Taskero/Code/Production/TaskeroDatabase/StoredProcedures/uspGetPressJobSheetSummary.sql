IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobSheetSummary')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobSheetSummary]
	END
GO
/*
	AUTHOR : Maulik Dhorajia
	Date : Aug/25/2008
	Description : Retrieves the information of Job Summary for DeviceType Press.	
	
	Updated By : Maulik Dhorajia
	Updated Date : Sept/9/2008
	Description : Added "AND Press.Sheets.IsReady = 1 AND Press.Sheets.SheetState = 2".
*/
CREATE Procedure [Press].[uspGetPressJobSheetSummary](@JobID INT , @DongleID VARCHAR(10) )AS
BEGIN
	BEGIN TRY
		
		SELECT 
			Sheets.SheetNumber
			, MAX(ScannedDateTime) AS ScannedDateTime
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.deltaE) ) , 0 ) AS AvgDeltaE , ISNULL( CONVERT( DECIMAL(18,2) , MAX(PCalc.deltaE) ) , 0 ) AS MaxDeltaE , ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.deltaE) ) , 0 ) AS StdDEVdeltaE		
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.Density) ) , 0 ) AS AvgDensity
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.Dotgain) ) , 0 ) AS AvgDotgain
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.PrintContrast) ) , 0 ) AS AvgPrintContrast
		FROM Press.PatchCalculations AS PCalc
		INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID AND Sheets.JobID = PCalc.JobID AND Sheets.DongleID = PCalc.DongleID 
		WHERE PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
			AND Sheets.IsReady = 1 AND Sheets.SheetState = 2
		GROUP BY Sheets.SheetNumber
		ORDER BY Sheets.SheetNumber
		
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
