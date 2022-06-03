IF NOT EXISTS (SELECT * FROM CP.LicenseType WHERE LicenseType='Custom BCO')
BEGIN
	INSERT INTO CP.LicenseType(LicenseType) VALUES ('Custom BCO')
END