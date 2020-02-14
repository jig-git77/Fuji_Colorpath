IF COL_LENGTH('CPSpot.PrintCondition', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CPSpot.PrintCondition
    ADD IsCustomPlugin BIT NULL
END
