IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'IsSync4D_Plus') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD IsSync4D_Plus TINYINT NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.ReferenceChartType'
                 AND COLUMN_NAME = 'Validation_4DPlus') 
BEGIN
	ALTER TABLE CP.ReferenceChartType
	ADD Validation_4DPlus TINYINT NULL;
END