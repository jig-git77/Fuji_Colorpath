USE [ColorPathSync]
GO
/****** Object:  StoredProcedure [CPSync4DMultiColor].[GetPatchDetailsByMeasurementID]    Script Date: 9/9/2021 11:58:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSync4DMultiColor].[GetPatchDetailsByMeasurementID]
@MeasurementID   INT   
AS  
BEGIN  

DECLARE @EnabledPaperScaling BIT;

SELECT @EnabledPaperScaling = EnabledPaperScaling FROM CPSync4D.Project AS P 
			INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID 
			INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID
		WHERE M.MeasurementID = @MeasurementID

IF (@EnabledPaperScaling = 0) 
	BEGIN
		-------Dataset for getting measurement patch details-------------
		SELECT      RC.ReferenceChartID,
		RC.SampleID, 
					RC.SampleName, 
					RC.CMYK_C, 
					RC.CMYK_M, 
					RC.CMYK_Y, 
					RC.CMYK_K, 
					RC.HTMLColor, 					
					CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[TargetL*] ELSE CRL.[TargetL*] END AS TargetL,
					CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targeta*] ELSE CRL.[Targeta*] END AS Targeta,
					CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN RL.[Targetb*] ELSE CRL.[Targetb*] END AS Targetb,
					MP.[CIELAB_L*] AS CIELAB_L,
					MP.[CIELAB_a*] AS CIELAB_a,
					MP.[CIELAB_b*] AS CIELAB_b,
					MP.ΔE,
					MP.ΔE94,
					MP.ΔE2000,
					MP.Density_V,
					MP.Density_C,
					MP.Density_M,
					MP.Density_Y,
					RC.PatchTypeID,
					'0.00' AS ΔH,
					'0.00' AS ΔCh,
					'0.00' AS ΔL,
					RCT.TotalNoColor,
					RCT.IsSupportMulticolor,					
					RC.Channel_5 AS Channel5,
					RC.Channel_6 AS Channel6,
					RC.Channel_7 AS Channel7,
					RC.Channel_8 AS Channel8,
					RC.Channel_9 AS Channel9,
					ISNULL(MCC5.MultiColorChannelName,'') AS Channel5Name,
					ISNULL(MCC6.MultiColorChannelName,'') AS Channel6Name,
					ISNULL(MCC7.MultiColorChannelName,'') AS Channel7Name,
					ISNULL(MCC8.MultiColorChannelName,'') AS Channel8Name,
					ISNULL(MCC9.MultiColorChannelName,'') AS Channel9Name
						
		FROM CP.ReferenceChart AS RC
						INNER JOIN CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
						INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
						INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
						INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID
						INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
						LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
						LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
						INNER JOIN CP.ReferenceChartType AS RCT ON RC.ChartTypeID=RCT.ReferenceChartTypeID
						LEFT JOIN CP.MultiColorChannel AS MCC5 ON RCT.Channel_5 = MCC5.MultiColorChannelID
						LEFT JOIN CP.MultiColorChannel AS MCC6 ON RCT.Channel_6 = MCC6.MultiColorChannelID
						LEFT JOIN CP.MultiColorChannel AS MCC7 ON RCT.Channel_7 = MCC7.MultiColorChannelID
						LEFT JOIN CP.MultiColorChannel AS MCC8 ON RCT.Channel_8 = MCC8.MultiColorChannelID
						LEFT JOIN CP.MultiColorChannel AS MCC9 ON RCT.Channel_9 = MCC9.MultiColorChannelID

		WHERE     M.MeasurementID = @MeasurementID

		ORDER BY RC.ReferenceChartID

	END
ELSE
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

--		SELECT @NewPaper_White_L,@NewPaper_White_a,@NewPaper_White_b,@CurrPaper_L,@CurrPaper_a,@CurrPaper_b;
		
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
						INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
						INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID
						INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID 
						INNER JOIN CP.PrintStandardType AS PST ON PST.PrintStandardTypeID = P.PrintStandardTypeID
						INNER JOIN CPSync4D.SourceProfile AS SP ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
						LEFT JOIN CPSync4D.ReferenceLab AS RL ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
						LEFT JOIN CPSync4D.CustomReferenceLab AS CRL ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
						WHERE  M.MeasurementID = @MeasurementID
					)  as a

		-----------Dataset for getting measurement patch details-------------
		SELECT      RC.SampleID, 
					RC.SampleName, 
					RC.CMYK_C, 
					RC.CMYK_M, 
					RC.CMYK_Y, 
					RC.CMYK_K, 
					RC.HTMLColor, 
					RL.TargetL, 
					RL.Targeta, 
					RL.Targetb,			
					MP.[CIELAB_L*] AS CIELAB_L,
					MP.[CIELAB_a*] AS CIELAB_a,
					MP.[CIELAB_b*] AS CIELAB_b,
					MP.ΔE,
					MP.ΔE94,
					MP.ΔE2000,
					MP.Density_V,
					MP.Density_C,
					MP.Density_M,
					MP.Density_Y,
					RC.PatchTypeID,
					'0.00' AS ΔH,
					'0.00' AS ΔCh,
					'0.00' AS ΔL
					
		FROM       CP.ReferenceChart AS RC INNER JOIN
							  @tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
							  CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
							  CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
							  CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID INNER JOIN
							  CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID AND M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName

		WHERE     M.MeasurementID = @MeasurementID

		ORDER BY RC.ReferenceChartID

	END

END
























