/****** Object:  StoredProcedure [Report].[GetSingle1DMeasurement]    Script Date: 1/17/2023 2:52:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [Report].[GetSingle1DMeasurement] 
@MeasurementID   int  
,@From_ΔE  NVARCHAR(20)  
,@ProjectType int 
AS
BEGIN

BEGIN TRY

	--Table0: Job Related Data  
	SELECT   P.ProjectName, OPT.RunName, M.MeasurementName, M.MeasurementDate, D.DeviceName, S.ProfileName, PC.Media, PC.Colorant, PC.RipSettings, B.Location, C.CompanyName, B.BranchName
			,CASE WHEN M.MetricTargetPresetID IS NOT NULL THEN (SELECT MetricTargetPresetName FROM CPSyncV.MetricTargetPreset WHERE MetricTargetPresetID = M.MetricTargetPresetID)
							   ELSE MTP.MetricTargetPresetName
						  END AS MetricTargetPresetName
			, MTP.dEMethod AS DeltaEFormula
			,CASE WHEN (SELECT SUM(CONVERT(INT,IsNotOk)) from CPSync.MetricMeasured where MeasurementID = @MeasurementID) > 0 THEN 'Out Of Tolerance'
				  ELSE 'OK'
			 END AS JobStatus, MTP.ValidationType
			 ,PC.TechnicianName, I.InstrumentType, MD.SerialNumber, MS.UVMode
			 ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.ReferenceChartTypeID) AS ChartName
			 ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.OPTReferenceChartTypeID) AS OPTChartName
			 ,PC.InkOrder, PC.FountainSolution, PC.Blanket, PC.BlanketAge, PC.Screening, PC.Coating, PC.Notes, P.LicenseTypeID, ISNULL(PC.PrintingType,0) AS PrintingType
	FROM      CPSync.Measurement AS M INNER JOIN 
					  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID =  M.OptimizeRunID INNER JOIN	                     
				      CPSync4D.Project AS P ON OPT.ProjectID = P.ProjectID INNER JOIN	
					  CPSync4D.MeasurementSetting as MS ON MS.ProjectID = P.ProjectID INNER JOIN
					  CP.MeasurementDevice AS MD ON MD.MeasurementDeviceID = MS.MeasurementDeviceID INNER JOIN
					  CP.InstrumentType AS I ON I.InstrumentTypeID = MD.InstrumentTypeID INNER JOIN
					  CPSyncV.MetricTargetPreset AS MTP ON MTP.MetricTargetPresetID = P.MetricTargetPresetID INNER JOIN                 
					  CPSync4D.PrintCondition AS PC ON P.ProjectID = PC.ProjectID INNER JOIN
					  CP.PrintStandardType PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID INNER JOIN
					  CPSync4D.SourceProfile AS S ON PST.SourceProfileID = S.SourceProfileID INNER JOIN
                      CP.Device AS D ON P.CPDeviceID = D.DeviceID INNER JOIN
                      CP.License AS L ON D.LicenseID = L.LicenseID INNER JOIN
                      CP.Branch AS B ON L.BranchID = B.BranchID INNER JOIN
					  CP.Company AS C ON C.CompanyID = B.CompanyID
	WHERE M.MeasurementID = @MeasurementID


	--Table1: For the spider plot a* - b* chart  

	SELECT      
			HTMLColor, 
			PatchType,	
			CMYK_C, 
			CMYK_M, 
			CMYK_Y, 
			CMYK_K,	
			ISNULL(TargetL,0) AS TargetL,	
			ISNULL(Targeta,0) AS Targeta,	
			ISNULL(Targetb,0) AS Targetb,				
			CIELAB_L,
			CIELAB_a,
			CIELAB_b,
			CASE WHEN PatchType = 'Paper' THEN 0  
				WHEN PatchType = 'Cyan' THEN 1  
				WHEN PatchType = 'Green' THEN 2  
				WHEN PatchType = 'Yellow' THEN 3  
				WHEN PatchType = 'Red' THEN 4  
				WHEN PatchType = 'Magenta' THEN 5  
				WHEN PatchType = 'Blue' THEN 6  
				END AS RANK  
	FROM   
	(  
		SELECT RC.HTMLColor,  
		CASE   
			WHEN RC.PatchTypeID = 8 OR (CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Paper' 	 
			WHEN RC.PatchTypeID = 1 OR (CMYK_C > 0 AND CMYK_C < 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Cyan'  
			WHEN RC.PatchTypeID = 2 OR (CMYK_C = 0 AND CMYK_M > 0 AND CMYK_M < 100 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Magenta'  
			WHEN RC.PatchTypeID = 3 OR (CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y > 0 AND  CMYK_Y < 100 AND CMYK_K = 0) THEN 'Yellow'  
			WHEN RC.PatchTypeID = 5 OR (CMYK_C = 0 AND CMYK_K = 0 AND CMYK_M = CMYK_Y AND CMYK_M <> 0)   THEN 'Red'  
			WHEN RC.PatchTypeID = 6 OR (CMYK_C = CMYK_Y AND CMYK_M = 0 AND CMYK_K = 0) THEN 'Green'  			
			WHEN RC.PatchTypeID = 7 OR (CMYK_C = CMYK_M AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Blue'  		
			ELSE 'X' 	
			END AS PatchType  ,
		RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,	
			PTL.[TargetL*] AS TargetL, 
			PTL.[Targeta*] AS Targeta, 
			PTL.[Targetb*] AS Targetb,			
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_L*]),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_a*]),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_b*]),2)) AS CIELAB_b
		
		FROM  CPSync.MeasurementPatch AS MP 
					INNER JOIN CPSync.Measurement AS M ON M.MeasurementID = MP.MeasurementID
					INNER JOIN CPSync.OptimizeRun AS OPT ON M.OptimizeRunID = OPT.OptimizeRunID
					INNER JOIN CPSync4D.Project AS P ON OPT.ProjectID = P.ProjectID 
					INNER JOIN CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName
					LEFT OUTER JOIN CPSync.ProjectTargetLAB AS PTL ON PTL.ProjectID = P.ProjectID AND PTL.PatchTypeID = RC.PatchTypeID
		WHERE  M.MeasurementID = @MeasurementID 
		GROUP BY               
			RC.PatchTypeID
			,RC.HTMLColor	
			,RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K
			,PTL.[TargetL*], PTL.[Targeta*], PTL.[Targetb*] 	
	) AS GAMUT   
	WHERE PatchType <> 'X'
	ORDER BY RANK,CMYK_C,CMYK_M,CMYK_Y,CMYK_K 

			
	--TABLE 2: for the delta metrics -- summary delta 	
	IF @ProjectType = 1  --TABLE: G7 table (Only for G7 Type of Sync1D, i.e. ProjectType = 1)
		BEGIN
			----Get value whether this MTP has Weighted tolerance or not, according to that pass Metric Sums list to draw on chart
			DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM  CPSync.Measurement AS M INNER JOIN
								  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		
			
			IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
				BEGIN
					SELECT  MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
							FROM    CPSync.Measurement AS M INNER JOIN 
									CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									CPSync.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									MS.MetricSumID = MM.MetricSumID
							WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (36,37,38,39,40,41)
				END
			ELSE				----If Not-Weighted, pass regular MetricSumIDs
				BEGIN
					SELECT  MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
							FROM    CPSync.Measurement AS M INNER JOIN 
									CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									CPSync.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									MS.MetricSumID = MM.MetricSumID
							WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (8,9,10,11,12,13)
				END	
		END
	ELSE -----Sync1D ISO project types
		BEGIN
			SELECT     MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK,MS.IsSummary
					FROM       CPSync.Measurement AS M INNER JOIN 
								  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
							      CPSync.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
					WHERE  MM.MeasuredValue ! = 0 AND M.MeasurementID = @MeasurementID
		END

	--TABLE 3: for the delta E metrics -- summary delta
        SELECT
			CASE 
				WHEN PatchTypeID = 8 THEN '#D4CBC5'		------Give different htmlcolor for paper, so it will look good on chart
				ELSE HTMLColor
			END AS HTMLColor,
			PatchTypeID,
			CASE 
				WHEN PatchTypeID = 1 THEN 'Cyan'
				WHEN PatchTypeID = 2 THEN 'Magenta'
				WHEN PatchTypeID = 3 THEN 'Yellow'
				WHEN PatchTypeID = 4 THEN 'Black'
				WHEN PatchTypeID = 5 THEN 'Red'
				WHEN PatchTypeID = 6 THEN 'Green'
				WHEN PatchTypeID = 7 THEN 'Blue'
				WHEN PatchTypeID = 8 THEN 'Paper'
				WHEN PatchTypeID = 9 THEN '3-C'
				ELSE 'X'
			END AS PatchType,
			CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))
			END AS ΔE,
			ISNULL((CASE
					WHEN PatchTypeID IN (1,2,3) THEN (SELECT TOP 1 --CMY
													CASE 
														WHEN 43 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync.MetricMeasured WHERE MetricSumID IN (43,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (4) THEN (SELECT TOP 1 --K
													CASE 
														WHEN 44 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync.MetricMeasured WHERE MetricSumID IN (44,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (5,6,7) THEN (SELECT TOP 1  --RGB
													CASE 
														WHEN 14 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync.MetricMeasured WHERE MetricSumID IN (14,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (8) THEN (SELECT TOP 1      --PAPER
													CASE 
														WHEN 3 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync.MetricMeasured WHERE MetricSumID IN (3,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
				ELSE 0
		END),0) AS ΔELimit
		,CASE 
				WHEN PatchTypeID = 1 THEN 'C'
				WHEN PatchTypeID = 2 THEN 'M'
				WHEN PatchTypeID = 3 THEN 'Y'
				WHEN PatchTypeID = 4 THEN 'K'
				WHEN PatchTypeID = 5 THEN 'R'
				WHEN PatchTypeID = 6 THEN 'G'
				WHEN PatchTypeID = 7 THEN 'B'
				WHEN PatchTypeID = 8 THEN 'Substrate'
				WHEN PatchTypeID = 9 THEN 'CMY'
				ELSE 'X'
			END AS ShortPatchType
		,CONVERT(decimal(5,2),ROUND(AVG(TargetL),2)) AS TargetL
		,CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta
		,CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb
		,CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS ActualL
		,CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS Actuala
		,CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS Actualb
      FROM CPSync.vwGetUniquePatches
	  WHERE MeasurementID = @MeasurementID AND PatchTypeID IN (1,2,3,4,5,6,7,8,9)
       GROUP BY
                MeasurementID
				,PatchTypeID
				,HTMLColor				
       ORDER BY PatchTypeID

	--Table 4: TVI MaxSpread
	;WITH TVIMaxSpreades AS		----Store all TVI into Temp table to convert rows into columns later on
	(
		SELECT  'Tone20' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
				
		UNION ALL
		SELECT  'Tone25' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 			
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
			
		UNION ALL
		SELECT  'Tone40' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 			
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches		
			
		UNION ALL
			SELECT  'Tone50' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 				
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches		
				
		UNION ALL
		SELECT  'Tone60' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches		
				
		UNION ALL
			SELECT  'Tone75' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
				
		UNION ALL
		SELECT  'Tone80' As TonePct,  
			(MAX(ColorCharValue) - MIN(ColorCharValue)) As MaxSpread
			FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
			INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
			INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
			
	)
	----Use above Temp table and convert rows into columns using "PIVOT" command
	SELECT *
	FROM
	(
	  SELECT CONVERT(decimal(5,2),ROUND(ISNULL(MaxSpread,0),2)) AS MaxSpread, TonePct FROM TVIMaxSpreades 
	) d
	pivot
	(
	  MAX(MaxSpread)
	  FOR TonePct IN (Tone20, Tone25, Tone40, Tone50, Tone60, Tone75, Tone80)
	) piv

	--Table 5: Density Data
	
	------First select Previous Run's ID for showing comparison Avg DeltaE------
	DECLARE @PreOptimizeRunID INT;
	SELECT @PreOptimizeRunID = [CPSync].[GetPreviousRunID] (@MeasurementID)

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
	FROM    CPSync.vwGetUniquePatches AS VW LEFT OUTER JOIN CPSync.OptimizeRun_PreviousRunData AS PR
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
