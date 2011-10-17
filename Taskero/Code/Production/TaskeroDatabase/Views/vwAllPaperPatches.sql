IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllPaperPatches')
	BEGIN
		DROP  View [CPV].[vwAllPaperPatches]
	END
GO

CREATE View [CPV].[vwAllPaperPatches] AS

	SELECT
		MeasurementGUID
		, ISNULL( AVG(DE) , 0 ) as AVE_DE
		, ISNULL( AVG(ABS(DE)) , 0 ) as AVE_DE
		, ISNULL( MAX(Density) , 0 ) as Density
		, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
	FROM 
		CPPatchDataLab
	WHERE			
		(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0)
	GROUP BY
		MeasurementGUID


