IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwGetPerforamnceSummaySpecsForCompany')
	BEGIN
		DROP  View [CPV].[vwGetPerforamnceSummaySpecsForCompany]
	END
GO

CREATE View [CPV].[vwGetPerforamnceSummaySpecsForCompany] AS
	SELECT 
		CPD.CPDeviceID
		, PMD.CPMeasurementTypeID
		, SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors 
		, CONVERT( DECIMAL(18,2) , (CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(18,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(18,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) ) AS ErrorRate 
		, CONVERT( DECIMAL(18,2) , ISNULL(AVG(ACT.AVERAGE) , 0) ) AS AVG_DE  
		, CONVERT( DECIMAL(18,2) , ISNULL(MAX(ACT.PatchdEValue) , 0) ) AS MAX_DE 
		, ISNULL(AVG(ACT.Dens_AV_Neg), 0) AS AVG_Delta_NEG
		, ISNULL(AVG(ACT.Dens_AV_POS),0) AS AVG_Delta_POS
		, ISNULL(MIN(ACT.Dens_Max_Neg_Val) , 0) AS MAX_Delta_NEG
		, ISNULL(MAX(ACT.Dens_Max_Pos_Val) , 0) AS MAX_Delta_POS
	FROM CPDevice CPD
	LEFT OUTER JOIN CPMeasurement PMD ON PMD.CPDeviceID = CPD.CPDeviceID 
	LEFT OUTER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID 
	GROUP BY
		CPD.CPDeviceID , PMD.CPMeasurementTypeID
