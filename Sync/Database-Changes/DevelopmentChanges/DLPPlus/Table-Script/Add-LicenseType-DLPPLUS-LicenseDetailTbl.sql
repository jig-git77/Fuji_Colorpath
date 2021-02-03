---ADD License Type DLPPLUS where DLP License exists
INSERT INTO CP.LicenseDetail(LicenseID,LicenseTypeID)
SELECT LicenseID,11 FROM CP.LicenseDetail WHERE LicenseTypeID=2 AND LicenseID NOT IN(SELECT LicenseID FROM CP.LicenseDetail WHERE LicenseTypeID=11)
ORDER BY LicenseID ASC
