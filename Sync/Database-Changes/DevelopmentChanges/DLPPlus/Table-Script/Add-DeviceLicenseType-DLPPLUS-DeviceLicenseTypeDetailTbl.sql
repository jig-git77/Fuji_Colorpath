---ADD DeviceLicenseType DLPPLUS where DLP License exists Number of projects
INSERT INTO CP.DeviceLicenseType(DeviceID,LicenseTypeID,NumOfProject)
SELECT DeviceID,11,NumOfProject FROM CP.DeviceLicenseType WHERE LicenseTypeID=2 AND DeviceID NOT IN(SELECT LicenseID FROM CP.LicenseDetail WHERE LicenseTypeID=11)
ORDER BY DeviceID ASC
