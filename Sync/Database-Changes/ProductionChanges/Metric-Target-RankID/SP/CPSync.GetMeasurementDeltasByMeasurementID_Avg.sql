
ALTER PROCEDURE [CPSync].[GetMeasurementDeltasByMeasurementID_Avg]  
@MeasurementID   int
,@From_ΔE NVARCHAR(20)  
AS  
BEGIN   

	DECLARE @OptimizeRunID int
		SELECT @OptimizeRunID = OptimizeRunID FROM CPSync.Measurement WHERE MeasurementID = @MeasurementID

-------------Get data of Measurement as Summary------------------
	SELECT  M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
						  '' AS Customer, '' AS Notes, MTP.dEMethod, MTP.MetricTargetPresetName, MTP.ValidationType,
						  CASE sum(CONVERT(INT,MM.IsNotOK))
							WHEN 0 THEN 'OK'
							ELSE 'Out of Tolerance'
						  END AS MeasurementStatus							
						,(SELECT PrintStandard FROM CP.PrintStandardType AS PST 
								INNER JOIN CPSync4D.Project AS P ON P.PrintStandardTypeID = PST.PrintStandardTypeID
								INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
								INNER JOIN CPSync.Measurement AS M ON M.OptimizeRunID = OPT.OptimizeRunID						
								WHERE M.MeasurementID = @MeasurementID) AS PrintStandard
	FROM       CPSync.Measurement AS M INNER JOIN 
						  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
						  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
						  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
						  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
						  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
						  CPSync.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
						  MS.MetricSumID = MM.MetricSumID
	WHERE M.MeasurementID = @MeasurementID
	GROUP BY M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
			MTP.dEMethod, MTP.MetricTargetPresetName, MTP.ValidationType


		----Get value whether this MTP has Weighted tolerance or not, according to that pass Metric Sums list to display in summary list
			DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM       CPSync.Measurement AS M INNER JOIN
								   CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								   CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								   CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.IsUsed = 1 AND M.OptimizeRunID = @OptimizeRunID AND MS.MetricSumID = 37		
			
			IF @Isweighted >= 1 ----If Weighted, don't pass Non-Weighted MetricSumIDs
				BEGIN
					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						ROW_NUMBER() OVER (ORDER BY MS.MetricSumID) AS MetricMeasuredID,
						MS.MetricSumID, MS.MetricSumName, MM.TargetValue, ISNULL(CONVERT(decimal(5,2),ROUND(AVG(MM.MeasuredValue),2)),0) AS MeasuredValue,
						CASE WHEN AVG(MM.MeasuredValue) > AVG(MM.TargetValue) THEN 1
							ELSE 0
						END AS IsNotOK
					FROM      CPSync.Measurement AS M INNER JOIN 
										  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										  INNER JOIN CPSyncV.MetricTarget AS MT ON MT.MetricTargetPresetID = MTP.MetricTargetPresetID AND MT.MetricSumID = MM.MetricSumID										
					WHERE  MM.MeasuredValue ! = 0 AND M.IsUsed = 1 AND M.OptimizeRunID = @OptimizeRunID AND MS.MetricSumID NOT IN (8,9,10,11,12,13)			
					GROUP BY MS.MetricSumID, MS.MetricSumName, MM.TargetValue, ISNULL(MT.RankID,MS.RankID)
					ORDER BY ISNULL(MT.RankID,MS.RankID)
				END
			ELSE              ----If Not-Weighted, don't pass Weighted MetricSumIDs
				BEGIN
					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						ROW_NUMBER() OVER (ORDER BY MS.MetricSumID) AS MetricMeasuredID,
						MS.MetricSumID, MS.MetricSumName, MM.TargetValue, ISNULL(CONVERT(decimal(5,2),ROUND(AVG(MM.MeasuredValue),2)),0) AS MeasuredValue,
						CASE WHEN AVG(MM.MeasuredValue) > MM.TargetValue THEN 1
							ELSE 0
						END AS IsNotOK
					FROM      CPSync.Measurement AS M INNER JOIN 
										  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										  INNER JOIN CPSyncV.MetricTarget AS MT ON MT.MetricTargetPresetID = MTP.MetricTargetPresetID AND MT.MetricSumID = MM.MetricSumID		
					WHERE  MM.MeasuredValue ! = 0 AND M.IsUsed = 1 AND M.OptimizeRunID = @OptimizeRunID AND MS.MetricSumID NOT IN (36,37,38,39,40,41)	
					GROUP BY MS.MetricSumID, MS.MetricSumName, MM.TargetValue, ISNULL(MT.RankID,MS.RankID)
					ORDER BY ISNULL(MT.RankID,MS.RankID)
				END

END



GO


