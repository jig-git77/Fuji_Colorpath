IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'ReferenceChartType'
                 AND TABLE_SCHEMA='CP'
                 AND COLUMN_NAME = 'IsSync1DMultiColor_G7') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsSync1DMultiColor_G7 TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'ReferenceChartType'
                 AND TABLE_SCHEMA='CP'
                 AND COLUMN_NAME = 'IsSync1DMultiColor_ISO') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsSync1DMultiColor_ISO TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'ReferenceChartType'
                 AND TABLE_SCHEMA='CP'
                 AND COLUMN_NAME = 'Validation_Sync1D_Multi_Color') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Validation_Sync1D_Multi_Color TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT * FROM CP.LicenseType WHERE LicenseType='Sync1D MultiColor')
BEGIN
	INSERT INTO CP.LicenseType(LicenseType) VALUES('Sync1D MultiColor')
END
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME='CPSyncMultiColor')
BEGIN
	EXEC('CREATE SCHEMA CPSyncMultiColor;');
END

