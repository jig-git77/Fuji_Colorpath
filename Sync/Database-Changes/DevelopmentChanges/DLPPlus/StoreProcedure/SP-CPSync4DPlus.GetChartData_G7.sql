
GO

/****** Object:  StoredProcedure [CPSync4DPlus].[GetChartData_G7]    Script Date: 1/12/2021 4:27:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








-- =============================================
-- Author:		<DHRUTI PATEL>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [CPSync4DPlus].[GetChartData_G7]
/* Retrieve data for G7 Balance tab for selected measurement */  
@MeasurementID  int  
AS  
BEGIN  
  
	----Get value whether this MTP has Weighted tolerance or not, according to that pass Metric Sums list to display in summary list
			DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM       CPSync4D.Measurement AS M INNER JOIN
								  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		
			
	 --Gray Δch / ΔF  
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔF),2)) AS ΔF, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔF),2)) AS wΔF,
		(SELECT TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSync4D.Project AS P ON MT.MetricTargetPresetID = P.MetricTargetPresetID
		INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
		INNER JOIN CPSync4D.Measurement AS M ON OPT.OptimizeRuniD = M.OptimizeRuniD
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (9,37)) As ΔLimit 
	 FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSync4DPlus.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 4 OR CC.ColorCharID = 18) ---4 = ΔF AND 18 = wΔF
	 GROUP BY CMYK_C	
	 ORDER BY CMYK_C
	
	--Gray Δch / ΔF Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM      
						CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (36,37)
			END
	ELSE			----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM      
						CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (8,9)
			END

	--Black ΔL
	 SELECT  RC.CMYK_K, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔL),2)) AS ΔL, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔL),2)) AS wΔL, 
		(SELECT TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSync4D.Project AS P ON MT.MetricTargetPresetID = P.MetricTargetPresetID
		INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
		INNER JOIN CPSync4D.Measurement AS M ON OPT.OptimizeRuniD = M.OptimizeRuniD
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (11,39)) As ΔLimit 
	 FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSync4DPlus.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 16 OR CC.ColorCharID = 17) AND RC.PatchTypeID in (4,15,25,26,27)
	 GROUP BY CMYK_K
	 ORDER BY CMYK_K  

	--Black ΔL Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM    CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (38,39)
			END
	ELSE				----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM    CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (10,11)
			END
	--CMY / Gray ΔL
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔL),2)) AS ΔL, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔL),2)) AS wΔL, 
		(SELECT TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSync4D.Project AS P ON MT.MetricTargetPresetID = P.MetricTargetPresetID
		INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
		INNER JOIN CPSync4D.Measurement AS M ON OPT.OptimizeRuniD = M.OptimizeRuniD
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (13,41)) As ΔLimit 
	 FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSync4DPlus.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 16 OR CC.ColorCharID = 17) AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) 
	 GROUP BY CMYK_C
	 ORDER BY CMYK_C  

	--Gray ΔL Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM    CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (40,41)
			END
	ELSE			----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT  MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM    CPSync4D.Measurement AS M INNER JOIN 
						CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (12,13)
			END

	--Black G7 NPD
	 SELECT  RC.CMYK_K, CONVERT(decimal(5,2),ROUND(MAX(CC.G7NPDC),2)) AS G7NPDC,
			CONVERT(decimal(5,2),ROUND((SELECT LOG10(MAX(MP.CIEXYZ_Y)) FROM  CPSync4D.Measurement AS M 
				INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID 
				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) - LOG10(MAX(MP.CIEXYZ_Y)),2)) AS G7NPDC_Act			 
		FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSync4DPlus.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND CC.ColorCharID = 13 AND RC.PatchTypeID in (4,15,25,26,27)
	 GROUP BY RC.CMYK_K
	 ORDER BY CMYK_K  

	 --Gray G7 NPD
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.G7NPDC),2)) AS G7NPDC,
			CONVERT(decimal(5,2),ROUND((SELECT LOG10(MAX(MP.CIEXYZ_Y)) FROM  CPSync4D.Measurement AS M 
				INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID 
				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) - LOG10(MAX(MP.CIEXYZ_Y)),2)) AS G7NPDC_Act		
		FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSync4DPlus.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND CC.ColorCharID = 13 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) 
	 GROUP BY RC.CMYK_C
	 ORDER BY CMYK_C  	

END












GO


