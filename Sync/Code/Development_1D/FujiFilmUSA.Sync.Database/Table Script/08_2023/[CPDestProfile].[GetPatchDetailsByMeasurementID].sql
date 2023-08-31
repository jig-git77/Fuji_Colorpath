ALTER PROCEDURE [CPDestProfile].[GetPatchDetailsByMeasurementID]
@MeasurementID   INT   
AS  
BEGIN  

	-- for this destination profile license type , paper scaling will be always be 0
	-------Dataset for getting measurement patch details-------------
	SELECT      RC.ReferenceChartID,
				RC.SampleID, 
				RC.SampleName, 
				RC.CMYK_C, 
				RC.CMYK_M, 
				RC.CMYK_Y, 
				RC.CMYK_K, 
				RC.HTMLColor, 					
				'0.00' AS TargetL,
				'0.00' AS Targeta,
				'0.00' AS Targetb,
				MP.[CIELAB_L*] AS CIELAB_L,
				MP.[CIELAB_a*] AS CIELAB_a,
				MP.[CIELAB_b*] AS CIELAB_b,
				'0.00' AS ΔE,
				'0.00' AS ΔE94,
				'0.00' AS ΔE2000,
				MP.Density_V,
				MP.Density_C,
				MP.Density_M,
				MP.Density_Y,
				RC.PatchTypeID,
				'0.00' AS ΔH,
				'0.00' AS ΔCh,
				'0.00' AS ΔL
	FROM 		CP.ReferenceChart AS RC
				INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
				INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
				INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
				INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
	WHERE		M.MeasurementID = @MeasurementID

	ORDER BY RC.ReferenceChartID

END
