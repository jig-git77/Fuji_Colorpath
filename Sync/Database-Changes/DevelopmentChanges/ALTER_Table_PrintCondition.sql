IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'IsImportProfilerOption'
          AND OBJECT_ID=OBJECT_ID('CPSync4D.PrintCondition'))
BEGIN
    -- Column Exists
	ALTER TABLE CPSync4D.PrintCondition ADD IsImportProfilerOption BIT NULL;
END
