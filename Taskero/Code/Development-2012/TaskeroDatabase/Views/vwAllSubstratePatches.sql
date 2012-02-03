IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllSubstratePatches')
	BEGIN
		DROP  View [CPV].[vwAllSubstratePatches]
	END
GO
/*
	Author :- Maulik Dhorajia
	Created Date :- Aug 6 2008
	Description :- The view for all Substrate patches(Paper Patches)
*/
CREATE View [CPV].[vwAllSubstratePatches] AS

	SELECT
		MeasurementGUID
		, ISNULL( AVG(DE) , 0 ) as AVE_DE
		, ISNULL( AVG(ABS(DH)) , 0 ) as AVE_DH
		, ISNULL( MAX(Density) , 0 ) as Density
		, ISNULL( MAX(CREATED_TIME) , 0 ) as Created_Time
	FROM 
		CPPatchDataLab
	WHERE			
		(CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0)  
	GROUP BY
		MeasurementGUID


