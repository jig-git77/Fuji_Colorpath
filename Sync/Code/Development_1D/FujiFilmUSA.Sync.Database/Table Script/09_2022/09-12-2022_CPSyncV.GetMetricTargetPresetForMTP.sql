CREATE PROCEDURE [CPSyncV].[GetMetricTargetPresetForMTP] 
@ReferenceChartTypeID INT
AS
BEGIN
	SELECT		[MetricTargetPreset].[MetricTargetPresetID]
				,[MetricTargetPreset].[MetricTargetPresetName]
	FROM		[CPSyncV].[MetricTargetPreset],
				[CP].[ReferenceChartType]
	WHERE		[MetricTargetPreset].[IsUsed] = 1
	AND			( @ReferenceChartTypeID IS NULL OR [ReferenceChartType].ReferenceChartTypeID = @ReferenceChartTypeID)
	ORDER BY	[MetricTargetPreset].[MetricTargetPresetName]

				--FOR FUTURE PURPOSE
  /*AND			(ISNULL ([ReferenceChartType].IsSync1D_G7 ,0) = ISNULL ([MetricTargetPreset].IsSync1D,0)
	OR 			ISNULL ([ReferenceChartType].IsSync1D_ISO,0) = ISNULL ([MetricTargetPreset].IsSync1D,0))
	AND			( ISNULL([ReferenceChartType].Validation_1D ,0 ) = isnull([MetricTargetPreset].Validation_1D,0))
	AND			( ISNULL([ReferenceChartType].Validation_4D ,0 ) = isnull([MetricTargetPreset].Validation_4D,0))*/
			
END
