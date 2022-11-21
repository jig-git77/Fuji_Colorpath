SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dipak Varma
-- Create date: 2018-05-24
-- Description:	Get Spot Target Details
-- =============================================
/*
EXEC CPSpot.GetSpotTarget 10
*/
ALTER PROCEDURE [CPSpot].[GetSpotTarget] 
@SpotTargetID INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT s.SpotTargetID
		,s.CompanyID
		,s.SpotTargetName
		,s.Description
		,s.IsUsed
		,c.ChartName
		,c.NumOfRows
		,c.NumOfCols
		,c.ChartSizeX
		,c.ChartSizeY
		,c.PatchSizeX
		,c.PatchSizeY
		,c.FirstSample
		,c.ReadDirection
		,c.ReadMode
		,c.Pages
		,c.RowsPerPage
		,c.ChartBorder
		,c.Vgap
		,c.Hgap
		,c.GapLeval
		,c.PreviousL
		,c.ChartRes
	FROM CPSpot.SpotTarget s
	INNER JOIN CPSpot.SpotTargetChartSetting c ON s.SpotTargetID = c.SpotTargetID
	WHERE s.SpotTargetID = @SpotTargetID;
		
	SELECT	[SpotTargetLABID]
			,[SpotTargetID]
			,[SpotID]
			,[SpotName]
			,[Target_L*]
			,[Target_a*]
			,[Target_b*]
			,[HTMLColor]
  FROM [CPSpot].[SpotTargetLAB]
  WHERE [SpotTargetID] = @SpotTargetID ;
	
END


/****** Object:  StoredProcedure [CPSpot].[GetSpotTargets]    Script Date: 6/15/2018 6:10:23 PM ******/
SET ANSI_NULLS ON
