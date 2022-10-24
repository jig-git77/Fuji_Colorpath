ALTER TABLE CPSpot.SpotTargetChartSetting 
	Add IsCustom BIT ;
GO

UPDATE CPSpot.SpotTargetChartSetting
	SET IsCustom = 0;
GO