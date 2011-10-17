IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressJobZoneSummarySheet')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressJobZoneSummarySheet]
	END

GO

CREATE Procedure [Press].[uspGetPressJobZoneSummarySheet]( @JobID INT , @DongleID VARCHAR(10) , @SheetNumber INT )  AS
BEGIN

/*---------- Gets the information for the chart ----------*/
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
			AND Sheets.SheetNumber = @SheetNumber

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
		 AND Sheets.SheetNumber = @SheetNumber
	GROUP BY PCalc.ZoneNumber , _ColorID
	ORDER BY PCalc.ZoneNumber	

END
