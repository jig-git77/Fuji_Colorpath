IF NOT EXISTS(SELECT 1 FROM CP.Permission WHERE PermissionID=22 AND PermissionDesc='ManageMToolVersion')
BEGIN
	INSERT INTO CP.Permission(PermissionID,PermissionDesc,Notes) VALUES(22,'Manage MTool Version','Permission to Add, Update and Delete MTool Version')
END


