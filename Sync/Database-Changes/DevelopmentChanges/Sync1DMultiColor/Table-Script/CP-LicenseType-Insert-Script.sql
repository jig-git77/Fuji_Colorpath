IF NOT EXISTS(SELECT * FROM CP.LicenseType WHERE LicenseType='Sync1D MultiColor')
BEGIN
INSERT INTO CP.LicenseType(LicenseType)
VALUES('Sync1D MultiColor')
END