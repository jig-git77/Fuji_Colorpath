

/****** Object:  StoredProcedure [CPSync4D].[GetReferenceChart]    Script Date: 4/5/2021 5:02:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [CPSync4D].[GetReferenceChart]
	@ProjectID int,
	@ReferenceChartDisplayType int
AS
			SELECT
				RCT.ReferenceChartTypeID,
				RCT.ChartName,
				RCT.NumOfRows,
				RCT.NumOfCols,
				(CAST (RCT.NumOfRows As Decimal(9,2)) * RCT.NumOfCols) As TotalPatches,
				MD.InstrumentTypeID,
				IsNull(MD.SerialNumber,'') as MeasureDeviceSrNo,
				IL.IlluminantType,
				OT.ObserverType,
				CASE @ReferenceChartDisplayType
					WHEN 3 THEN	MS.VUVMode				-- Verification
					ELSE MS.UVMode
				END AS UVMode,
				CASE @ReferenceChartDisplayType
					WHEN 3 THEN	MS.VReadMode			-- Verification
					ELSE MS.ReadMode
				END AS ReadMode,		
				CASE @ReferenceChartDisplayType
					WHEN 3 THEN	MS.VMeasurementCount	-- Verification
					ELSE MS.MeasurementCount
				END AS MeasurementCount,
				P.DensityType
				
			FROM   
				CP.ReferenceChartType AS RCT INNER JOIN CPSync4D.MeasurementSetting MS
					ON RCT.ReferenceChartTypeID = 
						CASE @ReferenceChartDisplayType
							WHEN 1 THEN MS.ReferenceChartTypeID -- Profile Chart
							WHEN 2 THEN MS.OPTReferenceChartTypeID -- Optimization Chart
							WHEN 3 THEN MS.VReferenceChartTypeID -- Verification Chart
						END
				INNER JOIN CP.MeasurementDevice MD
					ON MD.MeasurementDeviceID = 
						CASE @ReferenceChartDisplayType
							WHEN 1 THEN MS.MeasurementDeviceID -- Profile Chart
							WHEN 2 THEN MS.MeasurementDeviceID -- Optimization Chart
							WHEN 3 THEN MS.VMeasurementDeviceID -- Verification Chart
						END
				INNER JOIN CP.Illuminant IL
					ON MS.Illuminant = IL.IlluminantID
				INNER JOIN CP.ObserverType OT
					ON MS.ObserverType = OT.ObserverTypeID
				INNER JOIN CPSync4D.Project P
					ON MS.ProjectID = P.ProjectID
			WHERE  
				MS.ProjectID = @ProjectID

			SELECT  
				RC.ChartTypeID,  
				RC.SampleName,  
				RC.CMYK_C,  
				RC.CMYK_M,  
				RC.CMYK_Y,  
				RC.CMYK_K,  
				RC.PatchTypeID,
				RC.HTMLColor,
				RC.Channel_5,
				RC.Channel_6,
				RC.Channel_7,
				RC.Channel_8,
				RC.Channel_9
			FROM  
				CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting MS  
				on RC.ChartTypeID = 
					CASE @ReferenceChartDisplayType
							WHEN 1 THEN MS.ReferenceChartTypeID -- Profile Chart
							WHEN 2 THEN MS.OPTReferenceChartTypeID -- Optimization Chart
							WHEN 3 THEN MS.VReferenceChartTypeID -- Verification Chart
						END
			WHERE  
				MS.ProjectID = @ProjectID
			ORDER BY  
				ReferenceChartID







GO


