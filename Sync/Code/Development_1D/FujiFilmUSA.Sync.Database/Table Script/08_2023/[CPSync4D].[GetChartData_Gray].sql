
-- =============================================
-- Author:		Dhruti Patel
-- =============================================
ALTER PROCEDURE [CPDestProfile].[GetChartData_Gray] 
@MeasurementID   int   
AS  
BEGIN  

BEGIN TRY

DECLARE @EnabledPaperScaling BIT, @LicenseTypeID TINYINT;

SELECT @EnabledPaperScaling = EnabledPaperScaling, @LicenseTypeID=P.LicenseTypeID FROM CPSync4D.Project AS P 
			INNER JOIN CPSync4D.OptimizeRun AS OTR ON OTR.ProjectID = P.ProjectID 
			INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OTR.OptimizeRunID
		WHERE M.MeasurementID = @MeasurementID

	-- for this destination profile license type , paper scaling will be always be 0
	-- L*a*b* values Spider Plot (C, M, Y, R,G,B and Paper)
	--C,M,Y,R,G,B and Paper
	SELECT		HTMLColor, 
				CASE PatchTypeID
						 WHEN 1 THEN 'Cyan'
						 WHEN 2 THEN 'Magenta'
						 WHEN 3 THEN 'Yellow'
						 WHEN 5 THEN 'Red'
						 WHEN 6 THEN 'Green'
						 WHEN 7 THEN 'Blue'
						 WHEN 8 THEN 'Paper'			
				END As PatchType,		
				'0.00' AS ΔE,	
				'0.00' AS TargetL, 
				'0.00' AS Targeta, 
				'0.00' AS Targetb,			
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*]),2)) AS CIELAB_L,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*]),2)) AS CIELAB_a,
				CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*]),2)) AS CIELAB_b,
				'0.00' AS ΔL,
				'0.00' AS Δa,
				'0.00' AS Δb			
	FROM		CP.ReferenceChart AS RC INNER JOIN
				CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN
				CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName INNER JOIN
				CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID INNER JOIN
				CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID
	WHERE 		M.MeasurementID = @MeasurementID AND PatchTypeID >= 1 AND PatchTypeID <= 8 AND PatchTypeID != 4
	GROUP BY	M.MeasurementID,
				PatchTypeID,
				HTMLColor	
	ORDER BY 	PatchTypeID

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
		SELECT		CMYK_C, 		
					CMYK_M, 			
					CMYK_Y, 			
					CMYK_K,	
					'0.00' AS TargetL, 
					'0.00' AS Targeta, 
					'0.00' AS Targetb,					
					CONVERT(decimal(5,2),ROUND(AVG([CIELAB_L*]),2)) AS CIELAB_L,
					CONVERT(decimal(5,2),ROUND(AVG([CIELAB_a*]),2)) AS CIELAB_a,
					CONVERT(decimal(5,2),ROUND(AVG([CIELAB_b*]),2)) AS CIELAB_b, 
					'0.00' AS ΔE,
					'0.00' As ΔL,
					'0.00' AS Δa,
					'0.00' AS Δb, 					
					PatchTypeID	
		FROM		CP.ReferenceChart AS RC INNER JOIN
					CPSync4D.Measurement AS M ON M.ReferenceChartTypeID = RC.ChartTypeID INNER JOIN
					CPSync4D.MeasurementPatch AS MP ON M.MeasurementID = MP.MeasurementID AND MP.SampleName = RC.SampleName INNER JOIN
					CPSync4D.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID INNER JOIN
					CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID
		WHERE		M.MeasurementID = @MeasurementID AND PatchTypeID IN (8,9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)
		GROUP BY	PatchTypeID,
					CMYK_C, 		
					CMYK_M, 			
					CMYK_Y, 			
					CMYK_K
		ORDER BY	CMYK_C	

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
