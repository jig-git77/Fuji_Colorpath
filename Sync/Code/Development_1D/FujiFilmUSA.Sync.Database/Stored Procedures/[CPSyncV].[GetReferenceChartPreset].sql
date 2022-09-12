CREATE PROCEDURE [CPSyncV].[GetReferenceChartPreset] 
@ReferenceChartTypeID INT
AS
BEGIN

	SELECT		[ReferenceChartPresetID]
				,[ReferenceChartTypeID]
				,[MetricTargetPresetID]
	FROM		[CPSyncV].[ReferenceChartPreset]
	WHERE		(@ReferenceChartTypeID IS NULL OR ReferenceChartTypeID = @ReferenceChartTypeID)
 
END