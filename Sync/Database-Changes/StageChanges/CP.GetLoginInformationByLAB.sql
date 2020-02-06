USE [ColorPathProd_Stage2]
GO
/****** Object:  StoredProcedure [CP].[GetLoginInformationByLAB]    Script Date: 2/6/2020 1:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
EXEC [CP].[GetLoginInformationByLAB]
	@DeviceSerialNo='304060',
	@InstrumentTypeID=1 

*/


ALTER PROCEDURE [CP].[GetLoginInformationByLAB]--    'psylvester','fuji'
(   
	@DeviceSerialNo nvarchar(100),
	@InstrumentTypeID tinyint
)   
AS   	


		DECLARE @UserID INT
		DECLARE @ProjectID INT
		DECLARE @ProfileTypeID INT
		DECLARE @ProfileType VARCHAR(50)
		DECLARE @LicenseTypeID INT

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
			 d.DeviceName
			 FROM [CP].[MeasurementPluginStatus] AS m 
			 LEFT JOIN CPSync4D.Project AS p
			 ON m.ProjectID=p.ProjectID AND m.ProfileType != 'Spot Project'
			 LEFT JOIN CPSpot.SpotProject AS sp
			 ON m.ProjectID=sp.SpotProjectID AND m.ProfileType='Spot Project'  
			 LEFT JOIN CPSync4D.Project AS dp
			 ON Dp.ProjectID=ISNULL(sp.ProjectID,p.ProjectID)
			 LEFT JOIN CP.Device AS d
			 ON d.DeviceID=dp.CPDeviceID  
			WHERE m.MeaDeviceSerialNo=@DeviceSerialNo AND m.InstrumentTypeID=@InstrumentTypeID



		SELECT 
			@ProjectID=ProjectID,
			@ProfileTypeID= CASE ProfileType
							WHEN 'Profiler' THEN 1 
							WHEN 'Optimization' THEN 2
							WHEN 'Validation' THEN 3 
							ELSE 10 --spot project
						END,
			@ProfileType = ProfileType
		FROM [CP].[MeasurementPluginStatus]
		WHERE MeaDeviceSerialNo=@DeviceSerialNo AND InstrumentTypeID=@InstrumentTypeID
		
		SELECT @LicenseTypeID=LicenseTypeID FROM CPSync4D.Project WHERE ProjectID=@ProjectID

		IF(@ProfileTypeID<>10)--not spot project
		BEGIN
		CREATE TABLE #Temp4D
				(
					TempId INT IDENTITY(1,1),
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

		INSERT INTO #Temp4D(ReferenceChart.ChartTypeID,SampleName,CMYK_C,CMYK_M,CMYK_Y,CMYK_K,PatchTypeID,HTMLColor)
			SELECT  
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
				ReferenceChartID

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

		
		IF (@LicenseTypeID = 1)-- 1d project
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

						SELECT * FROM #Temp4D ORDER BY PatchTypeID ASC
						DROP TABLE #Temp1D						
						DROP TABLE #Temp4D
		END
		ELSE IF (@LicenseTypeID = 2 OR @LicenseTypeID =3 OR @LicenseTypeID = 5 OR @LicenseTypeID = 6  OR @LicenseTypeID = 7 ) -- 4D project 
		BEGIN
					SELECT RC.ChartTypeID,RC.SampleName,
					RL.[TargetL*] AS LabL,
					RL.[Targeta*] AS LabA,RL.[Targetb*] AS LabB
					INTO #Temp
				FROM CP.ReferenceChart AS RC
					 INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
					 INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
					 INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
					 INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
					 INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
					 INNER JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID 
				WHERE  P.ProjectID = @ProjectID
				
				UPDATE tmp4d
				
						SET 
							tmp4d.LabL=tmp.LabL,
							tmp4d.LabA=tmp.LabA,
							tmp4d.LabB=tmp.LabB
						FROM #Temp4D AS tmp4D JOIN #Temp AS tmp
						ON tmp4D.SampleName=tmp.SampleName 

				SELECT * FROM #Temp4D ORDER BY PatchTypeID ASC
				DROP TABLE #Temp				
				DROP TABLE #Temp4D
		END
		

	
		