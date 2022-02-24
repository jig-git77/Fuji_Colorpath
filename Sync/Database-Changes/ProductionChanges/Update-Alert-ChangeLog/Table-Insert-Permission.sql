IF NOT EXISTS(SELECT * FROM CP.Permission WHERE PermissionDesc='Manage UpdateChangeLog')
BEGIN
 INSERT INTO CP.Permission(PermissionID,PermissionDesc,Notes) VALUES(23,'Manage UpdateChangeLog','Permission to Add, Update and Delete Update Change Log')
END