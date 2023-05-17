
--Updated TotalRows in NumOfRows column in both tables------

UPDATE [CPSpot].[SpotTargetChartSetting]
   SET [NumOfRows] = [NumOfRows] * [Pages]
      
GO



UPDATE [CPSpot].[SpotProjectChartSetting]
   SET [NumOfRows] = [NumOfRows] * [Pages]
GO

