
/****** Object:  StoredProcedure [Report].[GetSingleMeasurement]    Script Date: 4/1/2022 10:36:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Report].[GetSingleMeasurement] 
@MeasurementID   int  
,@From_ΔE  NVARCHAR(20)  
AS
BEGIN

BEGIN TRY

	--Table0: Job Related Data  
	SELECT     P.ProjectName, OPT.RunName, M.MeasurementName, M.MeasurementDate, D.DeviceName, S.ProfileName, PC.Media, PC.Colorant, PC.RipSettings, B.Location, C.CompanyName, B.BranchName, P.LicenseTypeID,
			   CASE 
					WHEN @From_ΔE = 'E2000' THEN 'E2000'
					ELSE 'E'
			   END AS DeltaEFormula
			   ,PC.TechnicianName, I.InstrumentType, MD.SerialNumber, MS.UVMode, PC.Notes
			    ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.ReferenceChartTypeID) AS ChartName
			 ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.OPTReferenceChartTypeID) AS OPTChartName
			 ,DPO.ProfilerOptionsName
	FROM         CPSync4D.Measurement AS M INNER JOIN
                      CPSync4D.OptimizeRun AS OPT ON M.OptimizeRunID = OPT.OptimizeRunID INNER JOIN
                      CPSync4D.Project AS P ON OPT.ProjectID = P.ProjectID INNER JOIN
					  CPSync4D.MeasurementSetting as MS ON MS.ProjectID = P.ProjectID INNER JOIN
					  CP.MeasurementDevice AS MD ON MD.MeasurementDeviceID = MS.MeasurementDeviceID INNER JOIN
					  CP.InstrumentType AS I ON I.InstrumentTypeID = MD.InstrumentTypeID INNER JOIN
					  CPSync4D.DeviceFamilyProfilerOption AS DPO ON DPO.DeviceFamilyProfilerOptionID = P.BaseDeviceFamilyProfilerOption INNER JOIN
					  --CPSyncV.MetricTargetPreset AS MTP ON MTP.MetricTargetPresetID = P.MetricTargetPresetID INNER JOIN   
					  CPSync4D.PrintCondition AS PC ON P.ProjectID = PC.ProjectID INNER JOIN
					  CP.PrintStandardType AS PST ON PST.PrintStandardTypeId = P.PrintStandardTypeId INNER JOIN
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
			TargetL, 
			Targeta, 
			Targetb,			
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
			WHEN PatchTypeID = 8 OR (CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Paper' 	 
			WHEN PatchTypeID = 1 OR (CMYK_C > 0 AND CMYK_C < 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Cyan'  
			WHEN PatchTypeID = 2 OR (CMYK_C = 0 AND CMYK_M > 0 AND CMYK_M < 100 AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Magenta'  
			WHEN PatchTypeID = 3 OR (CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y > 0 AND  CMYK_Y < 100 AND CMYK_K = 0) THEN 'Yellow'  
			WHEN PatchTypeID = 5 OR (CMYK_C = 0 AND CMYK_K = 0 AND CMYK_M = CMYK_Y AND CMYK_M <> 0)   THEN 'Red'  
			WHEN PatchTypeID = 6 OR (CMYK_C = CMYK_Y AND CMYK_M = 0 AND CMYK_K = 0) THEN 'Green'  			
			WHEN PatchTypeID = 7 OR (CMYK_C = CMYK_M AND CMYK_Y = 0 AND CMYK_K = 0) THEN 'Blue'  			
			ELSE 'X' 	
			END AS PatchType  ,
			RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,				
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[TargetL*] ELSE CRL.[TargetL*] END AS TargetL,
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targeta*] ELSE CRL.[Targeta*] END AS Targeta,
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targetb*] ELSE CRL.[Targetb*] END AS Targetb,			
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_L*]),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_a*]),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_b*]),2)) AS CIELAB_b		
		FROM CP.ReferenceChart AS RC
		INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
		INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
		INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
		INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID
		INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
		LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
		LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1

		WHERE  M.MeasurementID = @MeasurementID 
		GROUP BY               
			PatchTypeID
			,RC.HTMLColor	
			,RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K
			,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[TargetL*] ELSE CRL.[TargetL*] END 
			,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targeta*] ELSE CRL.[Targeta*] END
			,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targetb*] ELSE CRL.[Targetb*] END
			,ISNULL(SP.SourceProfileType,0)
	) AS GAMUT   
	WHERE PatchType <> 'X'
	ORDER BY RANK,CMYK_C,CMYK_M,CMYK_Y,CMYK_K 
			
	--TABLE 2: for the delta metrics -- summary delta  
	SELECT	
		CASE 
			WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
			ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))
		END AS Avg_ΔE,
		CASE 
			WHEN @From_ΔE = 'E2000' THEN MAX(ΔE2000)
			ELSE MAX(ΔE)
		END AS Max_ΔE, 	
		CASE 
		WHEN @From_ΔE = 'E2000' THEN 
			(SELECT TOP 1 CONVERT(decimal(5,2),ROUND(AVG(MP.ΔE2000),2))  AS MaxE
			FROM
			CPSync4D.Measurement AS M INNER JOIN 
			CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
			CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4) GROUP BY RC.PatchTypeID ORDER BY MaxE DESC)
		ELSE (SELECT TOP 1 CONVERT(decimal(5,2),ROUND(AVG(MP.ΔE),2))  AS MaxE
			FROM
			CPSync4D.Measurement AS M INNER JOIN 
			CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
			CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4) GROUP BY RC.PatchTypeID ORDER BY MaxE DESC)
		END AS PriΔE,		
		( SELECT 
		CONVERT(decimal(5,2),ROUND(MAX(
		CP.fn_CalcΔH(
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[TargetL*] ELSE CRL.[TargetL*] END,
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targeta*] ELSE CRL.[Targeta*] END,
			CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targetb*] ELSE CRL.[Targetb*] END,
			MP.[CIELAB_L*],MP.[CIELAB_a*],MP.[CIELAB_b*])),2))
		FROM
			CPSync4D.Measurement AS M INNER JOIN 
			CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 			
			CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID INNER JOIN 
			CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID INNER JOIN 
			CP.PrintStandardType AS PST ON PST.PrintStandardTypeId = P.PrintStandardTypeId INNER JOIN 
			CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName INNER JOIN 
			CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID 
			LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
			LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1			
			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4)
			GROUP BY ISNULL(SP.SourceProfileType,0)) AS PriΔH,	
		CPSync4D.GetGrayΔE(@MeasurementID,@From_ΔE) AS GrayΔE,
		CPSync4D.GetPercentileΔE(@MeasurementID,0.95,@From_ΔE) AS NinetyFiveΔE,
		'5.00' as ΔELimit

	FROM

		CPSync4D.Measurement AS M INNER JOIN 
		CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID 
		WHERE M.MeasurementID = @MeasurementID 
		GROUP BY M.MeasurementID, M.MeasurementName, M.MeasurementDate 	  
		ORDER BY M.MeasurementID;

		----------Check for Paper scalling---------
		DECLARE @EnabledPaperScaling BIT;

		SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project AS P 
			INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID 
			INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID
		WHERE M.MeasurementID = @MeasurementID

		IF (@EnabledPaperScaling = 0)	---If no Paper scaling------
		BEGIN

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
						'5.00' AS ΔELimit
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
				   FROM CPSync4D.vwGetUniquePatches
				   WHERE MeasurementID = @MeasurementID AND PatchTypeID IN (1,2,3,4,5,6,7,8,9)
				   GROUP BY
							MeasurementID
							,PatchTypeID
							,HTMLColor				
				   ORDER BY PatchTypeID
			END

			ELSE			---If Paper scaling------
			BEGIN
				DECLARE @NewPaper_White_L DECIMAL(6,2);
				DECLARE @NewPaper_White_a DECIMAL(6,2); 
				DECLARE @NewPaper_White_b DECIMAL(6,2);
				DECLARE @NewPaper_Black_L DECIMAL(6,2);
				DECLARE @NewPaper_Black_a DECIMAL(6,2); 
				DECLARE @NewPaper_Black_b DECIMAL(6,2);
				DECLARE @CurrPaper_L DECIMAL(6,2);
				DECLARE @CurrPaper_a DECIMAL(6,2); 
				DECLARE @CurrPaper_b DECIMAL(6,2);

				SELECT @NewPaper_White_L = NewPaper_White_L, @NewPaper_White_a = NewPaper_White_a, @NewPaper_White_b = NewPaper_White_b, @NewPaper_Black_L = NewPaper_Black_L, @NewPaper_Black_a = NewPaper_Black_a, @NewPaper_Black_b = NewPaper_Black_b FROM CPSync4D.Project AS P 
					INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID 
					INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID
				WHERE M.MeasurementID = @MeasurementID


				SELECT @CurrPaper_L = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[TargetL*]) ELSE AVG(CRL.[TargetL*]) END,
					   @CurrPaper_a = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targeta*]) ELSE AVG(CRL.[Targeta*]) END,
						@CurrPaper_b = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[Targetb*]) ELSE AVG(CRL.[Targetb*]) END
						FROM CP.ReferenceChart AS RC
						INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
						INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
						INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID 
						INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
						LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
						LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
						WHERE  M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8
						GROUP BY SP.SourceProfileType,RC.PatchTypeID

				--	SELECT @NewPaper_White_L,@NewPaper_White_a,@NewPaper_White_b,@CurrPaper_L,@CurrPaper_a,@CurrPaper_b;
		
				-------Storing Paper scaled L,a,b values into temp table-------------
				DECLARE @tmpReferenceLAB Table(
							ReferenceChartID int,
							SourceProfileID int,
							TargetL DECIMAL(6,2),
							Targeta DECIMAL(6,2),
							Targetb DECIMAL(6,2)
						);
				
				INSERT INTO @tmpReferenceLAB
									(
										ReferenceChartID
									   ,SourceProfileID
									   ,TargetL
									   ,Targeta
									   ,Targetb
									)
						SELECT a.ReferenceChartID
								,a.SourceProfileID
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 1, ',') AS DECIMAL(6,2)) AS TargetL
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 2, ',') AS DECIMAL(6,2)) AS Targeta
								,CAST(dbo.UFN_SEPARATES_COLUMNS(a.NewPaperLAB, 3, ',') AS DECIMAL(6,2)) AS Targetb
						FROM
							(
								SELECT
										CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.ReferenceChartID ELSE CRL.ReferenceChartID END AS ReferenceChartID
									   ,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.SourceProfileID ELSE CRL.SourceProfileID END AS SourceProfileID
									   ,CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN 
											CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, RL.[TargetL*],RL.[Targeta*],RL.[Targetb*]) 
										ELSE 
											CP.fn_CalcPaperScaleLAB(@CurrPaper_L,@CurrPaper_a,@CurrPaper_b, @NewPaper_White_L, @NewPaper_White_a, @NewPaper_White_b, @NewPaper_Black_L, @NewPaper_Black_a, @NewPaper_Black_b, CRL.[TargetL*],CRL.[Targeta*],CRL.[Targetb*]) 
										END AS NewPaperLAB
								FROM CP.ReferenceChart AS RC
								INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
								INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
								INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
								INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID
								INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
								LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
								LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
								WHERE  M.MeasurementID = @MeasurementID
							)  as a

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
						'5.00' AS ΔELimit
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
					,CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_L*]),2)) AS ActualL
					,CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_a*]),2)) AS Actuala
					,CONVERT(decimal(5,2),ROUND(AVG(MP.[CIELAB_b*]),2)) AS Actualb
				   FROM 
					  CP.ReferenceChart AS RC INNER JOIN
                         CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN
                         CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName INNER JOIN
                         CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID INNER JOIN
                         CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID INNER JOIN
                         CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeId INNER JOIN
                         CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID = PST.SourceProfileID  INNER JOIN  
						 @tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID
					 WHERE M.MeasurementID = @MeasurementID AND PatchTypeID IN (1,2,3,4,5,6,7,8,9)
				   GROUP BY
							M.MeasurementID
							,PatchTypeID
							,HTMLColor				
				   ORDER BY PatchTypeID
			END

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
