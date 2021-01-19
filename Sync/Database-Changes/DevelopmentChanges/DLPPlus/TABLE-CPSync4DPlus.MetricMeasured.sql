
GO

/****** Object:  Table [CPSync].[MetricMeasured]    Script Date: 1/11/2021 8:52:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [CPSync4DPlus].[MetricMeasured](
	[MetricMeasuredID] [int] IDENTITY(1,1) NOT NULL,
	[MeasurementID] [int] NOT NULL,
	[MetricSumID] [smallint] NOT NULL,
	[TargetValue] [decimal](6, 2) NOT NULL,
	[MeasuredValue] [decimal](6, 2) NOT NULL,
	[IsNotOK] [bit] NOT NULL,
 CONSTRAINT [PK_MetricMeasured_MetricMeasuredID] PRIMARY KEY CLUSTERED 
(
	[MetricMeasuredID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [CPSync4DPlus].[MetricMeasured]  WITH CHECK ADD  CONSTRAINT [FK_SyncMetricMeasured_Measurement_MeasurementID] FOREIGN KEY([MeasurementID])
REFERENCES [CPSync4D].[Measurement] ([MeasurementID])
ON DELETE CASCADE
GO

ALTER TABLE [CPSync4DPlus].[MetricMeasured] CHECK CONSTRAINT [FK_SyncMetricMeasured_Measurement_MeasurementID]
GO

ALTER TABLE [CPSync4DPlus].[MetricMeasured]  WITH NOCHECK ADD  CONSTRAINT [FK_SyncMetricMeasured_MetricSum_MetricSumID] FOREIGN KEY([MetricSumID])
REFERENCES [CPSyncV].[MetricSum] ([MetricSumID])
GO

ALTER TABLE [CPSync4DPlus].[MetricMeasured] CHECK CONSTRAINT [FK_SyncMetricMeasured_MetricSum_MetricSumID]
GO


