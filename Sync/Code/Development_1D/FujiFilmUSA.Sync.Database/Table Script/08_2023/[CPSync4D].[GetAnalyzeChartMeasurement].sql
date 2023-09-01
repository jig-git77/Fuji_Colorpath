
/****** Object:  StoredProcedure [CPSync4D].[GetAnalyzeChartMeasurement]    Script Date: 9/1/2023 12:36:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSync4D].[GetAnalyzeChartMeasurement] 
   @OptimizeRunID INT
  ,@From_ΔE NVARCHAR(20)
AS
			-----Find LicenseType For Project-------
			DECLARE @LicenseTypeID INT;
			SELECT @LicenseTypeID = LicenseTypeID FROM [CPSync4D].[Project] AS P
				INNER JOIN [CPSync4D].[OptimizeRun] AS OPT ON OPT.ProjectID = P.ProjectID
				WHERE OPT.OptimizeRunID = @OptimizeRunID
	
			-----Regular queries to fetch measurement data--------
			SELECT
				MeasurementID,
				MeasurementName,
				IsUsed  
			FROM
				CPSync4D.Measurement
			WHERE
				OptimizeRunID = @OptimizeRunID

			
			SELECT 
				M.MeasurementID 
				,RC.HTMLColor 
				,RC.PatchTypeID  
				,CASE	-----------If @LicenseTypeID = 15 (Destination Profile), then no ΔE so pass hardcoded '0.9' as ΔE, for rest licensetypes let it go as it is----
					WHEN @LicenseTypeID != 15 THEN CASE 
							WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
							ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) 
						 END 
					ELSE 0.9 ------For @LicenseTypeID = 15 (Destination Profile)
					END AS ΔE2000
				
										
			FROM 
				CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP
					ON MP.MeasurementID = M.MeasurementID  
				INNER JOIN CP.ReferenceChart AS RC
					ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID
			WHERE 
				RC.PatchTypeID >= 1 
				AND RC.PatchTypeID <= 8 			
				AND M.OptimizeRunID = @OptimizeRunID 
			GROUP BY
				M.MeasurementID
				,RC.HTMLColor 
				,RC.PatchTypeID  
			ORDER BY
				RC.PatchTypeID



