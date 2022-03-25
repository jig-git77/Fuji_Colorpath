GO
CREATE TABLE [CPSync4DPlus].[OptimizeRun_PreviousRunData](
	[PreviousRunID] [int] IDENTITY(1,1) NOT NULL,
	[OptimizeRunID] [int] NOT NULL,
	[PatchTypeID] [int] NULL,
	[AvgDensity] [decimal](6, 2) NULL,
	[MinDensity] [decimal](6, 2) NULL,
	[MaxDensity] [decimal](6, 2) NULL,
	[STDdevDensity] [decimal](6, 2) NULL,
	[AvgΔE] [decimal](6, 2) NULL,
	[MinΔE] [decimal](6, 2) NULL,
	[MaxΔE] [decimal](6, 2) NULL,
	[STDdevΔE] [decimal](6, 2) NULL,
	[IsUsed] [bit] NULL,
 CONSTRAINT [PK_OptimizeRun_PreviousRunData_PreviousRunID] PRIMARY KEY CLUSTERED 
(
	[PreviousRunID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CPSync4DPlus].[OptimizeRun_PreviousRunData]  WITH CHECK ADD  CONSTRAINT [FK_OptimizeRun_PreviousRunData_OptimizeRunID] FOREIGN KEY([OptimizeRunID])
REFERENCES [CPSync4D].[OptimizeRun] ([OptimizeRunID])
ON DELETE CASCADE
GO

ALTER TABLE [CPSync4DPlus].[OptimizeRun_PreviousRunData] CHECK CONSTRAINT [FK_OptimizeRun_PreviousRunData_OptimizeRunID]
GO


