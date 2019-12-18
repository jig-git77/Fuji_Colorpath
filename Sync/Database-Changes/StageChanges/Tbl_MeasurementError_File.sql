
GO
/****** Object:  Table [dbo].[MeasurementError_File]    Script Date: 12/17/2019 5:54:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementError_File](
 [MeasurementErrorFileID] [int] IDENTITY(1,1) NOT NULL, 
 [OptimizationRunID] [int] NOT NULL,
 [ProjectID] [int] NOT NULL,
 [MeasurementFile] [nvarchar](max) NOT NULL,
 [CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MeasurementError_File] PRIMARY KEY CLUSTERED
(
 [MeasurementErrorFileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO