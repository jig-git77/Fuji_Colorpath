IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressChartZoneSelection')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressChartZoneSelection]
	END

GO
/*
	Author :- Maulik Dhorajia
	Date :- Aug 29 2008
	Description :- The query will give 2 results 
		1. The record with Zone chart Infromation to be plottede with DeltaE and ZoneNumber.
		2. The information of the colors per zone.
		
	Updated By : Maulik Dhorajia
	Updated Date : Sept/9/2008
	Description : Added "AND Press.Sheets.IsReady = 1 AND Press.Sheets.SheetState = 2".
*/
CREATE Procedure [Press].[uspGetPressChartZoneSelection]( @JobID INT , @DongleID VARCHAR(10) ) 
AS
BEGIN
	BEGIN TRY 

	/*---------- Gets the information for the chart ----------*/
	EXEC Press.uspGetPressJobZoneSummary @JobID , @DongleID ;

/*---------- Gets the colors ----------*/
	SELECT
		PCalc.ZoneNumber
		, Colors.ColorID
		, Colors.ColorName
		, Press.fnGetShortStringForColor(Colors.ColorName) AS ColorName1
		, Colors.HTMLColor
		, MIN(PMP.LocationFromBeginSheet) AS LocationFromBeginSheet
		, CONVERT( DECIMAL(18,2) , AVG( ISNULL(PCalc.DeltaE , 0 ) ) ) as DeltaE	

/*---------- TARGET ----------*/
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PMP.CIELAB_L ) ) ) , 'NA' ) as Target_L
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PMP.CIELAB_A ) ) ) , 'NA' ) as Target_A
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PMP.CIELAB_B ) ) ) , 'NA' ) as Target_B
		, 'NA' as Target_Hue
		, 'NA' as Target_Chroma
		, 'NA' as Target_DeltaH
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.PrintContrast ) ) ) , 'NA' ) as Target_PrintContrast
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.Trap ) ) ) , 'NA' ) as Target_Trap
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.DotGain ) ) ) , 'NA' ) as Target_DotGain		
/*---------- TARGET ----------*/

/*---------- ACTUAL ----------*/
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.CIELAB_L ) ) ) , 'NA') as ACTUAL_L
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.CIELAB_A ) ) ) , 'NA') as ACTUAL_A
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.CIELAB_B ) ) ) , 'NA') as ACTUAL_B
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.HUE ) ) ) , 'NA') as ACTUAL_Hue
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.Chroma ) ) ) , 'NA') as ACTUAL_Chroma
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.DeltaH ) ) ) , 'NA') as ACTUAL_DeltaH
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.PrintContrast ) ) ) , 'NA') as ACTUAL_PrintContrast
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.Trap ) ) ) , 'NA') as ACTUAL_Trap
		, ISNULL( CONVERT( VARCHAR , CONVERT( DECIMAL(18,2) , AVG( PCalc.Dotgain ) ) ) , 'NA') as ACTUAL_Dotgain
/*---------- ACTUAL ----------*/

	FROM 
		Press.PatchCalculations AS PCalc

		INNER JOIN Press.Sheets AS Sheets ON 
			Sheets.SheetID = PCalc.SheetID 
			AND Sheets.JobID = PCalc.JobID 
			AND Sheets.DongleID = PCalc.DongleID 

		INNER JOIN Press.Colors AS Colors ON 
			Colors.ColorID = PCalc.ColorID  
			AND Colors.JobID = PCalc.JobID 
			AND Colors.DongleID = PCalc.DongleID

		INNER JOIN Press.MeasuredPatches AS PMP ON 
			PMP.ColorName = Colors.ColorName 
			AND PMP.JobID = PCalc.JobID 
			AND PMP.DongleID = PCalc.DongleID

	WHERE Sheets.IsReady = 1 AND Sheets.SheetState = 2
	AND PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
	GROUP BY PCalc.ZoneNumber	, Colors.ColorID , Colors.ColorName	, Colors.HTMLColor 
	ORDER BY PCalc.ZoneNumber , LocationFromBeginSheet

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

