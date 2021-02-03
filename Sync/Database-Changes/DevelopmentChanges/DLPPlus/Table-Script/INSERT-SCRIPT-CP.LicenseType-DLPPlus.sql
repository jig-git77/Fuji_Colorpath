IF NOT EXISTS(SELECT * FROM CP.LicenseType WHERE  LicenseType='DLP Plus')
BEGIN
	INSERT INTO CP.LicenseType(LicenseType) VALUES('DLP Plus')
END