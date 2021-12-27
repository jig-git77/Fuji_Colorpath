------------------------------------------------------------------CP Device Table Add Two New Field------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CP' AND TABLE_NAME='Device' AND COLUMN_NAME='IsRevoriaType')
BEGIN
	
	ALTER TABLE CP.Device ADD IsRevoriaType BIT NULL DEFAULT 0;
END
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CP' AND TABLE_NAME='Device' AND COLUMN_NAME='DeviceKey')
BEGIN
	
	ALTER TABLE CP.Device ADD DeviceKey NVARCHAR(50) NULL DEFAULT NULL;
END
GO
------------------------------------------------------------------CPSync4D Project Table Add Two New Field------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CPSync4D' AND TABLE_NAME='Project' AND COLUMN_NAME='IsRevoriaType')
BEGIN
	
	ALTER TABLE CPSync4D.Project ADD IsRevoriaType BIT NULL DEFAULT 0;
END
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CPSync4D' AND TABLE_NAME='Project' AND COLUMN_NAME='PrintQId')
BEGIN
	
	ALTER TABLE CPSync4D.Project ADD PrintQId NVARCHAR(MAX) NULL DEFAULT NULL;
END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------