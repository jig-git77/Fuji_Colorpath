IF EXISTS (SELECT * FROM sysobjects WHERE type = 'V' AND name = 'vwAlldePaperAvgMaxPrimary')
	BEGIN
		DROP  View [CPV].[vwAlldePaperAvgMaxPrimary]
	END
GO
/*
	Author :- Maulik Dhorajia
	Created Date :- Aug 6 2008
	Description :- The view for all dE Paper, de Primary , dE Max & dE Avg
*/
CREATE View [CPV].[vwAlldePaperAvgMaxPrimary] AS
SELECT
			CPMeasurement.CPMeasurementGUID
			, ISNULL( CONVERT( DECIMAL(18,2) , Paper ), 0 ) AS dePaperWhite
			, ISNULL( CONVERT( DECIMAL(18,2) , Average ), 0 ) AS deAverage
			, ISNULL( CONVERT( DECIMAL(18,2) , PatchdEValue ), 0 ) AS deMaximum
			, ISNULL( CONVERT( DECIMAL(18,2) , PatchPrimarydEValue ), 0 ) AS dePrimary
			, CreatedDT
		FROM
			CPMeasurement_ACT
		INNER JOIN CPMeasurement ON CPMeasurement.CPMeasurementGUID = CPMeasurement_ACT.CPMeasurementGUID