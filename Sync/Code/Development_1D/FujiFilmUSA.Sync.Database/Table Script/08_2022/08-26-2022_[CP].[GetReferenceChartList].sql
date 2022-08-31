
/****** Object:  StoredProcedure [CP].[GetReferenceChartList]    Script Date: 8/31/2022 10:58:59 AM ******/
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
		RCT.IsDLPMultiColor,
		RCT.Validation_4D_Mutli_Color,
		RCT.IsSupportMulticolor,
		ISNULL(RCT.TotalNoColor,0) AS TotalNoColor,
		RCT.Channel_5,
		RCT.Channel_6,
		RCT.Channel_7,
		RCT.Channel_8,
		RCT.Channel_9,
		NULL AS ChartSetting,
		RCT.IsSync1DMultiColor_ISO,
		RCT.IsSync1DMultiColor_G7,
		RCT.Validation_Sync1D_Multi_Color,
		RCT.DeviceFamilyID


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
		RCT.IsDLPMultiColor,
		RCT.Validation_4D_Mutli_Color,
		RCT.IsSupportMulticolor,
		ISNULL(RCT.TotalNoColor,0) AS TotalNoColor,
		RCT.Channel_5,
		RCT.Channel_6,
		RCT.Channel_7,
		RCT.Channel_8,
		RCT.Channel_9,		
		(SELECT ChartSetting FROM CP.ChartSetting WHERE ReferenceChartTypeId = RCT.ReferenceChartTypeID) AS ChartSetting,
		RCT.IsSync1DMultiColor_ISO,
		RCT.IsSync1DMultiColor_G7,
		RCT.Validation_Sync1D_Multi_Color,
		RCT.DeviceFamilyID

	FROM
		CP.ReferenceChartType AS RCT INNER JOIN
		CP.InstrumentType AS IST ON IST.InstrumentTypeID = RCT.InstrumentTypeID 
	WHERE RCT.ReferenceChartTypeID = @ReferenceChartTypeID
END
END



