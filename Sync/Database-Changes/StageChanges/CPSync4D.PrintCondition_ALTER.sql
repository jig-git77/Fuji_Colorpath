IF COL_LENGTH('CPSync4D.PrintCondition', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CPSync4D.PrintCondition
    ADD IsCustomPlugin BIT NULL
END
