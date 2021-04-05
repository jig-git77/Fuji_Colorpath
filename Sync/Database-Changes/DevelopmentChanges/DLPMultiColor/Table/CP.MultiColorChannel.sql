

/****** Object:  Table [CP].[MultiColorChannel]    Script Date: 4/5/2021 5:04:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CP].[MultiColorChannel](
	[MultiColorChannelID] [int] IDENTITY(1,1) NOT NULL,
	[MultiColorChannelName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_CP.MultiColorChannel] PRIMARY KEY CLUSTERED 
(
	[MultiColorChannelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


