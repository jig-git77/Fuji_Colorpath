
GO

/****** Object:  StoredProcedure [CPSync4DPlus].[GetMeasurementDeltasByMeasurementID]    Script Date: 1/12/2021 4:28:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [CPSync4DPlus].[GetMeasurementDeltasByMeasurementID]  
@MeasurementID   int
,@From_ΔE NVARCHAR(20) 
AS  
BEGIN   

-------------Get data of Measurement as Summary------------------
	SELECT  M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
						  '' AS Customer, '' AS Notes, MTP.dEMethod, 
						  CASE WHEN M.MetricTargetPresetID IS NOT NULL THEN (SELECT MetricTargetPresetName FROM CPSyncV.MetricTargetPreset WHERE MetricTargetPresetID = M.MetricTargetPresetID)
							   ELSE MTP.MetricTargetPresetName
						  END AS MetricTargetPresetName
						, MTP.ValidationType,
						  CASE sum(CONVERT(INT,MM.IsNotOK))
							WHEN 0 THEN 'OK'
							ELSE 'Out of Tolerance'
						  END AS MeasurementStatus							
						,(SELECT PrintStandard FROM CP.PrintStandardType AS PST 
								INNER JOIN CPSync4D.Project AS P ON P.PrintStandardTypeID = PST.PrintStandardTypeID
								INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
								INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OPT.OptimizeRunID						
								WHERE M.MeasurementID = @MeasurementID) AS PrintStandard
	FROM       CPSync4D.Measurement AS M INNER JOIN 
						  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						  CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
						  MS.MetricSumID = MM.MetricSumID
	WHERE M.MeasurementID = @MeasurementID
	GROUP BY M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
			MTP.dEMethod, MTP.MetricTargetPresetName, MTP.ValidationType, M.MetricTargetPresetID


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
			
			IF @Isweighted >= 1 ----If Weighted, don't pass Non-Weighted MetricSumIDs
				BEGIN
					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						 MM.MetricMeasuredID, MS.MetricSumID, MS.MetricSumName, 
						  MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
					FROM      CPSync4D.Measurement AS M INNER JOIN 
										  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync4DPlus.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										
					WHERE  MM.MeasuredValue ! = 0 AND M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (8,9,10,11,12,13)			
					ORDER BY MS.RankID
				END
			ELSE              ----If Not-Weighted, don't pass Weighted MetricSumIDs
				BEGIN
					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						 MM.MetricMeasuredID, MS.MetricSumID, MS.MetricSumName, 
						  MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
					FROM      CPSync4D.Measurement AS M INNER JOIN 
										  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync4DPlus.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										
					WHERE  MM.MeasuredValue ! = 0 AND M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (36,37,38,39,40,41)	
					ORDER BY MS.RankID
				END



END











GO


