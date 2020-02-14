IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSync4D.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync4D.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSync.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSpot.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSpot.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSyncV.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSyncV.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END