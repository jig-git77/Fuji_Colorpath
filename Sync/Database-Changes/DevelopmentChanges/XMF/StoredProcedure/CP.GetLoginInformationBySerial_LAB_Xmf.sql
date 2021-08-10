USE [ColorPathSync_Stage]
GO
/****** Object:  StoredProcedure [CP].[GetLoginInformationBySerial_LAB_Xmf]    Script Date: 4/19/2021 5:34:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [CP].[GetLoginInformationBySerial_LAB_Xmf]
(   
	@DeviceSerialNo nvarchar(100),
	@InstrumentTypeID tinyint,
	@ProjectID int
)   
AS   	


		DECLARE @UserID INT
		DECLARE @ProfileTypeID INT
		DECLARE @ProfileType VARCHAR(50)
		DECLARE @LicenseTypeID INT
		DECLARE @OptimizationRunID INT


		DECLARE @EnabledPaperScaling BIT;
		------Pass new paper scalable L,a,b values							
		DECLARE @NewPaper_White_L DECIMAL(6,2);
		DECLARE @NewPaper_White_a DECIMAL(6,2); 
		DECLARE @NewPaper_White_b DECIMAL(6,2);
		DECLARE @NewPaper_Black_L DECIMAL(6,2);
		DECLARE @NewPaper_Black_a DECIMAL(6,2); 
		DECLARE @NewPaper_Black_b DECIMAL(6,2);
		DECLARE @CurrPaper_L DECIMAL(6,2);
		DECLARE @CurrPaper_a DECIMAL(6,2); 
		DECLARE @CurrPaper_b DECIMAL(6,2);
		
		-------Storing Paper scaled L,a,b values into temp table-------------
		DECLARE @tmpReferenceLAB Table(
			SampleName NVARCHAR(100),
			PatchTypeID TINYINT,
			ReferenceChartID int,
			SourceProfileID int,
			TargetL DECIMAL(6,2),
			Targeta DECIMAL(6,2),
			Targetb DECIMAL(6,2)
		);

	-------Storing validation project values into temp table as a dummy MeasurementPluginStatus table-------------
		DECLARE @tmpMeasurementPluginStatus Table(
			MeasurementPluginStatusID int,
			UserID int,
			ProjectID int,
			OptimizationRunID int,
			ProfileType VARCHAR(50),
			MeaDeviceSerialNo NVARCHAR(100),
			InstrumentTypeID tinyint,
			EntryDate NVARCHAR(100),
			IsActive BIT,
			LicenseTypeID tinyint
		);


			INSERT INTO @tmpMeasurementPluginStatus(MeasurementPluginStatusID,UserID,ProjectID,OptimizationRunID,ProfileType,MeaDeviceSerialNo,InstrumentTypeID,EntryDate,IsActive,LicenseTypeID)
			SELECT RANK () OVER ( 
				ORDER BY P.[ProjectID] DESC
				) MeasurementPluginStatusID
				, 0 AS UserID, P.ProjectID, 0 AS OptimizationRunID, 'Validation' AS ProfileType
				, @DeviceSerialNo AS MeaDeviceSerialNo, @InstrumentTypeID AS InstrumentTypeID, GETDATE() AS EntryDate, 1 AS IsActive, P.LicenseTypeID
			  FROM [CPSync4D].[Project] AS P
			  WHERE P.ProjectID = @ProjectID


		SELECT 
			 m.ProjectID, 
			 ISNULL(p.ProjectName,sp.ProjectName) AS ProjectName,   
			 m.ProfileType,
			 m.OptimizationRunID,
			 CASE m.ProfileType
			  WHEN 'Profiler' THEN 1 
			  WHEN 'Optimization' THEN 2
			  WHEN 'Validation' THEN 3 
			  ELSE 10  --spot project
			 END AS ProfileTypeID,
			 m.LicenseTypeID,
			 DATEDIFF(SECOND,'1970-01-01', m.EntryDate) AS EntryDate,
			 d.DeviceName,
			 pst.PrintStandard,
			 p.EnabledPaperScaling AS PaperScaling,
			 p.NewPaper_White_L, 
			 p.NewPaper_White_a, 
			 p.NewPaper_White_b, 
			 p.NewPaper_Black_L, 
			 p.NewPaper_Black_a, 
			 p.NewPaper_Black_b 
			 FROM @tmpMeasurementPluginStatus AS m 
			 LEFT JOIN CPSync4D.Project AS p
			 ON m.ProjectID=p.ProjectID AND m.ProfileType != 'Spot Project'
			 LEFT JOIN CPSpot.SpotProject AS sp
			 ON m.ProjectID=sp.SpotProjectID AND m.ProfileType='Spot Project'  
			 LEFT JOIN CPSync4D.Project AS dp
			 ON Dp.ProjectID=ISNULL(sp.ProjectID,p.ProjectID)
			 LEFT JOIN CP.Device AS d
			 ON d.DeviceID=dp.CPDeviceID  
			 LEFT JOIN CP.PrintStandardType AS pst
			 ON pst.PrintStandardTypeID = dp.PrintStandardTypeId
			WHERE m.MeaDeviceSerialNo = @DeviceSerialNo AND m.InstrumentTypeID = @InstrumentTypeID



		SELECT 
			@ProjectID = ProjectID,
			@OptimizationRunID = OptimizationRunID,
			@ProfileTypeID = CASE ProfileType
							WHEN 'Profiler' THEN 1 
							WHEN 'Optimization' THEN 2
							WHEN 'Validation' THEN 3 
							ELSE 10 --spot project
						END,
			@ProfileType = ProfileType,
			@LicenseTypeID = LicenseTypeID
		FROM @tmpMeasurementPluginStatus
		WHERE MeaDeviceSerialNo = @DeviceSerialNo AND InstrumentTypeID = @InstrumentTypeID


		IF(@ProfileTypeID <> 10)--not spot project
		BEGIN
				IF OBJECT_ID('tempdb.#Temp4D') IS NOT NULL  
				BEGIN
					DROP TABLE #Temp4D
				END	
				CREATE TABLE #Temp4D
				(
					--TempId INT IDENTITY(1,1),
					ReferenceChartID INT,
					ChartTypeID TINYINT,					
					SampleName NVARCHAR(100),
					CMYK_C DECIMAL(6,2),  
					CMYK_M DECIMAL(6,2),  
					CMYK_Y DECIMAL(6,2),  
					CMYK_K DECIMAL(6,2),  
					PatchTypeID TINYINT,
					HTMLColor CHAR(7),
					LabL DECIMAL(6,2),
					LabA DECIMAL(6,2),
					LabB DECIMAL(6,2)
				)

			SELECT
				RCT.ReferenceChartTypeID,
				RCT.ChartName,
				RCT.NumOfRows,
				RCT.NumOfCols,
				(CAST (RCT.NumOfRows As Decimal(9,2)) * RCT.NumOfCols) As TotalPatches,
				MD.InstrumentTypeID,
				IsNull(MD.SerialNumber,'') as MeasureDeviceSrNo,
				IL.IlluminantType,
				OT.ObserverType,
				CASE @ProfileTypeID
					WHEN 3 THEN	MS.VUVMode				-- Verification
					ELSE MS.UVMode
				END AS UVMode,
				CASE @ProfileTypeID
					WHEN 3 THEN	MS.VReadMode			-- Verification
					ELSE MS.ReadMode
				END AS ReadMode,		
				CASE @ProfileTypeID
					WHEN 3 THEN	MS.VMeasurementCount	-- Verification
					ELSE MS.MeasurementCount
				END AS MeasurementCount,
				P.DensityType
				
			FROM   
				CP.ReferenceChartType AS RCT INNER JOIN CPSync4D.MeasurementSetting MS
					ON RCT.ReferenceChartTypeID = 
						CASE @ProfileTypeID
							WHEN 1 THEN MS.ReferenceChartTypeID -- Profile Chart
							WHEN 2 THEN MS.OPTReferenceChartTypeID -- Optimization Chart
							WHEN 3 THEN MS.VReferenceChartTypeID -- Verification Chart
						END
				INNER JOIN CP.MeasurementDevice MD
					ON MD.MeasurementDeviceID = 
						CASE @ProfileTypeID
							WHEN 1 THEN MS.MeasurementDeviceID -- Profile Chart
							WHEN 2 THEN MS.MeasurementDeviceID -- Optimization Chart
							WHEN 3 THEN MS.VMeasurementDeviceID -- Verification Chart
						END
				INNER JOIN CP.Illuminant IL
					ON MS.Illuminant = IL.IlluminantID
				INNER JOIN CP.ObserverType OT
					ON MS.ObserverType = OT.ObserverTypeID
				INNER JOIN CPSync4D.Project P
					ON MS.ProjectID = P.ProjectID
			WHERE  
				MS.ProjectID = @ProjectID

		INSERT INTO #Temp4D(ReferenceChart.ReferenceChartID,ChartTypeID,SampleName,CMYK_C,CMYK_M,CMYK_Y,CMYK_K,PatchTypeID,HTMLColor)
			SELECT  
			    RC.ReferenceChartID,
				RC.ChartTypeID,  
				RC.SampleName,  
				RC.CMYK_C,  
				RC.CMYK_M,  
				RC.CMYK_Y,  
				RC.CMYK_K,  
				RC.PatchTypeID,
				RC.HTMLColor
			FROM  
				CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting MS  
				on RC.ChartTypeID = 
					CASE @ProfileTypeID
							WHEN 1 THEN MS.ReferenceChartTypeID -- Profile Chart
							WHEN 2 THEN MS.OPTReferenceChartTypeID -- Optimization Chart
							WHEN 3 THEN MS.VReferenceChartTypeID -- Verification Chart
						END
			WHERE  
				MS.ProjectID = @ProjectID
			ORDER BY  
				RC.ReferenceChartID

		END
		ELSE  ---- SPOT Project
		BEGIN
				SELECT 0 AS ReferenceChartTypeID
					,RCT.ChartName
					--,RCT.NumOfRows
					,RCT.TotalRowsInChart AS NumOfRows
					,RCT.NumOfCols
					,(CAST(RCT.TotalRowsInChart AS DECIMAL(9, 2)) * RCT.NumOfCols) AS TotalPatches
					,MD.InstrumentTypeID
					,IsNull(MD.SerialNumber, '') AS MeasureDeviceSrNo
					,IL.IlluminantType
					,OT.ObserverType
					,MS.UVMode
					,MS.ReadMode
					,MS.MeasurementCount
					,PP.DensityType
							FROM CPSpot.SpotProjectChartSetting AS RCT
							INNER JOIN CPSpot.MeasurementSetting MS ON RCT.SpotProjectID = MS.SpotProjectID
							INNER JOIN CP.MeasurementDevice MD ON MD.MeasurementDeviceID = MS.MeasurementDeviceID
							INNER JOIN CP.Illuminant IL ON MS.Illuminant = IL.IlluminantID
							INNER JOIN CP.ObserverType OT ON MS.ObserverType = OT.ObserverTypeID
							INNER JOIN CPSpot.SpotProject P ON MS.SpotProjectID = P.SpotProjectID
							INNER JOIN CPSync4D.Project PP ON PP.ProjectID = P.ProjectID
							WHERE RCT.SpotProjectID = @ProjectID
				
						
					DECLARE @ReTargetSpotProjectID INT
					SELECT @ReTargetSpotProjectID = ReTargetSpotProjectID		
					FROM [CPSpot].[SpotProject] WHERE SpotProjectID = @ProjectID

					IF ISNULL(@ReTargetSpotProjectID,0) = 0	---If this Spot project is not custom Re target spot project
					BEGIN				
							SELECT STLab.SpotName AS SampleName
								,STLab.HTMLColor,STLab.[Target_L*] AS LabL,STLab.[Target_a*] AS LabA,STLab.[Target_b*] AS LabB
							FROM [CPSpot].[SpotProject] SP
							INNER JOIN [CPSpot].[SpotTargetLAB] STLab ON STLab.SpotTargetID = SP.SpotTargetID
							WHERE SP.SpotProjectID = @ProjectID order by STLab.Spotid
					END
					ELSE---If this Spot project is custom Re target spot project
					BEGIN				
							SELECT SPLab.SpotName AS SampleName
								,SPLab.Predicted_HTMLColor AS HTMLColor,SPLab.[Target_L*] AS LabL,SPLab.[Target_a*] AS LabA,SPLab.[Target_b*] AS LabB
							FROM [CPSpot].[SpotProject] SP
							INNER JOIN [CPSpot].[SpotProjectLAB] SPLab ON SPLab.SpotProjectID = SP.ReTargetSpotProjectID
							WHERE SP.SpotProjectID = @ProjectID order by SPLab.Spotid
					END
		END

		IF (@LicenseTypeID = 1)-- 1d project To get value for LAB and result merge with table.
		BEGIN
				SELECT TP.[TargetL*] AS LabL,TP.[Targeta*] AS LabA,TP.[Targetb*] AS LabB,TP.PatchTypeID INTO #Temp1D  FROM CPSync4D.Project AS P 
						LEFT JOIN CPSync.ProjectTargetLAB AS TP
						ON P.ProjectID =TP.ProjectID	
						WHERE P.ProjectID=@ProjectID
						
						UPDATE tmp4d
						SET 
							tmp4d.LabL=tmp1D.LabL,
							tmp4d.LabA=tmp1D.LabA,
							tmp4d.LabB=tmp1D.LabB
						FROM #Temp4D AS tmp4D JOIN #Temp1D AS tmp1D
						ON tmp4D.PatchTypeID=tmp1D.PatchTypeID 

						SELECT * FROM #Temp4D order by ReferenceChartID
						DROP TABLE #Temp1D						
						DROP TABLE #Temp4D
		END
		ELSE IF (@LicenseTypeID = 2 OR @LicenseTypeID =3 OR @LicenseTypeID = 5 OR @LicenseTypeID = 6  OR @LicenseTypeID = 7  OR @LicenseTypeID = 12) -- 4D project To get value for LAB and merge with table.--Added MultiColor
		BEGIN
		
					
					SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project 
					WHERE ProjectID = @ProjectID

					IF(@EnabledPaperScaling = 0)
					BEGIN
							SELECT RC.ChartTypeID,RC.SampleID,RC.SampleName,RC.PatchTypeID,
							RL.[TargetL*] AS LabL,
							RL.[Targeta*] AS LabA,RL.[Targetb*] AS LabB
							INTO #Temp
							FROM CP.ReferenceChart AS RC
							 INNER JOIN CPSync4D.MeasurementSetting AS MS ON RC.ChartTypeID =  CASE @ProfileType
																								  WHEN 'Profiler' THEN MS.ReferenceChartTypeID 
																								  WHEN 'Optimization' THEN MS.OPTReferenceChartTypeID
																								  WHEN 'Validation' THEN MS.VReferenceChartTypeID
																								END
							 INNER JOIN CPSync4D.Project AS P ON P.ProjectID = MS.ProjectID 
							 INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
							 INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
							 INNER JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID 
							WHERE  P.ProjectID = @ProjectID	

						UPDATE tmp4d
								SET 
									tmp4d.LabL=tmp.LabL,
									tmp4d.LabA=tmp.LabA,
									tmp4d.LabB=tmp.LabB
							FROM #Temp4D AS tmp4D JOIN #Temp AS tmp
							ON tmp4D.SampleName=tmp.SampleName  AND tmp4D.PatchTypeID=tmp.PatchTypeID

							DROP TABLE #Temp

					END
					ELSE
					BEGIN
							

							SELECT @NewPaper_White_L = NewPaper_White_L, @NewPaper_White_a = NewPaper_White_a, @NewPaper_White_b = NewPaper_White_b, @NewPaper_Black_L = NewPaper_Black_L, 
								   @NewPaper_Black_a = NewPaper_Black_a, @NewPaper_Black_b = NewPaper_Black_b FROM CPSync4D.Project
							WHERE ProjectID = @ProjectID


							SELECT 
									@CurrPaper_L = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[TargetL*]) ELSE AVG(CRL.[TargetL*]) END,
									@CurrPaper_a = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targeta*]) ELSE AVG(CRL.[Targeta*]) END,
									@CurrPaper_b = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targetb*]) ELSE AVG(CRL.[Targetb*]) END
							FROM CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting AS MS
									ON RC.ChartTypeID = 
														CASE 
														WHEN @ProfileTypeID =1 THEN --'Profiler'
															MS.ReferenceChartTypeID
														WHEN @ProfileTypeID =2 THEN --Optimization
															MS.OPTReferenceChartTypeID
														WHEN @ProfileTypeID =3 THEN --Validation
															MS.VReferenceChartTypeID
														ELSE RC.ChartTypeID
														END
											INNER JOIN CPSync4D.Project AS P ON P.ProjectID = MS.ProjectID 
											INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
											INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
											LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID  AND  
																			RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
											LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND 
																			CRL.ReferenceChartID = RC.ReferenceChartID 	AND ISNULL(SP.SourceProfileType,0)=1
									WHERE  P.ProjectID = @ProjectID AND RC.PatchTypeID = 8
									GROUP BY SP.SourceProfileType,RC.PatchTypeID


					
				
							INSERT INTO @tmpReferenceLAB
									(
										SampleName
									   ,PatchTypeID
									   ,ReferenceChartID
									   ,SourceProfileID
									   ,TargetL
									   ,Targeta
									   ,Targetb
									)
									SELECT 
											a.SampleName,
											a.PatchTypeID,
											a.ReferenceChartID
											,a.SourceProfileID
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 1, ',') AS DECIMAL(6,2)) AS TargetL
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 2, ',') AS DECIMAL(6,2)) AS Targeta
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 3, ',') AS DECIMAL(6,2)) AS Targetb
									FROM
										(
											SELECT
													RC.SampleName,RC.PatchTypeID,
													CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.ReferenceChartID ELSE CRL.ReferenceChartID END AS ReferenceChartID
												   ,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.SourceProfileID ELSE CRL.SourceProfileID END AS SourceProfileID
												   ,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN 
														CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, RL.[TargetL*],RL.[Targeta*],RL.[Targetb*]) 
													ELSE 
														CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, CRL.[TargetL*],CRL.[Targeta*],CRL.[Targetb*]) 
													END AS NewPaperLAB
											FROM CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting AS MS
														ON RC.ChartTypeID = 
																			CASE 
																			WHEN @ProfileTypeID =1 THEN --'Profiler'
																				MS.ReferenceChartTypeID
																			WHEN @ProfileTypeID =2 THEN --Optimization
																				MS.OPTReferenceChartTypeID
																			WHEN @ProfileTypeID =3 THEN --Validation
																				MS.VReferenceChartTypeID
																			ELSE RC.ChartTypeID
																			END											
											INNER JOIN CPSync4D.Project AS P ON P.ProjectID = MS.ProjectID 
											INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID
											INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
											LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
											LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
											WHERE  P.ProjectID = @ProjectID 
										)  AS a

									UPDATE tmp4d
										SET 
												tmp4d.LabL=tmp.TargetL,
												tmp4d.LabA=tmp.TargetA,
												tmp4d.LabB=tmp.TargetB
										FROM #Temp4D AS tmp4D JOIN @tmpReferenceLAB AS tmp
											ON tmp4D.SampleName=tmp.SampleName  AND tmp4D.PatchTypeID=tmp.PatchTypeID
					END
						SELECT * FROM #Temp4D order by ReferenceChartID							
						DROP TABLE #Temp4D
		END
		ELSE IF(@LicenseTypeID = 11) --DLPPLUS
		BEGIN
					
					SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project 
					WHERE ProjectID = @ProjectID
					PRINT @EnabledPaperScaling
					IF(@EnabledPaperScaling = 0)
					BEGIN
						SELECT TP.[TargetL*] AS LabL,TP.[Targeta*] AS LabA,TP.[Targetb*] AS LabB,TP.PatchTypeID INTO #TempDLPPLus  FROM CPSync4D.Project AS P 
						LEFT JOIN CPSync4DPlus.ProjectTargetLAB AS TP
						ON P.ProjectID =TP.ProjectID	
						WHERE P.ProjectID=@ProjectID
						
						UPDATE tmpDLPPlus
						SET 
							tmpDLPPlus.LabL=tmp1D.LabL,
							tmpDLPPlus.LabA=tmp1D.LabA,
							tmpDLPPlus.LabB=tmp1D.LabB
						FROM #Temp4D AS tmpDLPPlus JOIN #TempDLPPLus AS tmp1D
						ON tmpDLPPlus.PatchTypeID=tmp1D.PatchTypeID 

						SELECT * FROM #Temp4D order by ReferenceChartID
						DROP TABLE #Temp4D						
						DROP TABLE #TempDLPPLus

					END
					ELSE
					BEGIN
							SELECT @NewPaper_White_L = NewPaper_White_L, @NewPaper_White_a = NewPaper_White_a, @NewPaper_White_b = NewPaper_White_b, @NewPaper_Black_L = NewPaper_Black_L, 
								   @NewPaper_Black_a = NewPaper_Black_a, @NewPaper_Black_b = NewPaper_Black_b FROM CPSync4D.Project
							WHERE ProjectID = @ProjectID


							SELECT 
									@CurrPaper_L = SP.[TargetL*],
									@CurrPaper_a = SP.[Targeta*],
									@CurrPaper_b = SP.[Targetb*]
							FROM CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting AS MS
									ON RC.ChartTypeID = 
														CASE 
														WHEN @ProfileTypeID =1 THEN --'Profiler'
															MS.ReferenceChartTypeID
														WHEN @ProfileTypeID =2 THEN --Optimization
															MS.OPTReferenceChartTypeID
														WHEN @ProfileTypeID =3 THEN --Validation
															MS.VReferenceChartTypeID
														ELSE RC.ChartTypeID
														END
											INNER JOIN CPSync4D.Project AS P ON P.ProjectID = MS.ProjectID 											
											INNER JOIN CPSync4DPlus.ProjectTargetLAB AS SP ON SP.ProjectID = P.ProjectID
											
									WHERE  P.ProjectID = @ProjectID AND RC.PatchTypeID = 8
									


					
				
							INSERT INTO @tmpReferenceLAB
									(
										SampleName
									   ,PatchTypeID
									   ,ReferenceChartID
									   ,SourceProfileID
									   ,TargetL
									   ,Targeta
									   ,Targetb
									)
									SELECT 
											a.SampleName,
											a.PatchTypeID,
											a.ReferenceChartID
											,a.SourceProfileID
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 1, ',') AS DECIMAL(6,2)) AS TargetL
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 2, ',') AS DECIMAL(6,2)) AS Targeta
											,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 3, ',') AS DECIMAL(6,2)) AS Targetb
									FROM
										(
											SELECT
													RC.SampleName,RC.PatchTypeID,
													RC.ReferenceChartID  AS ReferenceChartID
												   ,0 AS SourceProfileID												   
												   , CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, SP.[TargetL*],SP.[Targeta*],SP.[Targetb*])  AS NewPaperLAB
											FROM CP.ReferenceChart AS RC INNER JOIN CPSync4D.MeasurementSetting AS MS
														ON RC.ChartTypeID = 
																			CASE 
																			WHEN @ProfileTypeID =1 THEN --'Profiler'
																				MS.ReferenceChartTypeID
																			WHEN @ProfileTypeID =2 THEN --Optimization
																				MS.OPTReferenceChartTypeID
																			WHEN @ProfileTypeID =3 THEN --Validation
																				MS.VReferenceChartTypeID
																			ELSE RC.ChartTypeID
																			END											
											INNER JOIN CPSync4D.Project AS P ON P.ProjectID = MS.ProjectID 																					
											INNER JOIN CPSync4DPlus.ProjectTargetLAB AS SP ON SP.ProjectID = P.ProjectID
											WHERE  P.ProjectID = @ProjectID 
										)  AS a

									UPDATE tmp4d
										SET 
												tmp4d.LabL=tmp.TargetL,
												tmp4d.LabA=tmp.TargetA,
												tmp4d.LabB=tmp.TargetB
										FROM #Temp4D AS tmp4D JOIN @tmpReferenceLAB AS tmp
											ON tmp4D.SampleName=tmp.SampleName  AND tmp4D.PatchTypeID=tmp.PatchTypeID

					SELECT * FROM #Temp4D order by ReferenceChartID							
					DROP TABLE #Temp4D
					END
					
		END

		--------Send Run Name and status of OptimizeRun if it is open, close or certify----------
		
		IF (@ProfileTypeID != 3) ----If run is not in validation mode right now
		BEGIN
			IF (@LicenseTypeID = 1)		   ----Sync1D project
			BEGIN
				SELECT [RunName] AS RunName, [Status] AS RunStatus FROM [CPSync].[OptimizeRun] WHERE [OptimizeRunID] = @OptimizationRunID
			END
			ELSE IF(@LicenseTypeID = 10)   ---Spot project
			BEGIN
				SELECT [RunName] AS RunName, [Status] AS RunStatus FROM [CPSpot].[OptimizeRun] WHERE [OptimizeRunID] = @OptimizationRunID
			END
			ELSE						   ---Sync4D and all other projects
			BEGIN
				SELECT [RunName] AS RunName, [Status] AS RunStatus FROM [CPSync4D].[OptimizeRun] WHERE [OptimizeRunID] = @OptimizationRunID
			END
		END
		ELSE
		BEGIN
				SELECT 'None'  AS RunName, 0 AS RunStatus   ----Validation
		END
		



