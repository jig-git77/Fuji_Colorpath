IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobSummary')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobSummary]
	END
GO
/*
	AUTHOR : Maulik Dhorajia
	Date : Aug/25/2008
	Description : Retrieves the information of Job Summary for DeviceType Press.	
	
	Updated By : Maulik Dhorajia
	Updated Date : Nov/4/2008
	Description : Changes Made to Optimize the query and resolve logical issues in the query.
*/
CREATE Procedure [Press].[uspGetPressJobSummary](@JobID INT , @DongleID VARCHAR(10))AS
BEGIN
	BEGIN TRY
				
		SELECT 
				PCalc.JobID
				, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.deltaE) ) , 0 ) AS AvgDeltaE
				, ISNULL( CONVERT( DECIMAL(18,2) , MAX(PCalc.deltaE) ) , 0 ) AS MaxDeltaE
				, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(PCalc.deltaE) ) , 0 ) AS StdDEVdeltaE
		FROM Press.PatchCalculations AS PCalc
		INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID AND Sheets.DongleID = PCalc.DongleID
		WHERE PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		AND Sheets.IsReady = 1 AND Sheets.SheetState = 2
		Group By PCalc.JobId
		
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

/*
		SELECT 
				Press.Jobs.JobID
				, ISNULL( CONVERT( DECIMAL(18,2) , AVG(deltaE) ) , 0 ) AS AvgDeltaE
				, ISNULL( CONVERT( DECIMAL(18,2) , MAX(deltaE) ) , 0 ) AS MaxDeltaE
				, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(deltaE) ) , 0 ) AS StdDEVdeltaE
		FROM Press.PatchCalculations 
		INNER JOIN Press.Sheets ON Press.PatchCalculations.SheetID = Press.Sheets.SheetID 
		INNER JOIN Press.Jobs ON Press.Jobs.JOBID = Press.Sheets.JobID 
		WHERE Press.Jobs.JobID = @JobID 
		AND Press.Sheets.DongleID = @DongleID AND Press.PatchCalculations.DongleID = @DongleID AND Press.Jobs.DongleID = @DongleID
		AND Press.Sheets.IsReady = 1 AND Press.Sheets.SheetState = 2
		Group By Press.Jobs.JobId
*/