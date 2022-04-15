
ALTER PROCEDURE [CPSync4D].[CreateOptimizeRun]    
 @ProjectId INT,    
 @Status INT, 
 @StartDateTime DATETIME
AS    
 BEGIN TRY    
 
   --IF alreday Run "Open" so no need to create new "Open" Run.
	IF NOT EXISTS (SELECT * FROM CPSync4D.OptimizeRun WHERE [Status] = 1 AND EndDateTime IS NULL AND ProjectID = @ProjectId)
	BEGIN
			DECLARE @MasterRunID INT             
			SELECT TOP 1 @MasterRunID=OptimizeRunID FROM CPSync4D.OptimizeRun WHERE ProjectID = @ProjectID ORDER BY OptimizeRunID DESC    
  
			DECLARE @RunCount INT            
			SELECT @RunCount = COUNT(*) FROM CPSync4D.OptimizeRun WHERE ProjectID = @ProjectID            

			DECLARE @vOptimizeRunID INT,@vProfileType NVARCHAR(50)
   
			INSERT INTO CPSync4D.OptimizeRun    
			 (RunName,    
			 ProjectID,    
			 Status,    
			 StartDateTime,    
			 EndDateTime,    
			 DeviceLinkProfile,    
			 MasterRunID,    
			 IsUsed)    
			VALUES(    
			 'Run-' + CONVERT(VARCHAR(3),@RunCount),          
			 @ProjectID,            
			 @Status,        
			 @StartDateTime,            
			 NULL,    
			 NULL,    
			 @MasterRunID,    
			 1)  
			
			SET @vOptimizeRunID=SCOPE_IDENTITY();

			DECLARE @LicenseTypeID TINYINT
			SELECT @LicenseTypeID=LicenseTypeID FROM [CPSync4D].[Project] WHERE ProjectID = @ProjectId
			IF(@LicenseTypeID = 11)---IF DLPPLUS then we stroe previous run details
			BEGIN
				-----Call stored procedure to stored Previous Run data for comparison----
		     	IF @MasterRunID > 0 
				BEGIN
					EXEC [CPSync4DPlus].[StorePreviousRunDataByOptimizeRunID] @MasterRunID
				END
			END
			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE() WHERE ProjectID = @ProjectId

			--------If user is using third party app then update its run status---
			IF(@Status = 1)
			BEGIN
					IF(@RunCount = 0)
					BEGIN
						SET @vProfileType='Profiler'
					END
					ELSE IF(@RunCount >= 1)
					BEGIN
						SET @vProfileType='Optimization'
					END
					
					EXEC [CP].[UpdateMeasurementPluginStatus] @ProjectID = @ProjectID,@IsActive=1,@IsAllowDelete=0,@vProfileType=@vProfileType,@vOptimizationRunID=@vOptimizeRunID
			END
	END

 END TRY  
 BEGIN CATCH  
   DECLARE @ERROR_NUMBER INT     
   DECLARE @ERROR_SEVERITY INT    
   DECLARE @ERROR_STATE INT     
   DECLARE @ERROR_PROCEDURE NVARCHAR(252)     
   DECLARE @ERROR_LINE INT     
   DECLARE @ERROR_MESSAGE NVARCHAR(4000)     
   SELECT @ERROR_NUMBER = ERROR_NUMBER()  , @ERROR_NUMBER = ERROR_NUMBER() , @ERROR_SEVERITY = ERROR_SEVERITY() , @ERROR_STATE = ERROR_STATE() , @ERROR_PROCEDURE = ERROR_PROCEDURE() , @ERROR_LINE = ERROR_LINE() , @ERROR_MESSAGE = ERROR_MESSAGE()     
   EXEC InsertSQLErrorLog @ERROR_NUMBER , @ERROR_SEVERITY , @ERROR_STATE , @ERROR_PROCEDURE , @ERROR_LINE , @ERROR_MESSAGE     
 END CATCH



GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CPSync4D].[ImportMeasurementDetails]	
	@MeasurementName NVARCHAR(50),
	@OptimizeRunID INT,
	@MeasurementDate DATETIME,
	@ReferenceChartTypeID TINYINT,
	@MeasurementPatches XML,
	@MeasurementID INT OUTPUT,
	@PCSDataType INT = 1,	
	@LicenseType INT,
	@ProjectID INT,
	@Notes NVARCHAR(1000),
	@Customer NVARCHAR(50),
	@PageOrSurface NVARCHAR(50),
	@JDFJobId NVARCHAR(50),
	@UserID INT
AS
	BEGIN TRY

		DECLARE @TotalReferenceChartPatches INT
		DECLARE @TotalCurrentPatches INT
		
		SELECT @TotalCurrentPatches= COUNT(1) FROM
						@MeasurementPatches.nodes('/MeasurementPatches/MeasurementPatch') p([MeasurementPatchXml])


	

		IF(@LicenseType = 1 OR @LicenseType = 2 OR @LicenseType = 51 OR @LicenseType = 11 OR @LicenseType = 12 OR @LicenseType = 13)
		BEGIN
		

			SELECT @TotalReferenceChartPatches = (CAST(NumOfRows AS INT) * CAST(NumOfCols AS INT)) FROM [CP].[ReferenceChartType]
			WHERE ReferenceChartTypeID=@ReferenceChartTypeID

			PRINT @TotalReferenceChartPatches
			
			PRINT @TotalCurrentPatches
			
			IF(@TotalReferenceChartPatches = @TotalCurrentPatches)
			BEGIN
				
				IF(@LicenseType IN (1,13) ) --1D and Sync1DMultiColor
				BEGIN				
						EXEC [CPSync].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END
				
				IF(@LicenseType IN (2,12,11)) -- DLP & DLPMultiColor Channel AND DLPLUS
				BEGIN
					EXEC [CPSync4D].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END

				IF(@LicenseType = 51) -- validation
				BEGIN
					EXEC [CPSyncV].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@ProjectID  = @ProjectID,
						@MeasurementDate = @MeasurementDate,
						@ReferenceChartTypeID = @ReferenceChartTypeID,
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@Notes = @Notes,
						@Customer = @Customer,
						@PageOrSurface = @PageOrSurface,
						@JDFJobId = @JDFJobId,
						@UserID=@UserID,
						@MeasurementInfo = NULL
				END
			END
			ELSE
			BEGIN
				SET @MeasurementID = -2
				RETURN
			END

		END
		ELSE
		BEGIN

			DECLARE @SpotProjectID INT

			SELECT @SpotProjectID = SpotProjectID FROM CPSpot.OptimizeRun WHERE OptimizeRunID=@OptimizeRunID
			
			SELECT @TotalReferenceChartPatches = (CAST(TotalRowsInChart AS INT) * CAST(NumOfCols AS INT)) FROM CPSpot.SpotProjectChartSetting
			WHERE SpotProjectID=@SpotProjectID

			IF(@TotalReferenceChartPatches = @TotalCurrentPatches)
			BEGIN
				
				EXEC [CPSpot].[CreateMeasurement]
						@MeasurementName = @MeasurementName,
						@OptimizeRunID = @OptimizeRunID,
						@MeasurementDate = @MeasurementDate,						
						@MeasurementPatches = @MeasurementPatches,
						@MeasurementID = @MeasurementID OUTPUT,
						@PCSDataType = @PCSDataType,
						@UserID=@UserID,
						@MeasurementInfo = NULL				
								
			END
			ELSE
			BEGIN				
				SET @MeasurementID = -2
				RETURN
			END

		END
					
	END TRY
	BEGIN CATCH
		SET @MeasurementID = -1
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

