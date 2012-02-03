IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllGrayPatches')
	BEGIN
		DROP  View [CPV].[vwAllGrayPatches]
	END
GO

CREATE View [CPV].[vwAllGrayPatches] AS

	SELECT
		MeasurementGUID
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(DE) ), 0 ) as AVE_DE
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(ABS(DH)) ), 0 ) as AVE_DH
		, ISNULL( CONVERT( DECIMAL(18,2) , MAX(Density) ), 0 ) as Density
		, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
	FROM 
		CPPatchDataLab
	WHERE			
		CMYK_K = 0 AND
		((CMYK_C =  3.14  AND CMYK_M =  2.21 AND CMYK_Y = 2.21) OR (CMYK_C = 10.2  AND CMYK_M =  7.41 AND CMYK_Y = 7.41) OR (CMYK_C = 25  AND CMYK_M =  19 AND CMYK_Y = 19) OR (CMYK_C = 50  AND CMYK_M =  40 AND CMYK_Y = 40) OR (CMYK_C = 75  AND CMYK_M =  66 AND CMYK_Y = 66))
		
		/*
		Old Criteria
		((CMYK_C = 50 AND CMYK_M = 39 AND CMYK_Y = 39 )
		OR (CMYK_C = 50 AND CMYK_M = 40 AND CMYK_Y = 40 )
		OR (CMYK_C = 50 AND CMYK_M = 50 AND CMYK_Y = 50 ))
		*/
	GROUP BY
		MeasurementGUID


