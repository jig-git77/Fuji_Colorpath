IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'IsDLPMultiColor') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsDLPMultiColor TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Validation_4D_Mutli_Color') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Validation_4D_Mutli_Color TINYINT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'IsSupportMulticolor') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsSupportMulticolor BIT NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Channel_5') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Channel_5 tinyint NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Channel_6') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Channel_6 tinyint NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Channel_7') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Channel_7 tinyint NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Channel_8') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Channel_8 tinyint NULL;
END
GO
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Channel_9') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Channel_9 tinyint NULL;
END
