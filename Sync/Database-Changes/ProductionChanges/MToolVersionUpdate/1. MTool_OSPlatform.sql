
GO

/****** Object:  Table [CP].[MTool_OSPlatform]    Script Date: 7/13/2020 6:37:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CP].[MTool_OSPlatform](
	[OSPlatformID] [tinyint] IDENTITY(1,1) NOT NULL,
	[OSPlatformName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MTool_OSPlatform_OSPlatformID] PRIMARY KEY CLUSTERED 
(
	[OSPlatformID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO



GO

/****** Object:  Table [CP].[MTool_OSPlatformDetails]    Script Date: 7/13/2020 6:38:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CP].[MTool_OSPlatformDetails](
	[OSPlatformDetailsID] [smallint] IDENTITY(1,1) NOT NULL,
	[OSPlatformID] [tinyint] NOT NULL,
	[LatestVersion] [nvarchar](50) NOT NULL,
	[MinReqVersion] [nvarchar](50) NOT NULL,
	[URLlocation] [nvarchar](200) NOT NULL,
	[UpdateInfo] [nvarchar](500) NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_MTool_OSPlatform_OSPlatformDetailsID] PRIMARY KEY CLUSTERED 
(
	[OSPlatformDetailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [CP].[MTool_OSPlatformDetails] ADD  CONSTRAINT [DF_MTool_OSPlatformDetails_IsUsed]  DEFAULT ((1)) FOR [IsUsed]
GO

ALTER TABLE [CP].[MTool_OSPlatformDetails]  WITH CHECK ADD  CONSTRAINT [FK_MTool_OSPlatformDetails_OSPlatform_OSPlatformID] FOREIGN KEY([OSPlatformID])
REFERENCES [CP].[MTool_OSPlatform] ([OSPlatformID])
ON DELETE CASCADE
GO

ALTER TABLE [CP].[MTool_OSPlatformDetails] CHECK CONSTRAINT [FK_MTool_OSPlatformDetails_OSPlatform_OSPlatformID]
GO


/***************Fill look up data**************/

GO

INSERT INTO [CP].[MTool_OSPlatform]
           ([OSPlatformID]
           ,[OSPlatformName])
     VALUES
           (1
           ,'Mac')
GO

INSERT INTO [CP].[MTool_OSPlatform]
           ([OSPlatformID]
           ,[OSPlatformName])
     VALUES
           (2
           ,'PC')
GO


INSERT INTO [CP].[MTool_OSPlatform]
           ([OSPlatformID]
           ,[OSPlatformName])
     VALUES
           (3
           ,'Mac Test')
GO


INSERT INTO [CP].[MTool_OSPlatformDetails]
           ([OSPlatformDetailsID]
           ,[OSPlatformID]
           ,[LatestVersion]
           ,[MinReqVersion]
           ,[URLlocation]
           ,[UpdateInfo])
     VALUES
           (1
           ,1
           ,'1.15'
           ,'1.14'
           ,'http://stage.colorpathsync.com/Download/MTool/measure_10_14.dmg'
           ,'More Details will come later')
GO


INSERT INTO [CP].[MTool_OSPlatformDetails]
           ([OSPlatformDetailsID]
           ,[OSPlatformID]
           ,[LatestVersion]
           ,[MinReqVersion]
           ,[URLlocation]
           ,[UpdateInfo])
     VALUES
           (1
           ,3
           ,'1.15'
           ,'1.14'
           ,'http://stage.colorpathsync.com/Download/MTool/measure_10_14_d.dmg'
           ,'More Details will come later')
GO


/*********** StoredProcedure [CP].[CheckMToolVersionUpdateStatus] ********/

GO
/****** Object:  StoredProcedure [CP].[CheckMToolVersionUpdateStatus]    Script Date: 7/14/2020 10:12:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[CheckMToolVersionUpdateStatus]
	@OSPlatformID TINYINT
AS
BEGIN
			SELECT
				[OSPlatformID],
				[LatestVersion],
				[MinReqVersion],
				[URLlocation],
				[UpdateInfo]
			FROM
				[CP].[MTool_OSPlatformDetails]
			WHERE
				[OSPlatformID] = @OSPlatformID AND [IsUsed] = 1 
				
			ORDER BY
				[OSPlatformDetailsID]
END



