IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobZoneSummary')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobZoneSummary]
	END
GO
/*
	AUTHOR : Maulik Dhorajia
	Date : Aug/27/2008
	Description : Retrieves the information of Zone Summary for DeviceType Press.	
	
	Updated By : Maulik Dhorajia
	Updated Date : Sept/9/2008
	Description : Added "AND Press.Sheets.IsReady = 1 AND Press.Sheets.SheetState = 2".
*/
CREATE Procedure [Press].[uspGetPressJobZoneSummary]( @JobID INT , @DongleID VARCHAR(10) )AS
BEGIN
	BEGIN TRY
		
/*
	SELECT 
			Press.PatchCalculations.ZoneNumber
			, ISNULL( CONVERT( DECIMAL(18,2) , AVG(deltaE) ) , 0 ) AS AvgDeltaE
			, ISNULL( CONVERT( DECIMAL(18,2) , MAX(deltaE) ) , 0 ) AS MaxDeltaE
			, ISNULL( CONVERT( DECIMAL(18,2) , STDEV(deltaE) ) , 0 ) AS StdDEVdeltaE
		FROM Press.PatchCalculations 
		INNER JOIN Press.Sheets ON Press.PatchCalculations.SheetID = Press.Sheets.SheetID 
		WHERE Press.Sheets.JobID = @JobID 
			AND Press.Sheets.DongleID = @DongleID AND Press.PatchCalculations.DongleID = @DongleID
			AND Press.Sheets.IsReady = 1 AND Press.Sheets.SheetState = 2
		GROUP BY Press.PatchCalculations.ZoneNumber
		ORDER BY Press.PatchCalculations.ZoneNumber		
*/

		SELECT 
		PCalc.ZoneNumber	
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(PCalc.deltaE) ) , 0 ) AS AvgDeltaE
		, COALESCE( (CASE _ColorID WHEN 1 THEN 'Red' WHEN 2 THEN 'Yellow' ELSE 'Green' END) , 'Green' ) AS Color
	FROM 
		Press.PatchCalculations AS PCalc 
	
	INNER JOIN Press.Sheets ON PCalc.SheetID = Sheets.SheetID 
		AND Sheets.JobID = @JobID AND Sheets.DongleID = @DongleID 
		AND Sheets.IsReady = 1 AND Sheets.SheetState = 2
	
	LEFT OUTER JOIN
	(
		SELECT
			PCalc.ZoneNumber
		,	MIN(
				CASE  
					WHEN PCalc.DeltaE > CTT.DetlaE_L THEN 1		-- Red
					WHEN PCalc.DeltaE > CTT.DeltaE_C THEN 2		-- Yellow
					ELSE 3 -- Green
				END
			) AS _ColorID
		FROM 
			Press.PatchCalculations AS PCalc			

		INNER JOIN Press.Sheets ON PCalc.SheetID = Sheets.SheetID 
			AND Sheets.JobID = @JobID AND Sheets.DongleID = @DongleID 
			AND Sheets.IsReady = 1 AND Sheets.SheetState = 2

		INNER JOIN Press.ColorTarTol AS CTT ON CTT.ColorID = PCalc.ColorID
			AND CTT.JobID = @JobID AND CTT.DongleID = @DongleID 

		WHERE  
			PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
		AND 
		(
			CASE
				WHEN PCalc.DeltaE > CTT.DetlaE_L THEN 1  
				WHEN PCalc.DeltaE > CTT.DeltaE_C THEN 2
				ELSE 3
			END
		) < 3
		GROUP BY 
			PCalc.ZoneNumber

	) AS VW1 ON VW1.ZoneNumber = PCalc.ZoneNumber	
	WHERE 
		 PCalc.JobID = @JobID AND PCalc.DongleID = @DongleID
	GROUP BY PCalc.ZoneNumber , _ColorID
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
