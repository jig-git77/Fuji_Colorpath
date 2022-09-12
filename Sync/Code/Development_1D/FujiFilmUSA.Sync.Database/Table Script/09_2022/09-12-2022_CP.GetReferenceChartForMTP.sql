CREATE PROCEDURE [CP].[GetReferenceChartForMTP] 
AS
BEGIN
	SELECT		[ReferenceChartTypeID]
				,[ChartName] + ' (' + [InstrumentType].[InstrumentType] + ')' as ChartName
			
	FROM		[CP].[ReferenceChartType]
	INNER JOIN	[CP].[InstrumentType] on [ReferenceChartType].[InstrumentTypeID] = [InstrumentType].[InstrumentTypeID]
	WHERE		[ReferenceChartType].[IsUsed] = 1
	ORDER BY	[ChartName]
		
END
