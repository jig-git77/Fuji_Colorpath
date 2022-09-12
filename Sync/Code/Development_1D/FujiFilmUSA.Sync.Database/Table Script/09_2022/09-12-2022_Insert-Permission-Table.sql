IF NOT EXISTS (SELECT * FROM [CP].[Permission] WHERE PermissionID = 24)
BEGIN
	INSERT INTO [CP].[Permission]
			   ([PermissionID]
			   ,[PermissionDesc]
			   ,[Notes])
	VALUES
			   (24
			   ,'AssignChartWithMTP'
			   ,'Permission to Add, Update and Delete Assign Chart With MTP')
END
