IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'IsSync1DMultiColor') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsSync1DMultiColor TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Validation_Sync1D_Multi_Color') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Validation_Sync1D_Multi_Color TINYINT NULL;
END

