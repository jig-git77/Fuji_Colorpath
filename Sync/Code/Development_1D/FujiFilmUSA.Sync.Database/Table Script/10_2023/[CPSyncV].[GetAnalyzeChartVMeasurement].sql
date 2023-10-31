
/****** Object:  StoredProcedure [CPSyncV].[GetAnalyzeChartVMeasurement]    Script Date: 10/31/2023 10:42:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [CPSyncV].[GetAnalyzeChartVMeasurement] 
   @ProjectID INT
  ,@From_ΔE NVARCHAR(20)
AS

		
		SELECT * FROM 
					(
					SELECT TOP 100
						M.MeasurementID,
						M.MeasurementName,
						M.MeasurementDate,
						MTP.dEMethod,
						CASE sum(CONVERT(INT,MM.IsNotOK))
							WHEN 0 THEN 'OK'
							ELSE 'Out of Tolerance'
						  END AS MeasurementStatus,	
						M.IsUsed  
					FROM
						CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementSetting AS MST ON MST.MeasurementID = M.MeasurementID
						INNER JOIN CPSyncV.MetricTargetPreset AS MTP ON MTP.MetricTargetPresetID = MST.MetricTargetPresetID				
						INNER JOIN CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID 
					WHERE
						M.ProjectID = @ProjectID
						AND M.IsOK = 1 	
						AND ((					----If Weighted, don't pass Non-Weighted MetricSumIDs
							MTP.Weighted >= 1
							AND MM.MetricSumID NOT IN (8,9,10,11,12,13,42))
						OR (
							MTP.Weighted = 0	----If Not-Weighted, don't pass Weighted MetricSumIDs
							AND MM.MetricSumID NOT IN (36,37,38,39,40,41,42))
						)
						 -----Start 7/28/23: Uncomment this section to fix issue of not displaying tringles on trendline chart for weighted metricsums 'out of tolerance' status because MTP is set to non weighted
						--AND 
						--( (SELECT    Count(*)
						--           FROM    CPSyncV.measurement AS M2
						--                INNER JOIN CPSyncV.MeasurementSetting AS MST ON M2.MeasurementID = MST.MeasurementID
						--                INNER JOIN CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID
						--                INNER JOIN CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID
						--                INNER JOIN CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
						--           WHERE    M2.MeasurementID = M.MeasurementID AND MS.MetricSumID = 37
						--   ) >= 1 AND MM.MetricSumID NOT IN ( 8, 9, 10, 11, 12, 13, 42 )   ----If Weighted, don't pass Non-Weighted MetricSumIDs
						--   OR MM.MetricSumID NOT IN (36,37,38,39,40,41,42)    ----If Not-Weighted, don't pass Weighted MetricSumIDs
						-- )	
						------End:
					GROUP BY
						M.MeasurementID,
						M.MeasurementName,
						M.MeasurementDate,
						MTP.dEMethod,
						M.IsUsed  
					ORDER BY M.MeasurementID DESC
					) AS V ORDER BY V.MeasurementID

		
			---Get Measurement Patch Details-------------
			SELECT 
				M.MeasurementID 
				,RC.HTMLColor 
				,RC.PatchTypeID 				
				,CASE 
					WHEN SUBSTRING(MTP.dEMethod, 2, LEN(MTP.dEMethod)) = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) 
				 END AS ΔE2000									
			FROM 
				CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID  
				INNER JOIN CPSyncV.MeasurementSetting AS MS ON MS.MeasurementID = M.MeasurementID
				INNER JOIN CPSyncV.MetricTargetPreset AS MTP ON MTP.MetricTargetPresetID = MS.MetricTargetPresetID
				INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID
			WHERE 
				RC.PatchTypeID >= 1 
				AND RC.PatchTypeID <= 8 			
				--AND M.ProjectID = @ProjectID 	
				AND M.MeasurementID IN (SELECT TOP 100 MeasurementID FROM CPSyncV.Measurement WHERE ProjectID = @ProjectID ORDER BY MeasurementID DESC)	
				AND M.IsOK = 1			
			GROUP BY
				M.MeasurementID
				,RC.HTMLColor 
				,RC.PatchTypeID  
				,MTP.dEMethod 
			ORDER BY
				M.MeasurementID,
				RC.PatchTypeID






