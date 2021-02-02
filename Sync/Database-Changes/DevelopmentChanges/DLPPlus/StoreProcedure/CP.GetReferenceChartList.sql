/****** Object:  StoredProcedure [CP].[GetReferenceChartList]    Script Date: 12/30/2020 5:38:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CP].[GetReferenceChartList]
 @ReferenceChartTypeID INT

AS    
BEGIN 

IF ISNULL(@ReferenceChartTypeID,0) = 0 
BEGIN  
	SELECT 
		RCT.ReferenceChartTypeID,
		RCT.ChartName,
		RCT.NumOfRows,
		RCT.NumOfCols,
		(CAST (RCT.NumOfRows As Decimal(9,0)) * RCT.NumOfCols) As TotalPatches,
		RCT.InstrumentTypeID,
		IST.InstrumentType AS InstrumentName,		
		RCT.IsUsed,		
		RCT.ChartPreview,		
		RCT.OriginalChart,
		RCT.IsSync1D_ISO,
		RCT.IsSync1D_G7,
		RCT.Validation_1D,
		RCT.IsSync4D,
		RCT.Validation_4D,
		RCT.IsSync4D_Plus,
		RCT.Validation_4DPlus,
		NULL AS ChartSetting

	FROM
		CP.ReferenceChartType  AS RCT WITH (NOLOCK) INNER JOIN
		CP.InstrumentType  AS  IST  WITH (NOLOCK) ON IST.InstrumentTypeID = RCT.InstrumentTypeID 
END  
 ELSE  
 BEGIN  
	SELECT 
		RCT.ReferenceChartTypeID,
		RCT.ChartName,
		RCT.NumOfRows,
		RCT.NumOfCols,
		(CAST (RCT.NumOfRows As Decimal(9,0)) * RCT.NumOfCols) As TotalPatches,
		RCT.InstrumentTypeID,
		IST.InstrumentType AS InstrumentName,	
		RCT.IsUsed,		
		RCT.ChartPreview,
		RCT.OriginalChart,
		RCT.IsSync1D_ISO,
		RCT.IsSync1D_G7,
		RCT.Validation_1D,
		RCT.IsSync4D,
		RCT.Validation_4D,
		RCT.IsSync4D_Plus,
		RCT.Validation_4DPlus,
		(SELECT ChartSetting FROM CP.ChartSetting WHERE ReferenceChartTypeId = RCT.ReferenceChartTypeID) AS ChartSetting
	FROM
		CP.ReferenceChartType AS RCT INNER JOIN
		CP.InstrumentType AS IST ON IST.InstrumentTypeID = RCT.InstrumentTypeID 
	WHERE RCT.ReferenceChartTypeID = @ReferenceChartTypeID
END
END




GO


