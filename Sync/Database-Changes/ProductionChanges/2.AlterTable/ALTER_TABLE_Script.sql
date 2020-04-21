IF COL_LENGTH('CP.License', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CP.License 
    ADD IsCustomPlugin BIT NULL
END
GO
IF COL_LENGTH('CPSpot.PrintCondition', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CPSpot.PrintCondition
    ADD IsCustomPlugin BIT NULL
END
GO
IF COL_LENGTH('CPSync4D.PrintCondition', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CPSync4D.PrintCondition
    ADD IsCustomPlugin BIT NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSync4D.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync4D.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSync.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSpot.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSpot.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'MeasurementInfo'
          AND OBJECT_ID=OBJECT_ID('CPSyncV.Measurement'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSyncV.Measurement ADD MeasurementInfo NVARCHAR(500) NULL;
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PrintStandardTypeID'
          AND OBJECT_ID=OBJECT_ID('CPSync.PrintStandardDensity'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync.PrintStandardDensity ALTER COLUMN PrintStandardTypeID INT NOT NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PrintStandardTypeID'
          AND OBJECT_ID=OBJECT_ID('CPSync.PrintStandardLAB'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync.PrintStandardLAB ALTER COLUMN PrintStandardTypeID INT NOT NULL
END
GO
IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PrintStandardTypeID'
          AND OBJECT_ID=OBJECT_ID('CPSync.PrintStandardTVI'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync.PrintStandardTVI ALTER COLUMN PrintStandardTypeID INT NOT NULL
END
---Below column which may be not part of MTooL.
IF COL_LENGTH('CPSpot.MeasurementSetting', 'PluginType') IS NULL
BEGIN
    ALTER TABLE CPSpot.MeasurementSetting 
    ADD PluginType BIT NULL
END
GO
IF COL_LENGTH('CPSync4D.MeasurementSetting', 'PluginType') IS NULL
BEGIN
    ALTER TABLE CPSync4D.MeasurementSetting 
    ADD PluginType BIT NULL
END
GO
IF COL_LENGTH('CPSyncV.MeasurementSetting', 'PluginType') IS NULL
BEGIN
    ALTER TABLE CPSyncV.MeasurementSetting 
    ADD PluginType BIT NULL
END