GO
ALTER PROCEDURE [CPSync4D].[ImportMeasurements]      
 @OriginalMeasurementID  int 
,@NewOptimizeRunID  int  
,@UserID int
AS      
BEGIN      
		DECLARE @NewMeasurementID INT;
		DECLARE @ProjectID INT;
		DECLARE @LicenseTypeID TINYINT
		SELECT @ProjectID = ProjectID from CPSync4D.OptimizeRun where OptimizeRunID = @NewOptimizeRunID

		DECLARE @tmpMeasurementPatch Table(
					SampleName nvarchar(50),
					MeasurementPatchID INT,
					MeasurementID INT
				);

		INSERT INTO CPSync4D.Measurement (MeasurementName, OptimizeRunID,  MeasurementDate, ReferenceChartTypeID, IsUsed)
				 SELECT 
					  M.MeasurementName, @NewOptimizeRunID, M.MeasurementDate, M.ReferenceChartTypeID, M.IsUsed   
				  FROM    
					  CPSync4D.Measurement M   
				  WHERE    
				   M.MeasurementID =  @OriginalMeasurementID        

				  SET @NewMeasurementID = @@identity

	   INSERT INTO CPSync4D.MeasurementPatch
		 (MeasurementID, SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*], ΔE, ΔE94, ΔE2000,Density_C,Density_M,Density_Y,Density_V)
			output inserted.SampleName,INSERTED.MeasurementPatchID,INSERTED.MeasurementID into @tmpMeasurementPatch

			SELECT @NewMeasurementID,SampleName, CIEXYZ_X, CIEXYZ_Y, CIEXYZ_Z, [CIELAB_L*], [CIELAB_a*], [CIELAB_b*], ΔE, ΔE94, ΔE2000,Density_C,Density_M,Density_Y,Density_V
					
				FROM CPSync4D.MeasurementPatch WHERE MeasurementID = @OriginalMeasurementID

		INSERT INTO CPSync4D.MeasurementPatch_Spectral
		 (MeasurementPatchID, SampleName 
			       ,nm380
				   ,nm390
				   ,nm400
				   ,nm410
				   ,nm420
				   ,nm430
				   ,nm440
				   ,nm450
				   ,nm460
				   ,nm470
				   ,nm480
				   ,nm490
				   ,nm500
				   ,nm510
				   ,nm520
				   ,nm530
				   ,nm540
				   ,nm550
				   ,nm560
				   ,nm570
				   ,nm580
				   ,nm590
				   ,nm600
				   ,nm610
				   ,nm620
				   ,nm630
				   ,nm640
				   ,nm650
				   ,nm660
				   ,nm670
				   ,nm680
				   ,nm690
				   ,nm700
				   ,nm710
				   ,nm720
				   ,nm730)
			SELECT TMP.MeasurementPatchID						
				   ,TMP.SampleName	
				   ,nm380
				   ,nm390
				   ,nm400
				   ,nm410
				   ,nm420
				   ,nm430
				   ,nm440
				   ,nm450
				   ,nm460
				   ,nm470
				   ,nm480
				   ,nm490
				   ,nm500
				   ,nm510
				   ,nm520
				   ,nm530
				   ,nm540
				   ,nm550
				   ,nm560
				   ,nm570
				   ,nm580
				   ,nm590
				   ,nm600
				   ,nm610
				   ,nm620
				   ,nm630
				   ,nm640
				   ,nm650
				   ,nm660
				   ,nm670
				   ,nm680
				   ,nm690
				   ,nm700
				   ,nm710
				   ,nm720
				   ,nm730
				FROM CPSync4D.MeasurementPatch_Spectral MPS
						INNER JOIN @tmpMeasurementPatch TMP ON TMP.SampleName = MPS.SampleName 
						INNER JOIN CPSync4D.MeasurementPatch AS MP ON MP.MeasurementPatchID = MPS.MeasurementPatchID
				WHERE MP.MeasurementID = @OriginalMeasurementID


			--UPDATING ΔE,ΔE94,ΔE2000 VALUES into new Measurement since older ΔE,ΔE94,ΔE2000 VALUES calculated with different SourceProfile might not match newer other Source Profile
				UPDATE
						MP
							  SET
									MP.ΔE = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END,
									MP.ΔE94 = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE94(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE94(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END,
									MP.ΔE2000 = CASE ISNULL(SP.SourceProfileType,0) 
											WHEN 0 THEN
												CP.fn_CalcΔE2000(
												  RL.[TargetL*],
												  RL.[Targeta*],
												  RL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											ELSE
												CP.fn_CalcΔE2000(
												  CRL.[TargetL*],
												  CRL.[Targeta*],
												  CRL.[Targetb*],
												  MP.[CIELAB_L*],
												  MP.[CIELAB_a*],
												  MP.[CIELAB_b*])
											END
							  FROM
									CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch AS MP 
												ON M.MeasurementID = MP.MeasurementID AND M.MeasurementID = @NewMeasurementID 
									INNER JOIN CP.ReferenceChart AS RC
												ON RC.SampleName = MP.SampleName 
												AND RC.ChartTypeID = M.ReferenceChartTypeID
									--INNER JOIN CPSync4D.ReferenceLAB AS RL 
									--	  ON RL.ReferenceChartID = RC.ReferenceChartID
									INNER JOIN CPSync4D.OptimizeRun AS OP 
										  ON OP.OptimizeRunID = M.OptimizeRunID
									INNER JOIN CPSync4D.Project AS P 
										  ON P.ProjectID = OP.ProjectID
									INNER JOIN CP.PrintStandardType AS PST 
											ON PST.PrintStandardTypeID = P.PrintStandardTypeId
									INNER JOIN CPSync4D.SourceProfile AS SP
											ON SP.SourceProfileID = PST.SourceProfileID AND PST.SourceProfileID=PST.SourceProfileID
									LEFT JOIN CPSync4D.ReferenceLab AS RL 
											ON  RL.SourceProfileID = SP.SourceProfileID AND  RL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=0 
									LEFT JOIN CPSync4D.CustomReferenceLab AS CRL 
											ON CRL.SourceProfileID = SP.SourceProfileID AND CRL.ReferenceChartID = RC.ReferenceChartID AND ISNULL(SP.SourceProfileType,0)=1
					
			-------Update LastActiveDate for Project as Today's date-------
			UPDATE [CPSync4D].[Project] SET LastActiveDate = GETDATE()
				 WHERE ProjectID = 
						(SELECT OPT.ProjectID FROM CPSync4D.OptimizeRun AS OPT 
							INNER JOIN CPSync4D.Measurement AS M ON OPT.OptimizeRunID = M.OptimizeRunID 
														AND M.MeasurementID = @NewMeasurementID)
														
		     SELECT @LicenseTypeID=LicenseTypeID FROM CPSync4D.Project where ProjectID=@ProjectID
			 IF(@LicenseTypeID =11)---DLP PLUS
			 BEGIN
				  ---INSERT ΔCh VALUES INTO CPSync.MeasuredColorChar TABLE
					EXEC [CPSync4DPlus].[uspInsertColorCharΔCh] @NewMeasurementID,@UserID
					---INSERT ΔL VALUES INTO CPSync.MeasuredColorChar TABLE
					EXEC [CPSync4DPlus].[uspInsertColorCharΔL] @NewMeasurementID,@UserID
					--------Set Metrics----------------			
					EXEC [CPSync4DPlus].[CalculateMetrics] @NewMeasurementID,@UserID											
					-----Update IsOk entry onto CPSync.Measurement table-------
					UPDATE CPSync4D.Measurement SET IsOK = 1 WHERE MeasurementID = @NewMeasurementID
					-----Delete From dbo.MeasurementError_File table if ISOK =1 ---
					DELETE FROM dbo.MeasurementError_File WHERE OptimizationRunID=@NewOptimizeRunID AND ProjectID=@ProjectID

			 END	
END
GO
-- =============================================
-- Author:		Dhruti Patel
-- =============================================
CREATE PROCEDURE [CPSync4DPlus].[GetChartData_InkPaper] 
@MeasurementID   int   
,@From_ΔE NVARCHAR(20)
AS  
BEGIN  

-------Dataset for getting measurement patch details-------------

BEGIN TRY
	------First select Previous Run's ID for showing comparison Avg DeltaE------
	DECLARE @PreOptimizeRunID INT;
	
	SELECT @PreOptimizeRunID = [CPSync4DPlus].[GetPreviousRunID] (@MeasurementID)
	
	--ΔE 
	SELECT 
		CASE 
			WHEN VW.PatchTypeID = 8 THEN '#D4CBC5'
			ELSE HTMLColor
		END AS HTMLColor,	
		VW.PatchTypeID,
		CASE 
			WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
			ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2)) 
		END AS ΔE,
		ISNULL((CASE
					WHEN VW.PatchTypeID IN (1,2,3) THEN (SELECT TOP 1 --CMY LIMIT
													CASE 
														WHEN 43 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (43,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (4) THEN (SELECT TOP 1 --K LIMIT
													CASE 
														WHEN 44 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (44,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (5,6,7) THEN (SELECT TOP 1  --RGB LIMIT
													CASE 
														WHEN 14 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (14,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN VW.PatchTypeID IN (8) THEN (SELECT TOP 1      --PAPER LIMIT
													CASE 
														WHEN 3 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (3,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
				ELSE 0
		END),0) AS ΔELimit		
		,CONVERT(decimal(5,2),ROUND(AVG(TargetL),2)) AS TargetL, CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta
		,CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb, CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L
		,CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a, CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b
		,[AvgΔE] AS PreAvgΔE
		FROM 
			CPSync4DPlus.vwGetUniquePatches AS VW LEFT OUTER JOIN CPSync4DPlus.OptimizeRun_PreviousRunData AS PR
			ON PR.PatchTypeID = VW.PatchTypeID AND PR.OptimizeRunID = @PreOptimizeRunID 
		WHERE 
			MeasurementID = @MeasurementID AND VW.PatchTypeID >= 1 AND VW.PatchTypeID <= 9
		GROUP BY
				MeasurementID
				,VW.PatchTypeID
				,HTMLColor	
				,[AvgΔE]
		ORDER BY PatchTypeID

	--Paper
	SELECT    				
			CONVERT(decimal(5,2),ROUND(AVG(TargetL),2)) AS TargetL, 
			CONVERT(decimal(5,2),ROUND(AVG(Targeta),2)) AS Targeta, 
			CONVERT(decimal(5,2),ROUND(AVG(Targetb),2)) AS Targetb,			
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L),2)) AS CIELAB_L,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a),2)) AS CIELAB_a,
			CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b),2)) AS CIELAB_b,	
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_L - TargetL),2)) As ΔL,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_a - Targeta),2)) AS Δa,
		    CONVERT(decimal(5,2),ROUND(AVG(CIELAB_b - Targetb),2)) AS Δb
		
	FROM    CPSync4DPlus.vwGetUniquePatches
	WHERE   MeasurementID = @MeasurementID AND PatchTypeID = 8
	GROUP BY
            MeasurementID
			,PatchTypeID
	ORDER BY PatchTypeID 

	--Density  
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
	FROM    CPSync4DPlus.vwGetUniquePatches AS VW LEFT OUTER JOIN CPSync4DPlus.OptimizeRun_PreviousRunData AS PR
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

