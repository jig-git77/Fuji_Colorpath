
GO
/****** Object:  StoredProcedure [CPSync4D].[GetChartData_Gray]    Script Date: 7/8/2021 10:22:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dhruti Patel
-- =============================================
ALTER PROCEDURE [CPSync4D].[GetChartData_Gray] 
@MeasurementID   int   
AS  
BEGIN  

BEGIN TRY

DECLARE @EnabledPaperScaling BIT, @LicenseTypeID TINYINT;

SELECT @EnabledPaperScaling = EnabledPaperScaling, @LicenseTypeID=P.LicenseTypeID FROM CPSync4D.Project AS P 
			INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID 
			INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID
		WHERE M.MeasurementID = @MeasurementID

IF (@EnabledPaperScaling = 0) ---If no Paper scaling------
	BEGIN
	-- L*a*b* values Spider Plot (C, M, Y, R,G,B and Paper)
	--C,M,Y,R,G,B and Paper
	SELECT  	     
		HTMLColor, 
		CASE PatchTypeID
				 WHEN 1 THEN 'Cyan'
				 WHEN 2 THEN 'Magenta'
				 WHEN 3 THEN 'Yellow'
				 WHEN 5 THEN 'Red'
				 WHEN 6 THEN 'Green'
				 WHEN 7 THEN 'Blue'
				 WHEN 8 THEN 'Paper'			
			END As PatchType,		
		CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))AS ΔE,	
		CONVERT(decimal(5,2),ROUND(AVG(TargetL),2))AS TargetL, 
		CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta, 
		CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb,			
		CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L,
		CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a,
		CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b,
	    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L - TargetL),2)) AS ΔL,
	    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a - Targeta),2)) AS Δa,
	    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b - Targetb),2)) AS Δb			
	FROM  CPSync4D.vwGetUniquePatches
	WHERE MeasurementID = @MeasurementID AND PatchTypeID >= 1 AND PatchTypeID <= 8 AND PatchTypeID != 4
	GROUP BY
		MeasurementID
		,PatchTypeID
		,HTMLColor	
	ORDER BY PatchTypeID
 

	--Other values for Spider Plot
	--Cyan Screen
	SELECT  RC.HTMLColor,'Cyan' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE
		(@LicenseTypeID != 12 AND RC.CMYK_C > 0 AND RC.CMYK_C < 100 AND RC.CMYK_M = 0 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND RC.CMYK_C > 0 AND RC.CMYK_C < 100 AND RC.CMYK_M = 0 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	UNION ALL
	--Magenta Screen
	SELECT 	RC.HTMLColor,'Magenta' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE      
		(@LicenseTypeID != 12 AND RC.CMYK_C = 0 AND  RC.CMYK_M > 0 AND RC.CMYK_M < 100 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND RC.CMYK_C = 0 AND  RC.CMYK_M > 0 AND RC.CMYK_M < 100 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	UNION ALL
	--Yellow Screen
	SELECT 	RC.HTMLColor,'Yellow' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE    
		(@LicenseTypeID != 12 AND  RC.CMYK_C = 0 AND  RC.CMYK_M = 0  AND RC.CMYK_Y > 0 AND  RC.CMYK_Y < 100 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND  RC.CMYK_C = 0 AND  RC.CMYK_M = 0  AND RC.CMYK_Y > 0 AND  RC.CMYK_Y < 100 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	UNION ALL
	--Red
	SELECT 	RC.HTMLColor,'Red' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE    
		(@LicenseTypeID != 12 AND RC.CMYK_Y = RC.CMYK_M AND RC.CMYK_C = 0 AND RC.CMYK_K = 0  AND RC.CMYK_M <> 0 AND RC.PatchTypeID NOT IN (5,8) AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND RC.CMYK_Y = RC.CMYK_M AND RC.CMYK_C = 0 AND RC.CMYK_K = 0  AND RC.CMYK_M <> 0 AND RC.PatchTypeID NOT IN (5,8) AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	UNION ALL
	--Green 
	SELECT 	RC.HTMLColor,'Green' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE    
		(@LicenseTypeID != 12 AND RC.CMYK_C = RC.CMYK_Y AND RC.CMYK_M = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (6,8) AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND RC.CMYK_C = RC.CMYK_Y AND RC.CMYK_M = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (6,8) AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	UNION ALL			
	--Blue
	SELECT 	RC.HTMLColor,'Blue' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
				MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
	FROM       CP.ReferenceChart AS RC INNER JOIN						 
						  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
						  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
	WHERE  
		(@LicenseTypeID != 12 AND  RC.CMYK_C = RC.CMYK_M AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (7,8) AND M.MeasurementID = @MeasurementID)
		OR	------For MultiColor Projects
		(@LicenseTypeID = 12 AND  RC.CMYK_C = RC.CMYK_M AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (7,8) AND M.MeasurementID = @MeasurementID
			AND (ISNULL(RC.Channel_5,0) = 0 AND ISNULL(RC.Channel_6,0) = 0 AND ISNULL(RC.Channel_7,0) = 0 AND ISNULL(RC.Channel_8,0) = 0 AND ISNULL(RC.Channel_9,0) = 0))

	ORDER BY PatchType,RC.CMYK_C,RC.CMYK_M,RC.CMYK_Y,RC.CMYK_K 

	-- Gray L*a*b*  (For all patches of Gray and Paper)
	SELECT		
			CMYK_C, 		
			CMYK_M, 			
			CMYK_Y, 			
			CMYK_K,
			TargetL, 
			Targeta, 
			Targetb,				
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b, 												
			CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) AS ΔE,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L - TargetL),2)) As ΔL,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a - Targeta),2)) AS Δa,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b - Targetb),2)) AS Δb, 
			PatchTypeID	
		FROM  CPSync4D.vwGetUniquePatches
		WHERE MeasurementID = @MeasurementID AND PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)
		GROUP BY
				PatchTypeID,
				CMYK_C, 		
				CMYK_M, 			
				CMYK_Y, 			
				CMYK_K,
				TargetL, 
				Targeta, 
				Targetb	
		ORDER BY CMYK_C	
	END

ELSE		---If Paper scaling------
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


		SELECT  @CurrPaper_L = CASE ISNULL(SP.SourceProfileType,0) WHEN 0 THEN AVG(RL.[TargetL*]) ELSE AVG(CRL.[TargetL*]) END,
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
		-- L*a*b* values Spider Plot (C, M, Y, R,G,B and Paper)
		--C,M,Y,R,G,B and Paper
		SELECT  	     
			HTMLColor, 
			CASE PatchTypeID
					 WHEN 1 THEN 'Cyan'
					 WHEN 2 THEN 'Magenta'
					 WHEN 3 THEN 'Yellow'
					 WHEN 5 THEN 'Red'
					 WHEN 6 THEN 'Green'
					 WHEN 7 THEN 'Blue'
					 WHEN 8 THEN 'Paper'			
				END As PatchType,		
			CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))AS ΔE,	
			CONVERT(decimal(5,2),ROUND(AVG(TargetL),2))AS TargetL, 
			CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta, 
			CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb,			
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*]),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*]),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*]),2)) AS CIELAB_b,
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*] - TargetL),2)) AS ΔL,
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*] - Targeta),2)) AS Δa,
			CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*] - Targetb),2)) AS Δb			
		FROM   CP.ReferenceChart AS RC INNER JOIN
							  @tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
							  CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
							  CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
							  CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID INNER JOIN
							  CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID AND M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName

		WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID >= 1 AND RC.PatchTypeID <= 8 AND RC.PatchTypeID != 4
		GROUP BY
			M.MeasurementID
			,RC.PatchTypeID
			,HTMLColor	
		ORDER BY RC.PatchTypeID
	 

		--Other values for Spider Plot
		--Cyan Screen
		SELECT  RC.HTMLColor,'Cyan' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE      RC.CMYK_C > 0 AND RC.CMYK_C < 100  AND  RC.CMYK_M = 0 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID

		UNION ALL
		--Magenta Screen
		SELECT 	RC.HTMLColor,'Magenta' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE      RC.CMYK_C =0 AND  RC.CMYK_M > 0 AND RC.CMYK_M < 100 AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID

		UNION ALL
		--Yellow Screen
		SELECT 	RC.HTMLColor,'Yellow' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE     RC.CMYK_C =0 AND  RC.CMYK_M =0  AND RC.CMYK_Y > 0 AND  RC.CMYK_Y < 100 AND RC.CMYK_K = 0 AND M.MeasurementID = @MeasurementID

		UNION ALL
		--Red
		SELECT 	RC.HTMLColor,'Red' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE    RC.CMYK_Y = RC.CMYK_M AND RC.CMYK_C = 0 AND RC.CMYK_K = 0  AND RC.CMYK_M <> 0 AND RC.PatchTypeID NOT IN (5,8) AND M.MeasurementID = @MeasurementID

		UNION ALL
		--Green 
		SELECT 	RC.HTMLColor,'Green' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE    RC.CMYK_C = RC.CMYK_Y AND RC.CMYK_M = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (6,8) AND M.MeasurementID = @MeasurementID

		UNION ALL			
		--Blue
		SELECT 	RC.HTMLColor,'Blue' As PatchType, RC.CMYK_C, RC.CMYK_M, RC.CMYK_Y, RC.CMYK_K,							
					MP.[CIELAB_L*] AS CIELAB_L, MP.[CIELAB_a*] AS CIELAB_a,	MP.[CIELAB_b*] AS CIELAB_b	
		FROM       CP.ReferenceChart AS RC INNER JOIN						 
							  CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName
		WHERE   RC.CMYK_C = RC.CMYK_M AND RC.CMYK_Y = 0 AND RC.CMYK_K = 0  AND RC.CMYK_C <> 0 AND RC.PatchTypeID NOT IN (7,8) AND M.MeasurementID = @MeasurementID
		
		ORDER BY PatchType,RC.CMYK_C,RC.CMYK_M,RC.CMYK_Y,RC.CMYK_K 

		-- Gray L*a*b*  (For all patches of Gray and Paper)
		SELECT		
				CMYK_C, 		
				CMYK_M, 			
				CMYK_Y, 			
				CMYK_K,
				TargetL, 
				Targeta, 
				Targetb,				
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*]),2)) AS CIELAB_L,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*]),2)) AS CIELAB_a,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*]),2)) AS CIELAB_b, 												
				CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) AS ΔE,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*] - TargetL),2)) As ΔL,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*] - Targeta),2)) AS Δa,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*] - Targetb),2)) AS Δb, 
				PatchTypeID	
			FROM   CP.ReferenceChart AS RC INNER JOIN
							  @tmpReferenceLAB AS RL ON RL.ReferenceChartID = RC.ReferenceChartID INNER JOIN                                            
							  CP.PrintStandardType AS PST ON PST.SourceProfileID = RL.SourceProfileID INNER JOIN
							  CPSync4D.Project AS P ON P.PrintStandardTypeId = PST.PrintStandardTypeId INNER JOIN
							  CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID INNER JOIN
							  CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID AND M.ReferenceChartTypeID = RC.ChartTypeID 
							  INNER JOIN CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName

			WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)
			GROUP BY
					RC.PatchTypeID,
					CMYK_C, 		
					CMYK_M, 			
					CMYK_Y, 			
					CMYK_K,
					TargetL, 
					Targeta, 
					Targetb	
			ORDER BY CMYK_C	
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
