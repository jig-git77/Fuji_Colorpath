IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'IsDLPMultiColor') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsDLPMultiColor TINYINT NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Validation_4D_Mutli_Color') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Validation_4D_Mutli_Color TINYINT NULL;
END