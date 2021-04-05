IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'IsSupportMulticolor') 
BEGIN
	ALTER TABLE CP.Device
	ADD IsSupportMulticolor BIT NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'TotalNoColor') 
BEGIN
	ALTER TABLE CP.Device
	ADD TotalNoColor TINYINT NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'Channel_5') 
BEGIN
	ALTER TABLE CP.Device
	ADD Channel_5 tinyint NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'Channel_6') 
BEGIN
	ALTER TABLE CP.Device
	ADD Channel_6 tinyint NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'Channel_7') 
BEGIN
	ALTER TABLE CP.Device
	ADD Channel_7 tinyint NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'Channel_8') 
BEGIN
	ALTER TABLE CP.Device
	ADD Channel_8 tinyint NULL;
END
IF NOT EXISTS(SELECT *
          FROM   INFORMATION_SCHEMA.COLUMNS
          WHERE  TABLE_NAME = 'CP.Device'
                 AND COLUMN_NAME = 'Channel_9') 
BEGIN
	ALTER TABLE CP.Device
	ADD Channel_9 tinyint NULL;
END
