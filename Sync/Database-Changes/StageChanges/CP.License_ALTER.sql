IF COL_LENGTH('CP.License', 'IsCustomPlugin') IS NULL
BEGIN
    ALTER TABLE CP.License 
    ADD IsCustomPlugin BIT NULL
END
