-- =============================================
-- Author:		Dhruti Patel
-- =============================================
ALTER PROCEDURE [CPSync4DPlus].[GetChartData_InkPaper] 
@MeasurementID   int   
,@From_ΔE NVARCHAR(20)
AS  
BEGIN  

-------Dataset for getting measurement patch details-------------

BEGIN TRY
	------First select Previous Run's ID for showing comparison Avg DeltaE------
	DECLARE @PreOptimizeRunID INT;
	
	SELECT @PreOptimizeRunID = [CPSync4DPlus].[GetPreviousRunID] (@MeasurementID)
	
	--ΔE 
	SELECT 
		CASE 
			WHEN VW.PatchTypeID = 8 THEN '#D4CBC5'
			ELSE HTMLColor
		END AS HTMLColor,	
		VW.PatchTypeID,
		CASE 
			WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
			ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) 
		END AS ΔE,
		ISNULL((CASE
					WHEN VW.PatchTypeID IN (1,2,3) THEN (SELECT TOP 1 --CMY LIMIT
													CASE 
														WHEN 43 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (43,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (4) THEN (SELECT TOP 1 --K LIMIT
													CASE 
														WHEN 44 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (44,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (5,6,7) THEN (SELECT TOP 1  --RGB LIMIT
													CASE 
														WHEN 14 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (14,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (8) THEN (SELECT TOP 1      --PAPER LIMIT
													CASE 
														WHEN 3 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (3,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
				ELSE 0
		END),0) AS ΔELimit		
		,CONVERT(decimal(5,2),ROUND(AVG(TargetL),2)) AS TargetL, CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta
		,CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb, CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L
		,CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a, CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b
		,[AvgΔE] AS PreAvgΔE
		FROM 
			CPSync4DPlus.vwGetUniquePatches AS VW LEFT OUTER JOIN CPSync4DPlus.OptimizeRun_PreviousRunData AS PR
			ON PR.PatchTypeID = VW.PatchTypeID AND PR.OptimizeRunID = @PreOptimizeRunID 
		WHERE 
			MeasurementID = @MeasurementID AND VW.PatchTypeID >= 1 AND VW.PatchTypeID <= 9
		GROUP BY
				MeasurementID
				,VW.PatchTypeID
				,HTMLColor	
				,[AvgΔE]
		ORDER BY PatchTypeID

	--Paper
	SELECT    				
			CONVERT(decimal(5,2),ROUND(AVG(TargetL),2)) AS TargetL, 
			CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta, 
			CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb,			
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b,	
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L - TargetL),2)) As ΔL,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a - Targeta),2)) AS Δa,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b - Targetb),2)) AS Δb
		
	FROM    CPSync4DPlus.vwGetUniquePatches
	WHERE   MeasurementID = @MeasurementID AND PatchTypeID = 8
	GROUP BY
            MeasurementID
			,PatchTypeID
	ORDER BY PatchTypeID 

	--Density  
	SELECT  VW.PatchTypeID, HTMLColor,
	CASE   
		WHEN VW.PatchTypeID = 1 THEN CONVERT(decimal(5,2),ROUND(AVG(Density_C),2)) 
		WHEN VW.PatchTypeID = 2 THEN CONVERT(decimal(5,2),ROUND(AVG(Density_M),2))   
		WHEN VW.PatchTypeID = 3 THEN CONVERT(decimal(5,2),ROUND(AVG(Density_Y),2))   
		ELSE  
			CONVERT(decimal(5,2),ROUND(AVG(Density_V),2))  	 
			END As Density  
		,(SELECT  CONVERT(decimal(5,2),ROUND(AVG(Target),2))  FROM CPSync.ProjectTargetDensity AS PTD WHERE PTD.ProjectID = VW.ProjectID AND PTD.PatchTypeID = VW.PatchtypeID) AS DensityLimit
		,AvgDensity AS PreAvgDensity
	FROM    CPSync4DPlus.vwGetUniquePatches AS VW LEFT OUTER JOIN CPSync4DPlus.OptimizeRun_PreviousRunData AS PR
			ON PR.PatchTypeID = VW.PatchTypeID AND PR.OptimizeRunID = @PreOptimizeRunID 
	WHERE   MeasurementID = @MeasurementID AND VW.PatchTypeID >= 1 AND VW.PatchTypeID <= 4
	GROUP BY
            MeasurementID
			,VW.PatchTypeID, HTMLColor, ProjectID, AvgDensity
	ORDER BY VW.PatchTypeID 
  

END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		DECLARE @ERROR_NUMBER INT 
		DECLARE @ERROR_SEVERITY INT
		DECLARE @ERROR_STATE INT 
		DECLARE @ERROR_PROCEDURE NVARCHAR(252) 
		DECLARE @ERROR_LINE INT 
		DECLARE @ERROR_MESSAGE NVARCHAR(4000) 
		SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE() 
		EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE 
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH
END

















GO


