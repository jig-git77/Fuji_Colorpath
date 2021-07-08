
GO
/****** Object:  StoredProcedure [CPSyncV].[CalculateMetrics]    Script Date: 7/7/2021 12:32:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSyncV].[CalculateMetrics]	
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
						FROM   CPSyncV.MetricTargetPreset AS MTP INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN 
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
						WHERE MST.MeasurementID = @MeasurementID
						ORDER BY MS.MetricSumID

			-----Call Extra SPs for delta calculations if this measurement need it-------			
				IF ((SELECT COUNT(*) FROM @TEMP WHERE MetricSumID IN (8,9,36,37)) > 0) ----For 'G7 Gray ΔCh Avg/Max weighted and non weighted'
				BEGIN
					EXEC [CPSyncV].[uspInsertColorCharΔCh] @MeasurementID 	
				END
				IF ((SELECT COUNT(*) FROM @TEMP WHERE MetricSumID IN (10,11,12,13,38,39,40,41)) > 0) ----For 'G7 Gray/Black ΔL Avg/Max weighted and non weighted'
				BEGIN
					EXEC [CPSyncV].[uspInsertColorCharΔL] @MeasurementID 	
				END
				IF ((SELECT COUNT(*) FROM @TEMP WHERE MetricSumID IN (48)) > 0) ----For 'Fogra Gray ΔCh Avg '
				BEGIN
					EXEC [CPSyncV].[uspInsertColorCharΔCh_Fogra] @MeasurementID 	
				END

			-- Start looping through the records
			WHILE EXISTS (SELECT * FROM @TEMP)
			BEGIN
				-- Grab the first record out
				SELECT Top 1 @MetricSumID = MetricSumID, @dEMethod = dEMethod, @TargetValue = TargetValue FROM @TEMP
				

				IF  (@MetricSumID = 1) ---For 'ΔE Average'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
								ELSE AVG(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSyncV.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 2)  ----For 'ΔE Maximum'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSyncV.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 3)  ----For 'Paper ΔE Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
																	ELSE AVG(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 4)  ----For 'CMYK ΔE Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4))
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 5)  ----For 'CMYK ΔH Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 6)  ----For 'FOGRA Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (52,53,54,55,56,57))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 7)  ----For 'CMYKRGB ΔH Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4,5,6,7))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 8)  ----For 'G7 Gray ΔCh Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 9)  ----For 'G7 Gray ΔCh Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 10)  ----For 'G7 Black ΔL Avg'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 11)  ----For 'G7 Black ΔL Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 12)  ----For 'G7 CMY / Gray ΔL Avg'
					BEGIN	
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 13)  ----For 'G7 CMY / Gray ΔL Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 14)  ----For 'RGB ΔE Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (5,6,7))
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 15)  ----For 'CMY ΔTVI25'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136)		--25% C, M, Y Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 16)  ----For 'CMY ΔTVI50'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139)		--50% C, M, Y Patches	
								) AS V), 0									
					END
				ELSE IF (@MetricSumID = 17)  ----For 'CMY ΔTVI75'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142)		--75% C, M, Y Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 18)  ----For 'TVISpread25'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT 
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
									) ),0))
							AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 19)  ----For 'TVISpread50'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT   
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches	
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches	
									) ),0))
							AS MeasuredValue, 0								
					END
				ELSE IF (@MetricSumID = 20)  ----For 'TVISpread75'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT 
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches			
									) ),0))
							AS MeasuredValue, 0								
					END
				ELSE IF (@MetricSumID = 21)   ----For 'Paper ΔL' (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN	
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_L - TargetL),2))),2)),0)
								FROM CPSyncV.vwGetVUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 22)  ----For 'Paper Δa'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_a - Targeta),2))),2)),0)
								FROM CPSyncV.vwGetVUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 23)  ----For 'Paper Δb'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_b - Targetb),2))),2)),0)
								FROM CPSyncV.vwGetVUniquePatches 
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 24)  ----For 'ΔE CMY100 Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 9)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 25)  ----For 'Japan Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,61,62,63,64,65))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 26)  ----For 'G7 Gray ΔH Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 27)  ----For 'ΔE Maximum excluding Paper'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0	
							FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID != 8
					END
				ELSE IF (@MetricSumID = 28)  ----For 'ΔTVI20'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 29)  ----For 'ΔTVI40'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138)
								) AS V), 0									
					END
				ELSE IF (@MetricSumID = 30)  ----For 'ΔTVI60'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 31)  ----For 'ΔTVI80'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143)
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 32)  ----For 'TVISpread20'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT 
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
									) ),0))
							AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 33)  ----For 'TVISpread40'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT   
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches
									) ),0))
							AS MeasuredValue, 0								
					END
				ELSE IF (@MetricSumID = 34)  ----For 'TVISpread60'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT 
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches			
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches			
									) ),0))
							AS MeasuredValue, 0								
					END
				ELSE IF (@MetricSumID = 35)  ----For 'TVISpread80'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT 
								ISNULL(ABS (
								(
									SELECT MAX(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches			
									) -	
									(
										SELECT MIN(ColorCharValue) As TVI	
										FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
										INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
										INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
										WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
									) ),0))
							AS MeasuredValue, 0								
					END
				ELSE IF (@MetricSumID = 36)  ----For 'G7 Gray wΔCh Avg'
					BEGIN	
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 37)  ----For 'G7 Gray wΔCh Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 38)  ----For 'G7 Black wΔL Avg'
					BEGIN		
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 39)  ----For 'G7 Black wΔL Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 40)  ----For 'G7 CMY / Gray wΔL Avg'
					BEGIN	
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 41)  ----For 'G7 CMY / Gray wΔL Max'
					BEGIN					
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 43)  ----For 'CMY ΔE Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3))
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 44)  ----For 'K ΔE Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (4))
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 45)  ----For 'K ΔTVI25'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (25,147)		--25% K Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 46)  ----For 'K ΔTVI50'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (26,150)		--50% K Patches	
								) AS V), 0									
					END
				ELSE IF (@MetricSumID = 47)  ----For 'K ΔTVI75'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
								(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSyncV.Measurement AS M 										
																		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = M.ProjectID
																		INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
																		INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
																		INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
																		INNER JOIN CPSyncV.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
																		AND PTTVI.PatchTypeID = RC.PatchTypeID
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (27,153)		--75% K Patches	
								) AS V), 0							
					END
				ELSE IF (@MetricSumID = 48)  ----For 'Fogra Gray ΔCh Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(Avg(ColorCharValue),2)),0) 
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 49)  ----For 'RGB ΔE Avg'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
																	ELSE AVG(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (5,6,7))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 50)  ----For 'All Patches 95th Percentile'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
									WHEN @dEMethod = 'ΔE' THEN CPSyncV.GetVPercentileΔE(@MeasurementID,0.95,'E')
									ELSE CPSyncV.GetVPercentileΔE(@MeasurementID,0.95,'E2000')
								END AS MeasuredValue,0
					END
				ELSE IF (@MetricSumID = 51)  ----For 'Paper ΔE Max'
					BEGIN
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSyncV.Measurement AS M INNER JOIN 
								CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 56)  ----For 'Fogra Gray ΔCh Max'
				 BEGIN
				  INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				   SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				   (SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
					FROM
					CPSyncV.Measurement AS M INNER JOIN 
					CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
					CPSyncV.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
					WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
				   AS MeasuredValue, 0 
				 END

				ELSE IF (@MetricSumID = 42)  ----For 'Patch Tolerance'
					BEGIN					
						INSERT INTO CPSyncV.PatchMeasured (MeasurementID, PatchTargetID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, PatchTargetID, TargetValue
								, CASE 
									WHEN @dEMethod = 'ΔE' THEN (ΔE)
									ELSE (ΔE2000)
								END AS MeasuredValue, 0
							FROM CPSyncV.PatchTarget AS PT INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = PT.SampleName 
								INNER JOIN CPSyncV.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
								LEFT JOIN CPSyncV.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID AND MP.SampleName = PT.SampleName
								INNER JOIN CPSyncV.PatchTargetPreset AS PTP ON PTP.PatchTargetPresetID = PT.PatchTargetPresetID
								INNER JOIN CPSyncV.MeasurementSetting AS MST ON MST.MeasurementID = MP.MeasurementID AND MST.MetricTargetPresetID = PTP.MetricTargetPresetID 
							WHERE M.MeasurementID = @MeasurementID
						-----Set IsNOtOk value-----
						UPDATE CPSyncV.PatchMeasured 
							SET IsNotOK = CASE WHEN MeasuredValue <= TargetValue THEN 0	ELSE 1 END
							WHERE MeasurementID = @MeasurementID
						-----Add final entry into MetricMeasured table with IsNotOK value-----
						INSERT INTO CPSyncV.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue
								, CASE SUM(CONVERT(INT,CPSyncV.PatchMeasured.IsNotOK))
									WHEN 0 THEN 0
									ELSE 1
								  END AS MeasuredValue 
								, CASE SUM(CONVERT(INT,CPSyncV.PatchMeasured.IsNotOK))
								WHEN 0 THEN 0
								ELSE 1
							  END AS IsNotOK 
						FROM CPSyncV.PatchMeasured WHERE MeasurementID = @MeasurementID
					END

				DELETE FROM @TEMP WHERE MetricSumID = @MetricSumID
			END

			UPDATE CPSyncV.MetricMeasured 
				SET IsNotOK = CASE WHEN MeasuredValue <= TargetValue THEN 0	ELSE 1 END
				WHERE MeasurementID = @MeasurementID
			
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
		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in CPSyncV.CalculateMetrics measurement',@ERROR_MESSAGE,'SyncValidation'
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH



