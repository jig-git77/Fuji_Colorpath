
/****** Object:  StoredProcedure [CPSync].[CalculateMetrics]    Script Date: 5/5/2022 3:57:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [CPSync].[CalculateMetrics]	
	@MeasurementID INT,
	@UserID INT
AS
	BEGIN TRY
		BEGIN TRAN
			-- ALTER a temporary table to hold the records to be updated
			DECLARE @dEMethod VARCHAR(50)
			DECLARE @MetricSumID INT
			DECLARE @TargetValue DECIMAL(6,2)
			DECLARE @OutputValue DECIMAL(6,2)
			DECLARE @TEMP TABLE (dEMethod VARCHAR(50), MetricSumID INT, TargetValue DECIMAL(6,2))

			-- Insert into the temporary table a list of the records to be inserted
			INSERT INTO @TEMP (dEMethod, MetricSumID, TargetValue)
				SELECT  MTP.dEMethod, MS.MetricSumID, MT.TargetValue
						FROM   CPSync.Measurement AS M INNER JOIN 
									  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN 
									  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN									
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
						WHERE M.MeasurementID = @MeasurementID
						--ORDER BY MS.MetricSumID

			-- Start looping through the records
			WHILE EXISTS (SELECT * FROM @TEMP)
			BEGIN
				-- Grab the first record out
				SELECT Top 1 @MetricSumID = MetricSumID, @dEMethod = dEMethod, @TargetValue = TargetValue FROM @TEMP


				-- Generate all metrics according to metic sums
				IF  (@MetricSumID = 1) ---For 'ΔE Average'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
								ELSE AVG(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSync.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 2)  ----For 'ΔE Maximum'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSync.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 3)  ----For 'Paper ΔE Avg'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
																	ELSE AVG(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 4)  ----For 'CMYK ΔE Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 5)  ----For 'CMYK ΔH Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 6)  ----For 'FOGRA Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (52,53,54,55,56,57)) AS MeasuredValue, 0														
					END
				ELSE IF (@MetricSumID = 7)  ----For 'CMYKRGB ΔH Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4,5,6,7)) AS MeasuredValue, 0																					
					END
				ELSE IF (@MetricSumID = 8)  ----For 'G7 Gray ΔCh Avg'
					BEGIN
--					EXEC [CPSync].[uspInsertColorCharΔCh] @MeasurementID 				
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 9)  ----For 'G7 Gray ΔCh Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 10)  ----For 'G7 Black ΔL Avg'
					BEGIN					
--					EXEC [CPSync].[uspInsertColorCharΔL] @MeasurementID 
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 11)  ----For 'G7 Black ΔL Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 12)  ----For 'G7 CMY / Gray ΔL Avg'
					BEGIN	
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 13)  ----For 'G7 CMY / Gray ΔL Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 14)  ----For 'RGB ΔE Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (5,6,7)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 15)  ----For 'CMY ΔTVI25'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136)		--25% C, M, Y Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 16)  ----For 'CMY ΔTVI50'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139)		--50% C, M, Y Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 17)  ----For 'CMY ΔTVI75'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142)		--75% C, M, Y Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 18)  ----For 'TVISpread25'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT   
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
									) ),0)	) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 19)  ----For 'TVISpread50'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT  
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches		
									) ),0)	) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 20)  ----For 'TVISpread75'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT  
								ISNULL(ABS (
								(
									SELECT ISNULL(MAX(ColorCharValue),0) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
									) -	
									(
										SELECT ISNULL(MIN(ColorCharValue),0) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
									) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 21)  ----For 'Paper ΔL' (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN	
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_L - TargetL),2))),2)),0)
								FROM CPSync.vwGetUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 22)  ----For 'Paper Δa'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_a - Targeta),2))),2)),0)
								FROM CPSync.vwGetUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 23)  ----For 'Paper Δb'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue,  
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_b - Targetb),2))),2)),0)
								FROM CPSync.vwGetUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 24)  ----For 'ΔE CMY100 Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 9) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 25)  ----For 'Japan Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,61,62,63,64,65)) AS MeasuredValue, 0
							END
				ELSE IF (@MetricSumID = 26)  ----For 'G7 Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)) AS MeasuredValue, 0
							END
				ELSE IF (@MetricSumID = 27)  ----For 'ΔE Maximum excluding Paper'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0	
							FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID != 8
							END
				ELSE IF (@MetricSumID = 28)  ----For 'ΔTVI20'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 29)  ----For 'ΔTVI40'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 30)  ----For 'ΔTVI60'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 31)  ----For 'ΔTVI80'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 32)  ----For 'TVISpread20'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT   
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
									) ),0)	) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 33)  ----For 'TVISpread40'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT  
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches		
									) ),0)	) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 34)  ----For 'TVISpread60'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT  
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches		
									) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 35)  ----For 'TVISpread80'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT  
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
									) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 36)  ----For 'G7 Gray wΔCh Avg'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 37)  ----For 'G7 Gray wΔCh Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 38)  ----For 'G7 Black wΔL Avg'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 39)  ----For 'G7 Black wΔL Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 40)  ----For 'G7 CMY / Gray wΔL Avg'
					BEGIN	
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 41)  ----For 'G7 CMY / Gray wΔL Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 43)  ----For 'CMY ΔE Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 44)  ----For 'K ΔE Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (4)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 45)  ----For 'K ΔTVI25'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (25,147)		--25% K Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 46)  ----For 'K ΔTVI50'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (26,150)		--50% K Patches		
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 47)  ----For 'K ΔTVI75'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
																		INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (27,153)		--75% K Patches	
								) AS V), 0							
					END
				 ----ELSE IF (@MetricSumID = 50)  ----For 'All Patches 95th Percentile' (We don't have patches supported for this in Sync1D)
					----BEGIN
					----	INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
					----		SELECT @MeasurementID, @MetricSumID, @TargetValue, 
					----		(SELECT TOP 1 ISNULL(CONVERT(decimal(5,2),ROUND(
					----											CASE 
					----												WHEN @dEMethod = 'ΔE' THEN CPSync.GetPercentileΔE(@MeasurementID,0.95,'E')
					----												ELSE CPSync.GetPercentileΔE(@MeasurementID,0.95,'E2000')
					----											END,2)),0)
					----			FROM
					----			CPSync.Measurement AS M INNER JOIN 
					----			CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
					----			CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
					----			WHERE M.MeasurementID = @MeasurementID) AS MeasuredValue, 0							
					----END
				 ELSE IF (@MetricSumID = 51)  ----For 'Paper ΔE Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 52)  ----For 'Paper L* Min'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MIN([CIELAB_L*]),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (8)) AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 53)  ----For 'CMY ΔH Max'
					BEGIN
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 54)  ----For 'K ΔL Max'
					BEGIN					
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 55)  ----For 'K ΔC Max'
					BEGIN	
						EXEC [CPSync].[uspInsertColorCharΔC] @MeasurementID				
						INSERT INTO CPSync.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync.Measurement AS M INNER JOIN 
								CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 19 AND RC.PatchTypeID in (4))
							AS MeasuredValue, 0	
					END
				
				DELETE FROM @TEMP WHERE MetricSumID = @MetricSumID
			END

			---Update IsNotOk status in CPSyncV.MetricMeasured table-----
			UPDATE CPSync.MetricMeasured 
				SET IsNotOK = CASE WHEN MeasuredValue <= TargetValue THEN 0	ELSE 1 END
				WHERE MeasurementID = @MeasurementID

			---Update IsNotOk status (Value should be greater than 88 Target value) in CPSyncV.MetricMeasured table for Paper Min L* metric (MetricSumID = 52)-----
			UPDATE CPSync.MetricMeasured 
				SET IsNotOK = CASE WHEN MeasuredValue > TargetValue  THEN 0	ELSE 1 END
				WHERE MeasurementID = @MeasurementID AND MetricSumID = 52


		COMMIT TRAN
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
		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in CPSync.CalculateMetrics measurement',@ERROR_MESSAGE,'Sync1D'
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH
















