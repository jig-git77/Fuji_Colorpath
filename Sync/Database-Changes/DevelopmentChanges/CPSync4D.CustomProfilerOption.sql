USE [ColorPathProd_Stage1]
GO

/****** Object:  Table [CPSync4D].[CustomProfilerOption]    Script Date: 1/13/2020 1:34:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CPSync4D].[CustomProfilerOption](
	[CustomProfilerOptionID] [int] IDENTITY(1,1) NOT NULL,
	[ProfilerOptionsName] [nvarchar](50) NOT NULL,
	[ProfilerOptionsDesc] [nvarchar](300) NOT NULL,
	[ProjectTypeID] [tinyint] NOT NULL,
	[IsCustomOption] [bit] NOT NULL,
 CONSTRAINT [PK_CustomProfilerOptionID] PRIMARY KEY CLUSTERED 
(
	[CustomProfilerOptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CPSync4D].[CustomProfilerOption] ADD  DEFAULT ((0)) FOR [IsCustomOption]
GO

ALTER TABLE [CPSync4D].[CustomProfilerOption]  WITH CHECK ADD  CONSTRAINT [FK_CustomProfilerOption_ProjectTypeID] FOREIGN KEY([ProjectTypeID])
REFERENCES [CP].[ProjectType] ([ProjectTypeID])
GO

ALTER TABLE [CPSync4D].[CustomProfilerOption] CHECK CONSTRAINT [FK_CustomProfilerOption_ProjectTypeID]
GO


