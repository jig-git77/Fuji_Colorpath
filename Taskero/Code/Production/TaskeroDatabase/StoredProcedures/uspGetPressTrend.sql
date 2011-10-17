IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressTrend')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressTrend]
	END
GO
/*
	Name : Maulik Dhorajia
	Date : Sept 10 2008
	Description : The is will get all the information for the Trend chart of Press.
	
	Updated By : Maulik Dhorajia
	Date : Sept 12 2008
*/
CREATE Procedure [Press].[uspGetPressTrend]( @JobID INT , @DongleID VARCHAR(10) ) AS
BEGIN
	BEGIN TRY
		
		-- Print DeltaE And Density --
	SELECT 
		PCalc.ZoneNumber
		, Colors.ColorID
		, Colors.ColorName
		, IWells.PrimaryColorType
		, MAX(Sheets.ScannedDateTime) AS ScannedDateTime
		, ISNULL( AVG(PCalc.DeltaE) , 0 ) AS AvgDeltaE
		, ISNULL( AVG(PCalc.Density) , 0 ) AS AvgDensity

	FROM 
		Press.PatchCalculations AS PCalc

		INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
			AND Sheets.JobID = PCalc.JobID AND Sheets.DongleID = PCalc.DongleID
			AND Sheets.SheetState = 2 AND Sheets.IsReady = 1		

		INNER JOIN Press.Colors AS Colors ON Colors.ColorID = PCalc.ColorID
			AND Colors.JobID = PCalc.JobID AND Colors.DongleID = PCalc.DongleID
			AND Colors.ColorTypeID = 0

		INNER JOIN Press.InkWells IWells ON IWells.InkName = Colors.ColorName 
			AND IWells.JobID = PCalc.JobID AND IWells.DongleID = PCalc.DongleID
			AND IWells.IsUsed = 1
	WHERE	
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
	GROUP BY 
		Colors.ColorID, Colors.ColorName, PCalc.ZoneNumber , IWells.PrimaryColorType
	ORDER BY 
		PCalc.ZoneNumber , ScannedDateTime
		
-- Print Target And DotGain --
		
	SELECT 
		PCalc.ZoneNumber
		, Colors.ColorID
		, Colors.ColorName
		, IWells.PrimaryColorType
		, MAX(Sheets.ScannedDateTime) AS ScannedDateTime
		, ISNULL( AVG(PCalc.DotGain) , 0 ) AS AvgDotGain
		, ISNULL( AVG(PCalc.PrintContrast) , 0 ) AS AvgPrintContrast

	FROM 
		Press.PatchCalculations AS PCalc

		INNER JOIN Press.Sheets AS Sheets ON Sheets.SheetID = PCalc.SheetID
			AND Sheets.JobID = PCalc.JobID AND Sheets.DongleID = PCalc.DongleID
			AND Sheets.SheetState = 2 AND Sheets.IsReady = 1		

		INNER JOIN Press.Colors AS Colors ON Colors.ColorID = PCalc.ColorID
			AND Colors.JobID = PCalc.JobID AND Colors.DongleID = PCalc.DongleID
			AND Colors.ColorTypeID = 1

		INNER JOIN Press.InkWells IWells ON Colors.ColorName LIKE '%'+ IWells.InkName +'%'
			AND IWells.JobID = PCalc.JobID AND IWells.DongleID = PCalc.DongleID
			AND IWells.IsUsed = 1
	WHERE	
		PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
	GROUP BY 
		Colors.ColorID, Colors.ColorName, PCalc.ZoneNumber , IWells.PrimaryColorType
	ORDER BY 
		PCalc.ZoneNumber , ScannedDateTime

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
