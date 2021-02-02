IF NOT EXISTS(SELECT * FROM CP.LicenseType WHERE LicenseTypeID=11 AND LicenseType='DLP Plus')
BEGIN
	INSERT INTO CP.LicenseType(LicenseTypeID,LicenseType) VALUES(11,'DLP Plus')
END