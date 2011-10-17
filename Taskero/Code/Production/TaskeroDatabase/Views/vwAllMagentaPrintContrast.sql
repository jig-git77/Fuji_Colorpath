IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAllMagentaPrintContrast')
	BEGIN
		DROP  View [CPV].[vwAllMagentaPrintContrast]
	END
GO

/*
	Author :- Maulik Dhorajia
	Created Date :- Aug 6 2008
	Description :- The view for all Magenta print contrast settings
*/
CREATE View [CPV].[vwAllMagentaPrintContrast] AS

	SELECT     
		 A.MeasurementGUID  
		, A.Created_Time  
		, ISNULL(A.Density,0) AS D100  
		, ISNULL(B.Density,0) AS D75  
		, Print_Contrast_K AS Print_Contrast  
		, ISNULL(Print_Contrast_TH, 0 ) AS Print_Contrast_TH  
	FROM  
	CPPatchDataLab A  
	LEFT OUTER JOIN CPPatchDataLab B ON B.MeasurementGUID = A.MeasurementGUID   
	INNER JOIN CPMeasurement_PT_Prop ON CPMeasurementGUID  = A.MeasurementGUID   
	WHERE (A.CMYK_C = 0 AND A.CMYK_M = 100 AND A.CMYK_Y = 0 AND A.CMYK_K = 0)  
	AND (B.CMYK_C = 0 AND B.CMYK_M = 75 AND B.CMYK_Y = 0 AND B.CMYK_K = 0)  