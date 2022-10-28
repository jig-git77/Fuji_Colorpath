INSERT INTO	[CP].[Permission]
(
	PermissionID
	,PermissionDesc
	,Notes
)
VALUES
(
	25
	,'ManageCustomBCOTarget'
	,'Permission to Add, Update and Delete Custom BCO Target'
)
GO

INSERT INTO [CP].[RolePermission]
(
		RoleID
		,PermissionID
)
VALUES
(
	2
	,25
)
GO

INSERT INTO [CP].[RolePermission]
(
		RoleID
		,PermissionID
)
VALUES
(
	3
	,25
)
GO

INSERT INTO [CP].[RolePermission]
(
		RoleID
		,PermissionID
)
VALUES
(
	4
	,25
)
GO

INSERT INTO [CP].[RolePermission]
(
		RoleID
		,PermissionID
)
VALUES
(
	5
	,25
)
GO