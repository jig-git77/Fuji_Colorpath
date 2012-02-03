IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressPatchTrendChartPerZoneNumber')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressPatchTrendChartPerZoneNumber]
	END
GO
/*
	Name :- Maulik Dhorajia
	Date :- Sept/3/2008
	Description :- Patch Trend Chart (Trend of the Patch across Sheets in the same Zone number) 
*/
CREATE Procedure [Press].[uspGetPressPatchTrendChartPerZoneNumber]( @JobID INT , @DongleID VARCHAR(10) , @ZoneNumber INT , @ColorID INT  )
AS
BEGIN
	BEGIN TRY 
	
	/*---------- Retrieves The Delta E ----------*/
	SELECT 
		SheetNumber
		--, AVG(PCalc.DeltaE) AS AvgDeltaE
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.DeltaE) ) , 0 ) AS AvgDeltaE
	FROM 
		Press.PatchCalculations PCalc
		
		INNER JOIN Press.Sheets Sheets ON Sheets.SheetID = PCalc.SheetID
			AND Sheets.JobID = PCalc.JobID AND Sheets.DongleID = PCalc.DongleID
			AND Sheets.IsReady = 1 AND Sheets.SheetState = 2 
			--AND Sheets.SheetNumber = @SheetNumber
	WHERE
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		
	GROUP BY
		SheetNumber
	
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

