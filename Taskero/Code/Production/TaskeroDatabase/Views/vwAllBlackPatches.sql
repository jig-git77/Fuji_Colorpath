IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllBlackPatches')
	BEGIN
		DROP  View [CPV].[vwAllBlackPatches]
	END
GO
/*
	Author :- Maulik Dhorajia
	Created Date :- Aug 6 2008
	Description :- The view for all Black patches
*/
CREATE View [CPV].[vwAllBlackPatches] AS

	SELECT
		MeasurementGUID
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(DE) ), 0 ) as AVE_DE
		, ISNULL( CONVERT( DECIMAL(18,2) , AVG(ABS(DH))) , 0 ) as AVE_DH
		, ISNULL( CONVERT( DECIMAL(18,2) , MAX(Density) ), 0 ) as Density
		, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
	FROM 
		CPPatchDataLab
	WHERE			
		(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100)  
	GROUP BY
		MeasurementGUID


