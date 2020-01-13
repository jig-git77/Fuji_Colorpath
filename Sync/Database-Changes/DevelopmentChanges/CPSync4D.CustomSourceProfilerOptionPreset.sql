USE [ColorPathProd_Stage1]
GO

/****** Object:  Table [CPSync4D].[CustomSourceProfilerOptionPreset]    Script Date: 1/13/2020 1:36:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CPSync4D].[CustomSourceProfilerOptionPreset](
	[CustomSourceProfilerOptionPresetID] [int] IDENTITY(1,1) NOT NULL,
	[CustomSourceProfilerOptionID] [int] NOT NULL,
	[ProfileTypeID] [tinyint] NOT NULL,
	[ProfilerOptionID] [tinyint] NULL,
	[ProfilerOptionValue] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CustomSourceProfilerOptionPreset] PRIMARY KEY CLUSTERED 
(
	[CustomSourceProfilerOptionPresetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