GO
GO
/****** Object:  StoredProcedure [CPSync4DPlus].[GetMetricSumTargetValues]    Script Date: 2/28/2022 9:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [CPSync4DPlus].[GetMetricSumTargetValues]
	@MetricTargetPresetId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from	
	SET NOCOUNT ON;
	DECLARE @Isweighted BIT;
	SELECT @Isweighted=ISNULL(Weighted,0) FROM  CPSyncV.MetricTargetPreset where MetricTargetPresetID =@MetricTargetPresetId

	IF(@Isweighted != 1)
	BEGIN
		SELECT 
			ms.MetricSumId
			,ms.MetricSumName
			,mt.TargetValue
			FROM CPSyncV.MetricSum ms
			INNER JOIN CPSyncV.MetricTarget mt 
				ON ms.MetricSumId = mt.MetricSumId AND mt.MetricTargetPresetId = @MetricTargetPresetId
	END
	ELSE
	BEGIN
			SELECT 
				ms.MetricSumId
				,ms.MetricSumName
				,mt.TargetValue
				FROM CPSyncV.MetricSum ms
				INNER JOIN CPSyncV.MetricTarget mt 
					ON ms.MetricSumId = mt.MetricSumId AND mt.MetricTargetPresetId = @MetricTargetPresetId
			WHERE ms.MetricSumID NOT IN (8,9,10,11,12,13) --NON weighted metric sum removed
					
	END

END
GO
CREATE PROCEDURE [CPSync4DPlus].[StorePreviousRunDataByOptimizeRunID]      
 @OptimizeRunID  int    
AS      
BEGIN   
		---Get ΔE Type based on selected MetricTargetPresetID-------
		DECLARE @From_ΔE NVARCHAR(20);

		SELECT @From_ΔE = dEMethod FROM  CPSyncV.MetricTargetPreset AS MTP
				INNER JOIN CPSync4D.Project AS P ON P.MetricTargetPresetID = MTP.MetricTargetPresetID
				INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
			WHERE OPT.OptimizeRunID = @OptimizeRunID
		
		SET @From_ΔE = substring(@From_ΔE,2,len(@From_ΔE)); ----Remove Δ sign from string and pass just E / E2000---

		---First delete older records under same OptimizeRunID------
		DELETE FROM CPSync4DPlus.OptimizeRun_PreviousRunData WHERE OptimizeRunID = @OptimizeRunID;
   
		---Add new set of records for previous run--------
		INSERT INTO CPSync4DPlus.OptimizeRun_PreviousRunData
           ([OptimizeRunID]
           ,[PatchTypeID]
           ,[AvgDensity]
           ,[MinDensity]
           ,[MaxDensity]
           ,[STDdevDensity]
           ,[AvgΔE]
           ,[MinΔE]
           ,[MaxΔE]
           ,[STDdevΔE]
           ,[IsUsed])

			SELECT @OptimizeRunID, PatchTypeID,
				CONVERT(decimal(5,2),ROUND(AVG(Density),2)) AS AvgDensity,
				CONVERT(decimal(5,2),ROUND(MIN(Density),2)) AS MinDensity,
				CONVERT(decimal(5,2),ROUND(MAX(Density),2)) AS MaxDensity,
				CONVERT(decimal(5,2),ROUND(STDEV(Density),2)) AS STDdevDensity,
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(AVG(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(AVG(ΔE),2))
				END AS [AvgΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(MIN(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(MIN(ΔE),2))
				END AS [MinΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(MAX(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(MAX(ΔE),2))
				END AS [MaxΔE],
				CASE 
					WHEN @From_ΔE = 'E2000' THEN CONVERT(decimal(5,2),ROUND(STDEV(ΔE2000),2))
					ELSE CONVERT(decimal(5,2),ROUND(STDEV(ΔE),2))
				END AS [STDdevΔE],
				1 AS IsUsed

			FROM
			(SELECT  PatchTypeID, 
				CASE   
					WHEN PatchTypeID = 1 THEN Density_C
					WHEN PatchTypeID = 2 THEN Density_M
					WHEN PatchTypeID = 3 THEN Density_Y
					WHEN PatchTypeID = 4 THEN Density_V
					ELSE  
						0 	 
				END As Density,
				ΔE,
				ΔE2000
					
				FROM    CPSync4DPlus.vwGetUniquePatches AS VW
				WHERE   OptimizeRunID = @OptimizeRunID AND PatchTypeID >= 1 AND PatchTypeID <= 9 

				) AS V
			GROUP BY V.PatchTypeID
		
END

GO

ALTER PROCEDURE [CPSyncV].[GetMetricTargetPresetList]
	-- Add the parameters for the stored procedure here
	@CurrentUserID INT
   ,@ProjectTypeID INT 
   ,@PrintingTypeID TINYINT
   ,@LicenseTypeID INT ---Addd parameter for DLPPLus Validation
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@ProjectTypeID = 1 AND @LicenseTypeID != 11) -----Sync1D G7 Projects
	BEGIN	
		 SELECT  
				MTP.MetricTargetPresetID
				,MTP.MetricTargetPresetName	
				,MTP.MetricTargetPresetDesc			
			  FROM   
					CPSyncV.MetricTargetPreset MTP 
					LEFT JOIN CP.Region R  
						ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
					LEFT JOIN CP.Company C  
						ON C.CompanyID = MTP.CompanyID  
			  WHERE  
				Validation_1D = 1 AND ValidationType = 1 AND IsUsed = 1 AND
				(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
					(CASE  
						WHEN @PrintingTypeID = 1 THEN 1
						ELSE 0
					 END
					)
				
	END
	ELSE IF(@ProjectTypeID = 2 AND @LicenseTypeID != 11) -----Sync1D ISO Projects
	BEGIN		
		 SELECT  
				MTP.MetricTargetPresetID
				,MTP.MetricTargetPresetName	
				,MTP.MetricTargetPresetDesc				
			  FROM   
					CPSyncV.MetricTargetPreset MTP 
					LEFT JOIN CP.Region R  
						ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
					LEFT JOIN CP.Company C  
						ON C.CompanyID = MTP.CompanyID  
			  WHERE  
				Validation_1D = 1 AND ValidationType = 2 AND IsUsed = 1 AND
				(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
					(CASE  
						WHEN @PrintingTypeID = 1 THEN 1
						ELSE 0
					 END
					)

	END
	ELSE IF(@ProjectTypeID = 1 AND @LicenseTypeID = 11) -----DLP PLus project 
	BEGIN	
		 SELECT  
				MTP.MetricTargetPresetID
				,MTP.MetricTargetPresetName	
				,MTP.MetricTargetPresetDesc			
			  FROM   
					CPSyncV.MetricTargetPreset MTP 
					LEFT JOIN CP.Region R  
						ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
					LEFT JOIN CP.Company C  
						ON C.CompanyID = MTP.CompanyID  
			  WHERE  
				(Validation_4D = 1 ) AND ValidationType IN (0,1,2) AND IsUsed = 1 AND
				(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
					(CASE  
						WHEN @PrintingTypeID = 1 THEN 1
						ELSE 0
					 END
					)
				
	END
	ELSE ----Sync4D Projects
	BEGIN
		IF (@LicenseTypeID = 11)  -- DLPPLus PROJECT 
		BEGIN
			 --SELECT  
				--MTP.MetricTargetPresetID
				--,MTP.MetricTargetPresetName	
				--,MTP.MetricTargetPresetDesc			
			 -- FROM   
				--	CPSyncV.MetricTargetPreset MTP 
				--	LEFT JOIN CP.Region R  
				--		ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
				--	LEFT JOIN CP.Company C  
				--		ON C.CompanyID = MTP.CompanyID  
			 -- WHERE  
				--MTP.Validation_4D = 1 AND ValidationType = 1 AND IsUsed = 1 AND
				--(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				-- AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
				--	(CASE  
				--		WHEN @PrintingTypeID = 1 THEN 1
				--		ELSE 0
				--	 END
				--	)
				 SELECT  
				MTP.MetricTargetPresetID
				,MTP.MetricTargetPresetName	
				,MTP.MetricTargetPresetDesc				
			  FROM   
					CPSyncV.MetricTargetPreset MTP 
					LEFT JOIN CP.Region R  
						ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
					LEFT JOIN CP.Company C  
						ON C.CompanyID = MTP.CompanyID  
			  WHERE  
				Validation_4D = 1 AND IsUsed = 1 AND ValidationType IN (0,1,2) AND  ---Add Default mode as 0 also
				(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
					(CASE  
						WHEN @PrintingTypeID = 1 THEN 1
						ELSE 0
					 END
					)
		END
		ELSE
		BEGIN --- 4D Projects
		 SELECT  
				MTP.MetricTargetPresetID
				,MTP.MetricTargetPresetName	
				,MTP.MetricTargetPresetDesc				
			  FROM   
					CPSyncV.MetricTargetPreset MTP 
					LEFT JOIN CP.Region R  
						ON MTP.RegionID = R.RegionID AND CP.CheckUserRegion(@CurrentUserID,MTP.RegionID) = 1  
					LEFT JOIN CP.Company C  
						ON C.CompanyID = MTP.CompanyID  
			  WHERE  
				Validation_4D = 1 AND IsUsed = 1 AND ValidationType IN (0,1,2) AND  ---Add Default mode as 0 also
				(ISNULL(MTP.CompanyID,0) = 0 OR CP.CheckUserCompany(@CurrentUserID,MTP.CompanyID) = 1)
				 AND ISNULL(PrintingTypeID,0) =	----If Flexo print type, then pass only flexo enabled presets
					(CASE  
						WHEN @PrintingTypeID = 1 THEN 1
						ELSE 0
					 END
					)
		END
	END
END

GO


/****** Object:  StoredProcedure [Report].[GetSingleDLPPlusMeasurement]    Script Date: 3/23/2022 10:21:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Report].[GetSingleDLPPlusMeasurement] 
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
			,CASE WHEN (SELECT SUM(CONVERT(INT,IsNotOk)) from CPSync4DPlus.MetricMeasured where MeasurementID = @MeasurementID) > 0 THEN 'Out Of Tolerance'
				  ELSE 'OK'
			 END AS JobStatus, MTP.ValidationType
			 ,PC.TechnicianName, I.InstrumentType, MD.SerialNumber, MS.UVMode
			 ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.ReferenceChartTypeID) AS ChartName
			 ,(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.OPTReferenceChartTypeID) AS OPTChartName
			 ,PC.InkOrder, PC.FountainSolution, PC.Blanket, PC.BlanketAge, PC.Screening, PC.Coating, PC.Notes, P.LicenseTypeID, ISNULL(PC.PrintingType,0) AS PrintingType,
			 (SELECT ISNULL(ProfilerOptionsName,'') FROM CPSync4D.DeviceFamilyProfilerOption WHERE DeviceFamilyProfilerOptionID=P.BaseDeviceFamilyProfilerOption) AS ProfilerPresetName
	FROM      CPSync4D.Measurement AS M INNER JOIN 
					  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID =  M.OptimizeRunID INNER JOIN	                     
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
		
		FROM  CPSync4D.MeasurementPatch AS MP 
					INNER JOIN CPSync4D.Measurement AS M ON M.MeasurementID = MP.MeasurementID
					INNER JOIN CPSync4D.OptimizeRun AS OPT ON M.OptimizeRunID = OPT.OptimizeRunID
					INNER JOIN CPSync4D.Project AS P ON OPT.ProjectID = P.ProjectID 
					INNER JOIN CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName
					LEFT OUTER JOIN CPSync4DPlus.ProjectTargetLAB AS PTL ON PTL.ProjectID = P.ProjectID AND PTL.PatchTypeID = RC.PatchTypeID
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
			FROM  CPSync4D.Measurement AS M INNER JOIN
								  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		
			
			IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
				BEGIN
					SELECT  MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
							FROM    CPSync4D.Measurement AS M INNER JOIN 
									CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									MS.MetricSumID = MM.MetricSumID
							WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (36,37,38,39,40,41)
				END
			ELSE				----If Not-Weighted, pass regular MetricSumIDs
				BEGIN
					SELECT  MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
							FROM    CPSync4D.Measurement AS M INNER JOIN 
									CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									MS.MetricSumID = MM.MetricSumID
							WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (8,9,10,11,12,13)
				END	
		END
	ELSE -----Sync1D ISO project types
		BEGIN
			SELECT     MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
					FROM       CPSync4D.Measurement AS M INNER JOIN 
								  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
							      CPSync4DPlus.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
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
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (43,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (4) THEN (SELECT TOP 1 --K
													CASE 
														WHEN 44 IN (MetricSumID)  THEN (TargetValue)
														WHEN 4 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (44,4,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (5,6,7) THEN (SELECT TOP 1  --RGB
													CASE 
														WHEN 14 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (14,2) AND MeasurementID = @MeasurementID 
													ORDER BY MetricSumID DESC)
					WHEN PatchTypeID IN (8) THEN (SELECT TOP 1      --PAPER
													CASE 
														WHEN 3 IN (MetricSumID)  THEN (TargetValue)
														WHEN 2 IN (MetricSumID)  THEN (TargetValue)
														ELSE 0
													END AS B FROM CPSync4DPlus.MetricMeasured WHERE MetricSumID IN (3,2) AND MeasurementID = @MeasurementID 
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
      FROM CPSync4DPlus.vwGetUniquePatches
	  WHERE MeasurementID = @MeasurementID AND PatchTypeID IN (1,2,3,4,5,6,7,8,9)
       GROUP BY
                MeasurementID
				,PatchTypeID
				,HTMLColor				
       ORDER BY PatchTypeID

	
	---Table 4:For the Metric Summary --it is display not-weighted summary 
	--(Note:This temparory fixed Ben giving metric data which both weighted and non-weighted
	SELECT  MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
							FROM    CPSync4D.Measurement AS M INNER JOIN 
									CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									MS.MetricSumID = MM.MetricSumID
							WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (8,9,10,11,12,13,36,37,38,39,40,41)
	


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

GO

/****** Object:  StoredProcedure [CPSync4DPlus].[CalculateMetrics]    Script Date: 1/12/2021 4:28:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [CPSync4DPlus].[CalculateMetrics]	
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
						FROM   CPSync4D.Measurement AS M INNER JOIN 
									  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN 
									  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN									
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
						WHERE M.MeasurementID = @MeasurementID
						ORDER BY MS.MetricSumID

			-- Start looping through the records
			WHILE EXISTS (SELECT * FROM @TEMP)
			BEGIN
				-- Grab the first record out
				SELECT Top 1 @MetricSumID = MetricSumID, @dEMethod = dEMethod, @TargetValue = TargetValue FROM @TEMP


				-- Generate all metrics according to metic sums
				IF  (@MetricSumID = 1) ---For 'ΔE Average'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
								ELSE AVG(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSync4D.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 2)  ----For 'ΔE Maximum'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0				
							FROM CPSync4D.MeasurementPatch WHERE MeasurementID = @MeasurementID
					END
				ELSE IF (@MetricSumID = 3)  ----For 'Paper ΔE Avg'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN AVG(ΔE)
																	ELSE AVG(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 4)  ----For 'CMYK ΔE Max'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4)) AS MeasuredValue, 0							
					END
				--ELSE IF (@MetricSumID = 5)  ----For 'CMYK ΔH Max'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4)) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 6)  ----For 'FOGRA Gray ΔH Avg'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (52,53,54,55,56,57)) AS MeasuredValue, 0														
				--	END
				--ELSE IF (@MetricSumID = 7)  ----For 'CMYKRGB ΔH Max'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3,4,5,6,7)) AS MeasuredValue, 0																					
				--	END
				ELSE IF (@MetricSumID = 8)  ----For 'G7 Gray ΔCh Avg'
					BEGIN
--					EXEC [CPSync].[uspInsertColorCharΔCh] @MeasurementID 				
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 9)  ----For 'G7 Gray ΔCh Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 4)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 10)  ----For 'G7 Black ΔL Avg'
					BEGIN					
--					EXEC [CPSync].[uspInsertColorCharΔL] @MeasurementID 
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 11)  ----For 'G7 Black ΔL Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 12)  ----For 'G7 CMY / Gray ΔL Avg'
					BEGIN	
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 13)  ----For 'G7 CMY / Gray ΔL Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 14)  ----For 'RGB ΔE Max'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (5,6,7)) AS MeasuredValue, 0							
					END
				--ELSE IF (@MetricSumID = 15)  ----For 'CMY ΔTVI25'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136)		--25% C, M, Y Patches	
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 16)  ----For 'CMY ΔTVI50'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139)		--50% C, M, Y Patches	
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 17)  ----For 'CMY ΔTVI75'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142)		--75% C, M, Y Patches	
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 18)  ----For 'TVISpread25'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT   
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI	
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (16,19,22,114,125,136) --25% C, M, Y Patches		
				--					) ),0)	) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 19)  ----For 'TVISpread50'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT  
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (17,20,23,117,128,139) --50% C, M, Y Patches		
				--					) ),0)	) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 20)  ----For 'TVISpread75'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT  
				--				ISNULL(ABS (
				--				(
				--					SELECT ISNULL(MAX(ColorCharValue),0) As TVI	
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT ISNULL(MIN(ColorCharValue),0) As TVI	
				--						FROM CPSync4D.Measurement AS M INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (18,21,24,120,131,142) --75% C, M, Y Patches		
				--					) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
				--	END
				ELSE IF (@MetricSumID = 21)  ----For 'Paper ΔL' (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN	
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_L - TargetL),2))),2)),0)
								FROM CPSync4DPlus.vwGetUniquePatches
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 22)  ----For 'Paper Δa'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_a - Targeta),2))),2)),0)
								FROM CPSync4DPlus.vwGetUniquePatches
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 23)  ----For 'Paper Δb'  (Make square than apply SQRT to avoid (-ve) numbers)
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue,  
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(SQRT(POWER((CIELAB_b - Targetb),2))),2)),0)
								FROM CPSync4DPlus.vwGetUniquePatches
								WHERE MeasurementID = @MeasurementID AND PatchTypeID = 8)
							AS MeasuredValue, 0		
					END
				ELSE IF (@MetricSumID = 24)  ----For 'ΔE CMY100 Max'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 9) AS MeasuredValue, 0							
					END
				--ELSE IF (@MetricSumID = 25)  ----For 'Japan Gray ΔH Avg'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,61,62,63,64,65)) AS MeasuredValue, 0
				--			END
				--ELSE IF (@MetricSumID = 26)  ----For 'G7 Gray ΔH Avg'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ΔH),2)),0)
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50)) AS MeasuredValue, 0
				--			END
				ELSE IF (@MetricSumID = 27)  ----For 'ΔE Maximum excluding Paper'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							CASE 
								WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
								ELSE MAX(ΔE2000)
							END AS MeasuredValue, 0	
							FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID != 8
							END
				--ELSE IF (@MetricSumID = 28)  ----For 'ΔTVI20'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135)
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 29)  ----For 'ΔTVI40'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync4D.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync4D.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138)
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 30)  ----For 'ΔTVI60'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140)
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 31)  ----For 'ΔTVI80'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
				--														INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143)
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 32)  ----For 'TVISpread20'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT   
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (113,124,135) --20% C, M, Y Patches		
				--					) ),0)	) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 33)  ----For 'TVISpread40'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT  
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (116,127,138) --40% C, M, Y Patches		
				--					) ),0)	) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 34)  ----For 'TVISpread60'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT  
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (118,129,140) --60% C, M, Y Patches		
				--					) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
				--	END
				--ELSE IF (@MetricSumID = 35)  ----For 'TVISpread80'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT  
				--				ISNULL(ABS (
				--				(
				--					SELECT MAX(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
				--					) -	
				--					(
				--						SELECT MIN(ColorCharValue) As TVI	
				--						FROM CPSync.Measurement AS M INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--						INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--						INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--						WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (121,132,143) --80% C, M, Y Patches		
				--					) ),0)	AS MaxSpread ) AS MeasuredValue, 0							
				--	END
				ELSE IF (@MetricSumID = 36)  ----For 'G7 Gray wΔCh Avg'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 37)  ----For 'G7 Gray wΔCh Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 18)
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 38)  ----For 'G7 Black wΔL Avg'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 39)  ----For 'G7 Black wΔL Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (4,15,25,26,27))
							AS MeasuredValue, 0	
					END
				
				ELSE IF (@MetricSumID = 40)  ----For 'G7 CMY / Gray wΔL Avg'
					BEGIN	
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(AVG(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 41)  ----For 'G7 CMY / Gray wΔL Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 17 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50))
							AS MeasuredValue, 0	
					END
				ELSE IF (@MetricSumID = 43)  ----For 'CMY ΔE Max'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3)) AS MeasuredValue, 0							
					END
				ELSE IF (@MetricSumID = 44)  ----For 'K ΔE Max'
					BEGIN
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(
																CASE 
																	WHEN @dEMethod = 'ΔE' THEN MAX(ΔE)
																	ELSE MAX(ΔE2000)
																END,2)),0)
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (4)) AS MeasuredValue, 0							
					END
				--ELSE IF (@MetricSumID = 45)  ----For 'K ΔTVI25'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync4D.Measurement AS M 
				--														INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync4D.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (25,147)		--25% K Patches	
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 46)  ----For 'K ΔTVI50'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
				--														INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (26,150)		--50% K Patches		
				--				) AS V), 0							
				--	END
				--ELSE IF (@MetricSumID = 47)  ----For 'K ΔTVI75'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(MAX(CONVERT(decimal(5,2),ROUND(ABS((ActualTVI - TargetTVI)),2))),0) AS MeasuredValue FROM 
				--				(SELECT TVI AS TargetTVI , ColorCharValue As ActualTVI FROM CPSync.Measurement AS M 
				--														INNER JOIN CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID
				--														INNER JOIN CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID
				--														INNER JOIN CPSync.ProjectTargetTVI AS PTTVI ON PTTVI.ProjectID = P.ProjectID
				--														INNER JOIN CPSync.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				--														INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID  
				--														INNER JOIN CPSync.MeasuredColorChar AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID AND CC.ColorCharID = 9 --DotGain				
				--														AND PTTVI.PatchTypeID = RC.PatchTypeID
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (27,153)		--75% K Patches	
				--				) AS V), 0							
				--	END
				ELSE IF (@MetricSumID = 52)  ----For 'Paper L* Min'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MIN([CIELAB_L*]),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (8)) AS MeasuredValue, 0		
					END
				--ELSE IF (@MetricSumID = 53)  ----For 'CMY ΔH Max'
				--	BEGIN
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ΔH),2)),0) 
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID IN (1,2,3)) AS MeasuredValue, 0							
				--	END
				ELSE IF (@MetricSumID = 54)  ----For 'K ΔL Max'
					BEGIN					
						INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
							SELECT @MeasurementID, @MetricSumID, @TargetValue, 
							(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
								FROM
								CPSync4D.Measurement AS M INNER JOIN 
								CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
								CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
								CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
								WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 16 AND RC.PatchTypeID in (4))
							AS MeasuredValue, 0	
					END
				--ELSE IF (@MetricSumID = 55)  ----For 'K ΔC Max'
				--	BEGIN	
				--		EXEC [CPSync].[uspInsertColorCharΔC] @MeasurementID				
				--		INSERT INTO CPSync4DPlus.MetricMeasured (MeasurementID, MetricSumID, TargetValue, MeasuredValue, IsNotOK)
				--			SELECT @MeasurementID, @MetricSumID, @TargetValue, 
				--			(SELECT ISNULL(CONVERT(decimal(5,2),ROUND(MAX(ColorCharValue),2)),0) 
				--				FROM
				--				CPSync4D.Measurement AS M INNER JOIN 
				--				CPSync4D.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID INNER JOIN 
				--				CPSync4D.MeasuredColorChar AS MC on MP.MeasurementPatchID = MC.MeasurementPatchID INNER JOIN 
				--				CP.ReferenceChart AS RC ON M.ReferenceChartTypeID = RC.ChartTypeID AND MP.SampleName = RC.SampleName 
				--				WHERE M.MeasurementID = @MeasurementID AND ColorCharID = 19 AND RC.PatchTypeID in (4))
				--			AS MeasuredValue, 0	
				--	END
				
				DELETE FROM @TEMP WHERE MetricSumID = @MetricSumID
			END

			---Update IsNotOk status in CPSyncV.MetricMeasured table-----
			UPDATE CPSync4DPlus.MetricMeasured 
				SET IsNotOK = CASE WHEN MeasuredValue <= TargetValue THEN 0	ELSE 1 END
				WHERE MeasurementID = @MeasurementID

			---Update IsNotOk status (Value should be greater than 88 Target value) in CPSyncV.MetricMeasured table for Paper Min L* metric (MetricSumID = 52)-----
			UPDATE CPSync4DPlus.MetricMeasured 
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
		EXEC [dbo].[CreateMeasurementErrorLog] @UserID,@MeasurementID,'Error in CPSync4DPlus.CalculateMetrics measurement',@ERROR_MESSAGE,'SyncDLPPlus'
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH


GO

/****** Object:  StoredProcedure [CPSync4DPlus].[GetMeasurementDeltasByMeasurementID]    Script Date: 1/12/2021 4:28:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [CPSync4DPlus].[GetMeasurementDeltasByMeasurementID]  
@MeasurementID   int
,@From_ΔE NVARCHAR(20) 
AS  
BEGIN   




	----Get value whether this MTP has Weighted tolerance or not, according to that pass Metric Sums list to display in summary list
			DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM       CPSync4D.Measurement AS M INNER JOIN
								   CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								   CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								   CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		
			
			IF @Isweighted >= 1 ----If Weighted, don't pass Non-Weighted MetricSumIDs
				BEGIN
					
					-------------Get data of Measurement as Summary------------------
				SELECT  M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
									  '' AS Customer, '' AS Notes, MTP.dEMethod, 
									  CASE WHEN M.MetricTargetPresetID IS NOT NULL THEN (SELECT MetricTargetPresetName FROM CPSyncV.MetricTargetPreset WHERE MetricTargetPresetID = M.MetricTargetPresetID)
										   ELSE MTP.MetricTargetPresetName
									  END AS MetricTargetPresetName
									, MTP.ValidationType,
									  CASE sum(CONVERT(INT,MM.IsNotOK))
										WHEN 0 THEN 'OK'
										ELSE 'Out of Tolerance'
									  END AS MeasurementStatus							
									,(SELECT PrintStandard FROM CP.PrintStandardType AS PST 
											INNER JOIN CPSync4D.Project AS P ON P.PrintStandardTypeID = PST.PrintStandardTypeID
											INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
											INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OPT.OptimizeRunID						
											WHERE M.MeasurementID = @MeasurementID) AS PrintStandard
				FROM       CPSync4D.Measurement AS M INNER JOIN 
									  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (8,9,10,11,12,13)
				GROUP BY M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
						MTP.dEMethod, MTP.MetricTargetPresetName, MTP.ValidationType, M.MetricTargetPresetID

					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						 MM.MetricMeasuredID, MS.MetricSumID, MS.MetricSumName, 
						  MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
					FROM      CPSync4D.Measurement AS M INNER JOIN 
										  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync4DPlus.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										
					WHERE  MM.MeasuredValue ! = 0 AND M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (8,9,10,11,12,13)			
					ORDER BY MS.RankID
				END
			ELSE              ----If Not-Weighted, don't pass Weighted MetricSumIDs
				BEGIN

						-------------Get data of Measurement as Summary------------------
				SELECT  M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
									  '' AS Customer, '' AS Notes, MTP.dEMethod, 
									  CASE WHEN M.MetricTargetPresetID IS NOT NULL THEN (SELECT MetricTargetPresetName FROM CPSyncV.MetricTargetPreset WHERE MetricTargetPresetID = M.MetricTargetPresetID)
										   ELSE MTP.MetricTargetPresetName
									  END AS MetricTargetPresetName
									, MTP.ValidationType,
									  CASE sum(CONVERT(INT,MM.IsNotOK))
										WHEN 0 THEN 'OK'
										ELSE 'Out of Tolerance'
									  END AS MeasurementStatus							
									,(SELECT PrintStandard FROM CP.PrintStandardType AS PST 
											INNER JOIN CPSync4D.Project AS P ON P.PrintStandardTypeID = PST.PrintStandardTypeID
											INNER JOIN CPSync4D.OptimizeRun AS OPT ON OPT.ProjectID = P.ProjectID
											INNER JOIN CPSync4D.Measurement AS M ON M.OptimizeRunID = OPT.OptimizeRunID						
											WHERE M.MeasurementID = @MeasurementID) AS PrintStandard
				FROM       CPSync4D.Measurement AS M INNER JOIN 
									  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
									  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSync4DPlus.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (36,37,38,39,40,41)
				GROUP BY M.MeasurementID, M.MeasurementName, M.MeasurementDate, 
						MTP.dEMethod, MTP.MetricTargetPresetName, MTP.ValidationType, M.MetricTargetPresetID

					----------Get list of all measured metrics of this measurement for Summary----------------
					SELECT     
						 MM.MetricMeasuredID, MS.MetricSumID, MS.MetricSumName, 
						  MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
					FROM      CPSync4D.Measurement AS M INNER JOIN 
										  CPSync4D.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
										  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
										  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
										  CPSync4DPlus.MetricMeasured AS MM ON MM.MeasurementID = M.MeasurementID INNER JOIN
										  CPSyncV.MetricSum AS MS ON MS.MetricSumID = MM.MetricSumID 
										
					WHERE  MM.MeasuredValue ! = 0 AND M.MeasurementID = @MeasurementID AND MS.MetricSumID NOT IN (36,37,38,39,40,41)	
					ORDER BY MS.RankID
				END



END


GO




/****** Object:  StoredProcedure [CPSyncV].[GetVChartData_G7]    Script Date: 3/30/2022 10:58:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [CPSyncV].[GetVChartData_G7]
/* Retrieve data for G7 Balance tab for selected measurement */  
@MeasurementID  int  
AS  
BEGIN  
  
	----Get value whether this MTP has Weighted tolerance or not, according to that pass Metric Sums list to display in summary list
			DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM        CPSyncV.Measurement AS M INNER JOIN
							CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
							CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
							CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
							CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		
			
	 --Gray Δch / ΔF  
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔF),2)) AS ΔF, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔF),2)) AS wΔF,
		(SELECT  TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSyncV.MeasurementSetting AS MS ON MT.MetricTargetPresetID = MS.MetricTargetPresetID
		INNER JOIN CPSyncV.Measurement AS M ON MS.MeasurementID = M.MeasurementID
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (9,37)) As ΔLimit 
	 FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSyncV.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 4 OR CC.ColorCharID = 18) ---4 = ΔF AND 18 = wΔF
	 GROUP BY RC.CMYK_C
	 ORDER BY CMYK_C  
	
	--Gray Δch / ΔF Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (36,37)
			END
	ELSE			----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (8,9)
			END

	--Black ΔL
	 SELECT  RC.CMYK_K, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔL),2)) AS ΔL, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔL),2)) AS wΔL,  
		(SELECT  TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSyncV.MeasurementSetting AS MS ON MT.MetricTargetPresetID = MS.MetricTargetPresetID
		INNER JOIN CPSyncV.Measurement AS M ON MS.MeasurementID = M.MeasurementID
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (11,39)) As ΔLimit 
	 FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSyncV.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 16 OR CC.ColorCharID = 17) AND RC.PatchTypeID in (4,15,25,26,27)
	 GROUP BY RC.CMYK_K
	 ORDER BY CMYK_K  

	--Black ΔL Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (38,39)
			END
	ELSE			----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (10,11)
			END

	--CMY / Gray ΔL
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.ΔL),2)) AS ΔL, CONVERT(decimal(5,2),ROUND(MAX(CC.wΔL),2)) AS wΔL, 
		(SELECT  TOP 1 MT.TargetValue FROM CPSyncV.MetricTarget AS MT 
		INNER JOIN  CPSyncV.MeasurementSetting AS MS ON MT.MetricTargetPresetID = MS.MetricTargetPresetID
		INNER JOIN CPSyncV.Measurement AS M ON MS.MeasurementID = M.MeasurementID
		WHERE M.MeasurementID = @MeasurementID  AND MT.MetricSumID IN (13,41)) As ΔLimit 
	 FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSyncV.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND (CC.ColorCharID = 16 OR CC.ColorCharID = 17) AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) 
	 GROUP BY RC.CMYK_C
	 ORDER BY CMYK_C  

	--Gray ΔL Summary
	IF @Isweighted = 1 ----If Weighted, pass Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (40,41)
			END
	ELSE				----If Non-Weighted, pass Non-Weighted MetricSumIDs
			BEGIN
				SELECT     MM.MetricMeasuredID, MS.MetricSumName, MM.TargetValue, MM.MeasuredValue, MM.IsNotOK
				FROM       CPSyncV.Measurement AS M INNER JOIN
									  CPSyncV.MeasurementSetting AS MST ON M.MeasurementID = MST.MeasurementID INNER JOIN
									  CPSyncV.MetricTargetPreset AS MTP ON MST.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
									  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID INNER JOIN
									  CPSyncV.MetricMeasured AS MM ON M.MeasurementID = MM.MeasurementID AND 
									  MS.MetricSumID = MM.MetricSumID
				WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID IN (12,13)
			END

	--Black G7 NPD
	 SELECT  RC.CMYK_K, CONVERT(decimal(5,2),ROUND(MAX(CC.G7NPDC),2)) AS G7NPDC,
			CONVERT(decimal(5,2),ROUND((SELECT LOG10(MAX(MP.CIEXYZ_Y)) FROM  CPSyncV.Measurement AS M 
				INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID 
				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) - LOG10(MAX(MP.CIEXYZ_Y)),2)) AS G7NPDC_Act			 	 
		FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSyncV.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND CC.ColorCharID = 13 AND RC.PatchTypeID in (4,15,25,26,27)
	 GROUP BY RC.CMYK_K
	 ORDER BY CMYK_K  

	 --Gray G7 NPD
	 SELECT  RC.CMYK_C, CONVERT(decimal(5,2),ROUND(MAX(CC.G7NPDC),2)) AS G7NPDC,
			CONVERT(decimal(5,2),ROUND((SELECT LOG10(MAX(MP.CIEXYZ_Y)) FROM  CPSyncV.Measurement AS M 
				INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
				INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID 
				WHERE M.MeasurementID = @MeasurementID AND RC.PatchTypeID = 8) - LOG10(MAX(MP.CIEXYZ_Y)),2)) AS G7NPDC_Act			 	 		
		FROM CPSyncV.Measurement AS M INNER JOIN CPSyncV.MeasurementPatch As MP ON M.MeasurementID = MP.MeasurementID  
	 INNER JOIN CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName  AND RC.ChartTypeID = M.ReferenceChartTypeID
	 INNER JOIN CPSyncV.vwGetColorChars AS CC ON CC.MeasurementPatchID = MP.MeasurementPatchID  
	 WHERE M.MeasurementID = @MeasurementID AND CC.ColorCharID = 13 AND RC.PatchTypeID in (9,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50) 
	 GROUP BY RC.CMYK_C
	 ORDER BY CMYK_C  	

END













