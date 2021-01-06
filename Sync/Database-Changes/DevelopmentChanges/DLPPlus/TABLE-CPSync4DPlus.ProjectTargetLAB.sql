USE [ColorPathSync_Prod]
GO

/****** Object:  Table [CPSync].[ProjectTargetLAB]    Script Date: 12/28/2020 4:36:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE SCHEMA CPSync4DPlus;
GO
CREATE TABLE [CPSync4DPlus].[ProjectTargetLAB](
	[ProjectTargetID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[PatchTypeID] [tinyint] NOT NULL,
	[TargetL*] [decimal](6, 2) NULL,
	[Targeta*] [decimal](6, 2) NULL,
	[Targetb*] [decimal](6, 2) NULL,
 CONSTRAINT [PK_ProjectTargetLAB] PRIMARY KEY CLUSTERED 
(
	[ProjectTargetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CPSync4DPlus].[ProjectTargetLAB]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTargetLAB_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [CPSync4D].[Project] ([ProjectID])
ON DELETE CASCADE
GO

ALTER TABLE [CPSync4DPlus].[ProjectTargetLAB] CHECK CONSTRAINT [FK_ProjectTargetLAB_ProjectID]
GO


