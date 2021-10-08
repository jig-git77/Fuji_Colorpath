IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSync4D].[GetProjectDetail1]'))
	exec('CREATE PROCEDURE [CPSync4D].[GetProjectDetail] AS BEGIN SET NOCOUNT ON; END')

GO
/****** Object:  StoredProcedure [CPSync4D].[GetProjectDetail]    Script Date: 12/04/2012 09:30:16 ******/
ALTER PROCEDURE [CPSync4D].[GetProjectDetail] 
	@ProjectID INT,
	@IsVarification BIT = 0
AS  
	BEGIN TRY   
		DECLARE @LicenseTypeID INT
		SELECT @LicenseTypeID =  LicenseTypeId FROM CPSync4D.Project WHERE ProjectID = @ProjectID
		
		--Get detail from project  
		SELECT  
			PRJ.ProjectID,
			PRJ.CPDeviceID,
			PRJ.ProjectName,
			PRJ.CreateDate,
			PRJ.CurrDLPID,
			PRJ.IsUsed,
			ISNULL((SELECT TOP 1 OPT.Status FROM CPSync4D.OptimizeRun OPT WHERE OPT.ProjectID = PRJ.ProjectID ORDER BY OPT.OptimizeRunID DESC),1) AS ProjStatus,
			PRJ.PrintStandardTypeId,  			
			PST.PrintStandard,
			PRJ.BaseDeviceFamilyProfilerOption ,
			SP.ProfileName,
			SP.SourceProfileID,
			PRJ.EnabledValidation,
			PRJ.LicenseTypeID,
			PRJ.MetricTargetPresetID,
			(
			CASE ISNULL(PRJ.MetricTargetPresetID,0)
				WHEN 0 THEN 0
				ELSE (CASE 
						WHEN PRJ.LicenseTypeID = 1 THEN (SELECT TOP 1 ValidationType FROM [CPSyncV].[MetricTargetPreset]  AS MTP WHERE MTP.MetricTargetPresetId = PRJ.MetricTargetPresetID)
						WHEN PRJ.LicenseTypeID = 11 THEN (SELECT TOP 1 ValidationType FROM [CPSyncV].[MetricTargetPreset]  AS MTP WHERE MTP.MetricTargetPresetId = PRJ.MetricTargetPresetID) --Add for DLPPLUS Condition
						WHEN PRJ.LicenseTypeID = 13 THEN (SELECT TOP 1 ValidationType FROM [CPSyncV].[MetricTargetPreset]  AS MTP WHERE MTP.MetricTargetPresetId = PRJ.MetricTargetPresetID) --Add for Sync1DMultiColor Condition
						ELSE 3
						END)
			END
			) 'ProjectTypeId',
			PRJ.DensityType,
			PRJ.PCSDataType,
			PRJ.ReTargetProjectID,
			PRJ.CurveTypeID,
			PRJ.EnabledPaperScaling,
			PRJ.NewPaper_White_L,
			PRJ.NewPaper_White_a,
			PRJ.NewPaper_White_b,
			PRJ.NewPaper_Black_L,
			PRJ.NewPaper_Black_a,
			PRJ.NewPaper_Black_b,
			RTPJ.CPDeviceID AS RetargetProjectDeviceID
		FROM
			CPSync4D.Project PRJ INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeId = PST.PrintStandardTypeId  
			INNER JOIN CPSync4D.SourceProfile SP ON PST.SourceProfileID = SP.SourceProfileID
			LEFT JOIN CPSync4D.Project RTPJ ON RTPJ.ProjectID = PRJ.ReTargetProjectID
		WHERE  
			PRJ.ProjectID = @ProjectID  

		--Get detail from print condition  
		SELECT
			PrintConditionID,   
			Media,  
			Colorant,  
			RipSettings,  
			TechnicianName,  
			Notes,
			PrintingType,
			InkOrder,
			FountainSolution,
			Blanket,
			BlanketAge,
			Screening,
			Coating,
			ISNULL(IsCustomProfilerOption,0) AS IsCustomProfilerOption
			,ISNULL(IsCustomPlugin,0) AS IsCustomPlugin
		FROM  
			CPSync4D.PrintCondition  
		WHERE  
			ProjectID = @ProjectID  

		--Get detail from Measurement Setting  
		SELECT  
			MS.ObserverType,  
			MS.Illuminant,			
			MS.UVMode,
			MS.MeasurementCount,
			MS.ReadMode,
			MS.MeasurementDeviceID,
			(SELECT TOP 1 InstrumentTypeID FROM CP.MeasurementDevice MD 
				WHERE MD.MeasurementDeviceID = CASE WHEN @IsVarification = 0 THEN MS.MeasurementDeviceID ELSE MS.VMeasurementDeviceID END) AS InstrumentTypeID,
			MS.ReferenceChartTypeID,
			(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.ReferenceChartTypeID) AS ChartName,
			MS.OPTReferenceChartTypeID,
			(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.OPTReferenceChartTypeID) AS OPTChartName,
			
			MS.VMeasurementDeviceID,
			MS.VReferenceChartTypeID,
			(SELECT TOP 1 ChartName FROM CP.ReferenceChartType RC WHERE RC.ReferenceChartTypeID = MS.OPTReferenceChartTypeID) AS VChartName,
			MS.MetricTargetPresetID,
			(SELECT TOP 1 MetricTargetPresetName FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = MS.MetricTargetPresetID) AS MetricTargetPresetName,
			(SELECT TOP 1 ValidationType FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = MS.MetricTargetPresetID) AS ValidationType,
			(SELECT TOP 1 dEMethod FROM CPSyncV.MetricTargetPreset where dEMethod IN (SELECT dEMethod FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = MS.MetricTargetPresetID
               UNION ALL SELECT dEMethod FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = P.MetricTargetPresetID)) AS dEMethod,
			CASE 
				WHEN @IsVarification = 0 THEN (SELECT TOP 1 Weighted FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = (SELECT MetricTargetPresetID FROM cpsync4D.Project WHERE ProjectID = @ProjectID)) 
				ELSE (SELECT TOP 1 Weighted FROM CPSyncV.MetricTargetPreset MTP WHERE MTP.MetricTargetPresetID = MS.MetricTargetPresetID) 
			END AS Weighted,  
			MS.VUVMode,
			MS.VMeasurementCount,
			MS.VReadMode,
			(SELECT TOP 1 SerialNumber FROM CP.MeasurementDevice MD 
				WHERE MD.MeasurementDeviceID = CASE WHEN @IsVarification = 0 THEN MS.MeasurementDeviceID ELSE MS.VMeasurementDeviceID END) AS DeviceSerialNumber
			--,ISNULL(PluginType,1) AS PluginType
		FROM  
			CPSync4D.MeasurementSetting MS INNER JOIN CPSync4D.Project AS P ON P.Projectid = MS.ProjectID
		WHERE  
			MS.ProjectID = @ProjectID  
		
		--Get detail from project option
		IF (@LicenseTypeId = 1 OR @LicenseTypeId = 13) ---SYnc 1D and SYNC1DMulticolor
		BEGIN
			SELECT
			0 'ProfileTypeID',
			ProfilerOptionID,
			ProfilerOptionValue
			FROM
				CPSync.ProjectOption
			WHERE
				ProjectID = @ProjectID
		END
		ELSE
		BEGIN
			SELECT
			ProfileTypeID,
			ProfilerOptionID,
			ProfilerOptionValue
			FROM
				CPSync4D.ProjectOption
			WHERE
				ProjectID = @ProjectID
		END

		IF @IsVarification = 0
		BEGIN
			--Get detail from Optimize Runs 
			IF (@LicenseTypeId = 1 OR @LicenseTypeId = 13) ---SYnc 1D and SYNC1DMulticolor
			BEGIN
				SELECT  
					OPT.OptimizeRunID, 			
					OPT.ProjectID,  
					OPT.[Status],  
					OPT.StartDateTime,  
					OPT.EndDateTime,  
					CASE WHEN OPT.DeviceLinkProfile IS NULL THEN 0 ELSE 1 END AS HasProfile,  
					OPT.MasterRunID,
					(SELECT COUNT(*) FROM CPSync.Measurement mnt WHERE mnt.OptimizeRunID = OPT.OptimizeRunID) AS MeasurementCount,
					CPSync.GetOptimizeRunChartId(OPT.OptimizeRunID) AS ReferenceChartTypeId,
					(SELECT TOP 1 ChartName FROM CP.ReferenceChartType WHERE ReferenceChartTypeID = CPSync.GetOptimizeRunChartId(OPT.OptimizeRunID)) AS ChartName,
					OPT.DeviceLinkProfile,
					OPT.RunName AS CurrentRunName
				FROM  
					CPSync.OptimizeRun OPT  
				WHERE  
					OPT.ProjectID = @ProjectID  
			END
			ELSE
			BEGIN
				SELECT  
					OPT.OptimizeRunID, 			
					OPT.ProjectID,  
					OPT.[Status],  
					OPT.StartDateTime,  
					OPT.EndDateTime,  
					CASE WHEN OPT.DeviceLinkProfile IS NULL THEN 0 ELSE 1 END AS HasProfile,  
					OPT.MasterRunID,
					(SELECT COUNT(*) FROM CPSync4D.Measurement mnt WHERE mnt.OptimizeRunID = OPT.OptimizeRunID) AS MeasurementCount,
					CPSync4D.GetOptimizeRunChartId(OPT.OptimizeRunID) AS ReferenceChartTypeId,
					(SELECT TOP 1 ChartName FROM CP.ReferenceChartType WHERE ReferenceChartTypeID = CPSync4D.GetOptimizeRunChartId(OPT.OptimizeRunID)) AS ChartName,
					OPT.DeviceLinkProfile,
					OPT.RunName AS CurrentRunName
				FROM  
					CPSync4D.OptimizeRun OPT  
				WHERE  
					OPT.ProjectID = @ProjectID  
			END			
		END
		ELSE
		BEGIN
			--ALTER virtual run for verification and get information
			SELECT  
				0 AS OptimizeRunID, 			
				@ProjectID AS ProjectID,  
				1 AS [Status],  
				NULL AS StartDateTime,  
				NULL AS EndDateTime,  
				0 AS HasProfile,  
				0 AS MasterRunID,
				(SELECT COUNT(*) FROM CPSyncV.Measurement mnt WHERE mnt.ProjectID = @ProjectID) AS MeasurementCount,
				0 AS ReferenceChartTypeId,
				'' AS ChartName,
				NULL AS DeviceLinkProfile				
		END
	
	-----Select data (Only for Sync1D projects)
	   IF (@LicenseTypeId = 1 OR @LicenseTypeId = 13) ---Sync1D and SYNC1DMulticolor
		BEGIN
			------Select ProjectTargetLAB data for Sync1D Project
			SELECT			
					PLAB.PatchTypeID, PatchType
					,[TargetL*] AS TargetL
					,[Targeta*] AS TargetA
					,[Targetb*] AS TargetB
				FROM CPSync.ProjectTargetLAB AS PLAB INNER JOIN
				CP.PatchType AS PT ON PT.PatchTypeID = PLAB.PatchTypeID
				WHERE ProjectID = @ProjectID
			
	
			------Select ProjectTargetTVI data for Sync1D Project
            SELECT			
					PTVI.PatchTypeID, PatchType
					,TVI 
				FROM CPSync.ProjectTargetTVI AS PTVI INNER JOIN
				CP.PatchType AS PT ON PT.PatchTypeID = PTVI.PatchTypeID
				WHERE ProjectID = @ProjectID
			UNION ALL
			SELECT			
					PTVI.PatchTypeID, PatchType
					,TVI  
				FROM CPSync.ProjectTargetTVI AS PTVI INNER JOIN
				CP.PatchType_Sync1D AS PT ON PT.PatchTypeID = PTVI.PatchTypeID
				WHERE ProjectID = @ProjectID ----For all new ISO 10th positions CMYK patches

		------Select ProjectTargetDensity data for Sync1D Project
			SELECT			
					PTD.PatchTypeID, PatchType
					,[Target]
				FROM CPSync.ProjectTargetDensity AS PTD INNER JOIN
				CP.PatchType AS PT ON PT.PatchTypeID = PTD.PatchTypeID
				WHERE ProjectID = @ProjectID

		------Select ProjectTargetFlexoHueAngle data for Sync1D Project
			SELECT			
					PTF.PatchTypeID,
					CASE PTF.PatchTypeID
						WHEN 5 THEN 'Overprint_Red'
						WHEN 6 THEN 'Overprint_Green'
						WHEN 7 THEN 'Overprint_Blue'
						ELSE PatchType
					 END AS PatchType
					,[Target]
				FROM CPSync.ProjectTargetFlexoHueAngle AS PTF INNER JOIN
				CP.PatchType AS PT ON PT.PatchTypeID = PTF.PatchTypeID
				WHERE ProjectID = @ProjectID

		END


		IF @LicenseTypeId = 11 -- DLPPlus
		BEGIN
			------Select ProjectTargetLAB data for Sync1D Project
			SELECT			
					PLAB.PatchTypeID, PatchType
					,[TargetL*] AS TargetL
					,[Targeta*] AS TargetA
					,[Targetb*] AS TargetB
				FROM CPSync4DPlus.ProjectTargetLAB AS PLAB INNER JOIN
				CP.PatchType AS PT ON PT.PatchTypeID = PLAB.PatchTypeID
				WHERE ProjectID = @ProjectID
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSyncMultiColor].[GetPatchDetailsByMeasurementID]'))
	exec('CREATE PROCEDURE [CPSyncMultiColor].[GetPatchDetailsByMeasurementID] AS BEGIN SET NOCOUNT ON; END')

GO
ALTER PROCEDURE [CPSyncMultiColor].[GetPatchDetailsByMeasurementID]  
@MeasurementID   int   
AS  
BEGIN  

DECLARE @Isweighted INT;
			SELECT @Isweighted = COUNT(*)
			FROM       CPSync.Measurement AS M INNER JOIN
								  CPSync.OptimizeRun AS OPT ON OPT.OptimizeRunID = M.OptimizeRunID INNER JOIN
								  CPSync4D.Project AS P ON P.ProjectID = OPT.ProjectID INNER JOIN
								  CPSyncV.MetricTargetPreset AS MTP ON P.MetricTargetPresetID = MTP.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricTarget AS MT ON MTP.MetricTargetPresetID = MT.MetricTargetPresetID INNER JOIN
								  CPSyncV.MetricSum AS MS ON MT.MetricSumID = MS.MetricSumID
			WHERE M.MeasurementID = @MeasurementID AND MS.MetricSumID = 37		

-------Dataset for getting measurement patch details-------------
SELECT      RC.SampleID, 
			RC.SampleName, 
			RC.CMYK_C, 
			RC.CMYK_M, 
			RC.CMYK_Y, 
			RC.CMYK_K, 
			RC.HTMLColor,
			PLAB.[TargetL*] AS TargetL,
			PLAB.[Targeta*] AS Targeta,
			PLAB.[Targetb*] AS Targetb,
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
			MP.ΔH,			
			CONVERT(decimal(5,2),ROUND(ISNULL((SELECT ColorCharValue 
			FROM CPSync.MeasuredColorChar AS MCC 
			where MCC.ColorCharID = CASE WHEN (@Isweighted>0) THEN 18  ELSE 4 END
			AND MCC.MeasurementPatchID = MP.MeasurementPatchID),0),2)) AS ΔCh,
			CONVERT(decimal(5,2),ROUND(ISNULL((SELECT ColorCharValue FROM CPSync.MeasuredColorChar AS MCC 
			where MCC.ColorCharID = CASE WHEN (@Isweighted>0) THEN 17  ELSE 16 END 
			AND MCC.MeasurementPatchID = MP.MeasurementPatchID),0),2)) AS ΔL,
			MPS.nm380,
			MPS.nm390,
			MPS.nm400,
			MPS.nm410,
			MPS.nm420,
			MPS.nm430,
			MPS.nm440,
			MPS.nm450,
			MPS.nm460,
			MPS.nm470,
			MPS.nm480,
			MPS.nm490,
			MPS.nm500,
			MPS.nm510,
			MPS.nm520,
			MPS.nm530,
			MPS.nm540,
			MPS.nm550,
			MPS.nm560,
			MPS.nm570,
			MPS.nm580,
			MPS.nm590,
			MPS.nm600,
			MPS.nm610,
			MPS.nm620,
			MPS.nm630,
			MPS.nm640,
			MPS.nm650,
			MPS.nm660,
			MPS.nm670,
			MPS.nm680,
			MPS.nm690,
			MPS.nm700,
			MPS.nm710,
			MPS.nm720,
			MPS.nm730,
			CASE WHEN @Isweighted > 0 THEN 1 ELSE 0 END AS IsWeighted,
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
FROM        CPSync.Measurement AS M 
			INNER JOIN 	CPSync.MeasurementPatch AS MP ON MP.MeasurementID = M.MeasurementID  
			INNER JOIN  CPSync.OptimizeRun AS OTR ON OTR.OptimizeRunID = M.OptimizeRunID 
			INNER JOIN 	CPSync4D.Project AS P ON P.ProjectID = OTR.ProjectID  
			INNER JOIN  CP.ReferenceChart AS RC ON RC.SampleName = MP.SampleName AND RC.ChartTypeID = M.ReferenceChartTypeID		
			LEFT OUTER JOIN CPSync.ProjectTargetLAB AS PLAB ON PLAB.ProjectID = P.ProjectID	AND RC.PatchTypeID = PLAB.PatchTypeID	
			LEFT OUTER JOIN CPSync.MeasurementPatch_Spectral AS MPS ON MPS.MeasurementPatchID = MP.MeasurementPatchID
			INNER JOIN CP.ReferenceChartType AS RCT ON RC.ChartTypeID=RCT.ReferenceChartTypeID
			LEFT JOIN CP.MultiColorChannel AS MCC5 ON RCT.Channel_5 = MCC5.MultiColorChannelID
			LEFT JOIN CP.MultiColorChannel AS MCC6 ON RCT.Channel_6 = MCC6.MultiColorChannelID
			LEFT JOIN CP.MultiColorChannel AS MCC7 ON RCT.Channel_7 = MCC7.MultiColorChannelID
			LEFT JOIN CP.MultiColorChannel AS MCC8 ON RCT.Channel_8 = MCC8.MultiColorChannelID
			LEFT JOIN CP.MultiColorChannel AS MCC9 ON RCT.Channel_9 = MCC9.MultiColorChannelID

WHERE     M.MeasurementID = @MeasurementID 

ORDER BY RC.ReferenceChartID

END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSync4D].[ImportMeasurementDetails]'))
	exec('CREATE PROCEDURE [CPSync4D].[ImportMeasurementDetails] AS BEGIN SET NOCOUNT ON; END')

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

		IF(@LicenseType = 1 OR @LicenseType = 2 OR @LicenseType = 51 OR @LicenseType = 12 OR @LicenseType = 13)
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
				
				IF(@LicenseType IN (2,12)) -- DLP & DLPMultiColor Channel
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CP].[GetLoginInformationBySerial_LAB]'))
	exec('CREATE PROCEDURE [CP].[GetLoginInformationBySerial_LAB] AS BEGIN SET NOCOUNT ON; END')

GO
ALTER PROCEDURE [CP].[GetLoginInformationBySerial_LAB]--    'psylvester','fuji'
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
			 FROM [CP].[MeasurementPluginStatus] AS m 
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
		FROM [CP].[MeasurementPluginStatus]
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

		IF (@LicenseTypeID IN (1,13))-- 1d project To get value for LAB and result merge with table. Sync1D MultiColor
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
			IF (@LicenseTypeID IN (1,13))		   ----Sync1D project & Sync1DMulticolor
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
		

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CP].[CreateReferenceChart]'))
	exec('CREATE PROCEDURE [CP].[CreateReferenceChart] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CP].[CreateReferenceChart]
	@ChartName					NVARCHAR(27) 
	,@NumOfRows					TINYINT 
	,@NumOfCols					TINYINT 
	,@InstrumentTypeID			TINYINT 
	,@ChartPreview				VARBINARY(MAX)
	,@OriginalChart				VARBINARY(MAX)
	,@IsUsed					BIT 
	,@ReferenceChartData		XML
	,@IsSync1D_ISO				TINYINT
	,@IsSync1D_G7				TINYINT
	,@Validation_1D				TINYINT
	,@IsSync4D					TINYINT
	,@Validation_4D				TINYINT
	,@IsSync4D_Plus				TINYINT
	,@Validation_4DPlus			TINYINT
	,@IsSync1DMultiColor_ISO			TINYINT
	,@IsSync1DMultiColor_G7			TINYINT
	,@Validation_4D_Mutli_Color	TINYINT
	,@IsSupportMulticolor BIT=NULL
	,@TotalNoColor TINYINT=NULL
	,@Channel_5 TINYINT=NULL
	,@Channel_6 TINYINT=NULL
	,@Channel_7 TINYINT=NULL
	,@Channel_8 TINYINT=NULL
	,@Channel_9 TINYINT=NULL
	,@IsSync1DMultiColor			TINYINT
	,@Validation_Sync1D_Multi_Color	TINYINT
	,@ChartSetting				NVARCHAR(MAX)

AS

	BEGIN TRY
		IF EXISTS (SELECT * FROM CP.ReferenceChartType Where Upper(ChartName) = Upper(@ChartName) AND InstrumentTypeID = @InstrumentTypeID)
		BEGIN  
			Return -1
		END  
		BEGIN TRAN
			-----SET InstrumentTypeID For Pro2 type Same as i1-Pro or i1-IO
			IF (@InstrumentTypeID = 4)
			BEGIN
				SET @InstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 5)
			BEGIN
				SET @InstrumentTypeID = 3;
			END
			ELSE IF (@InstrumentTypeID = 8)
			BEGIN
				SET @InstrumentTypeID = 2;
			END

			DECLARE @ReferenceChartTypeID TINYINT
			SELECT
				@ReferenceChartTypeID = MAX(ReferenceChartTypeID) + 1 
			FROM CP.ReferenceChartType WITH (UPDLOCK, HOLDLOCK)

			INSERT INTO
				CP.ReferenceChartType
				(
					ReferenceChartTypeID,
					ChartName,
					NumOfRows,
					NumOfCols,
					InstrumentTypeID,
					ChartPreview,
					OriginalChart,
					IsSync1D_ISO,
					IsSync1D_G7,
					Validation_1D,
					IsSync4D,
					Validation_4D,					
					IsUsed,
					IsSync4D_Plus,
					Validation_4DPlus,
					IsDLPMultiColor,
					Validation_4D_Mutli_Color,
					IsSupportMulticolor,
					TotalNoColor,
					Channel_5,
					Channel_6,
					Channel_7,
					Channel_8,
					Channel_9,
					IsSync1DMultiColor_ISO,
					IsSync1DMultiColor_G7,
					Validation_Sync1D_Multi_Color
				)
			VALUES
				(
					@ReferenceChartTypeID,
					@ChartName,
					@NumOfRows,
					@NumOfCols,
					@InstrumentTypeID,					
					@ChartPreview,
					@OriginalChart,
					@IsSync1D_ISO,
					@IsSync1D_G7,
					@Validation_1D,
					@IsSync4D,
					@Validation_4D,					
					@IsUsed,
					@IsSync4D_Plus,
					@Validation_4DPlus,
					@IsSync1DMultiColor_ISO,
					@IsSync1DMultiColor_G7,
					@Validation_4D_Mutli_Color,
					@IsSupportMulticolor,
					@TotalNoColor,
					@Channel_5,
					@Channel_6,
					@Channel_7,
					@Channel_8,
					@Channel_9,
					@IsSync1DMultiColor,
					@Validation_Sync1D_Multi_Color
				)
			
			DECLARE @ChartSettingId TINYINT
			SELECT
				@ChartSettingId = MAX(ChartSettingID) + 1 
			FROM CP.ChartSetting WITH (UPDLOCK, HOLDLOCK)
			
			IF  @ChartSetting IS NOT NULL
			BEGIN
				INSERT INTO CP.ChartSetting
					(
						ChartSettingID,
						ChartSetting,
						ReferenceChartTypeID
					)
				VALUES
					(
						@ChartSettingId,
						@ChartSetting,
						@ReferenceChartTypeID
					)
			END
				
			--INSERTING REFERENCE CHART DATA
			;WITH ReferenceChartData AS
			(
				SELECT           
					[ReferenceChartXml].value('@SampleID', 'SMALLINT') AS SampleID,
					[ReferenceChartXml].value('@SampleName', 'NVARCHAR(50)') AS SampleName,
					[ReferenceChartXml].value('@CMYK_C', 'DECIMAL(6,2)') AS CMYK_C,
					[ReferenceChartXml].value('@CMYK_M', 'DECIMAL(6,2)') AS CMYK_M,
					[ReferenceChartXml].value('@CMYK_Y', 'DECIMAL(6,2)') AS CMYK_Y,
					[ReferenceChartXml].value('@CMYK_K', 'DECIMAL(6,2)') AS CMYK_K,
					[ReferenceChartXml].value('@HTMLColor', 'VARCHAR(7)') AS HTMLColor,
					[ReferenceChartXml].value('@Channel_5', 'DECIMAL(6,2)') AS Channel_5,
					[ReferenceChartXml].value('@Channel_6', 'DECIMAL(6,2)') AS Channel_6,
					[ReferenceChartXml].value('@Channel_7', 'DECIMAL(6,2)') AS Channel_7,
					[ReferenceChartXml].value('@Channel_8', 'DECIMAL(6,2)') AS Channel_8,
					[ReferenceChartXml].value('@Channel_9', 'DECIMAL(6,2)') AS Channel_9
				FROM
					@ReferenceChartData.
						nodes('/ReferenceChartData/ReferenceChart') p([ReferenceChartXml])
			)
			
			INSERT INTO CP.ReferenceChart
				(
					ChartTypeID,
					SampleID,
					SampleName,
					CMYK_C,
					CMYK_M,
					CMYK_Y,
					CMYK_K,
					PatchTypeID,
					IsOuterGamut,
					HTMLColor,
					Channel_5,
					Channel_6,
					Channel_7,
					Channel_8,
					Channel_9
				)
			SELECT
					@ReferenceChartTypeID,
					SampleID,
					SampleName,
					CMYK_C,
					CMYK_M,
					CMYK_Y,
					CMYK_K,
					CP.GetPatchTypeID(CMYK_C,CMYK_M,CMYK_Y,CMYK_K,Channel_5,Channel_6,Channel_7,Channel_8,Channel_9),
					null,
					HTMLColor,
					Channel_5,
					Channel_6,
					Channel_7,
					Channel_8,
					Channel_9
			FROM
					ReferenceChartData

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
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH


GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CP].[UpdateReferenceChart]'))
	exec('CREATE PROCEDURE [CP].[UpdateReferenceChart] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CP].[UpdateReferenceChart]
 
 @ReferenceChartTypeID		INT
,@ChartName					NVARCHAR(27)
,@NumOfRows					INT
,@NumOfCols					INT
,@InstrumentTypeID			INT
,@IsUsed					BIT
,@ChartPreview				VARBINARY(MAX)
,@OriginalChart				VARBINARY(MAX)
,@IsSync1D_ISO				TINYINT
,@IsSync1D_G7				TINYINT
,@Validation_1D				TINYINT
,@IsSync4D					TINYINT
,@Validation_4D				TINYINT
,@IsSync4D_Plus				TINYINT
,@Validation_4DPlus			TINYINT
,@IsDLPMultiColor			TINYINT
,@Validation_4D_Mutli_Color	TINYINT
,@IsSupportMulticolor BIT=NULL
,@TotalNoColor TINYINT=NULL
,@Channel_5 TINYINT=NULL
,@Channel_6 TINYINT=NULL
,@Channel_7 TINYINT=NULL
,@Channel_8 TINYINT=NULL
,@Channel_9 TINYINT=NULL
,@IsSync1DMultiColor_ISO			TINYINT
,@IsSync1DMultiColor_G7			TINYINT
,@Validation_Sync1D_Multi_Color	TINYINT
,@ChartSetting				NVARCHAR(MAX)
AS
BEGIN
	
	IF EXISTS (SELECT * FROM CP.ReferenceChartType Where ReferenceChartTypeID <> @ReferenceChartTypeID AND Upper(ChartName) = Upper(@ChartName) AND InstrumentTypeID = @InstrumentTypeID)  
		BEGIN  
			Return -1
		END

		
	BEGIN TRY
		BEGIN TRAN
			-----SET InstrumentTypeID For Pro2 type Same as i1-Pro or i1-IO
			IF (@InstrumentTypeID = 4)
			BEGIN
				SET @InstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 5)
			BEGIN
				SET @InstrumentTypeID = 3;
			END

			IF @ChartPreview IS NULL AND @OriginalChart IS NULL
				BEGIN
					UPDATE	CP.ReferenceChartType
						SET 
							 ChartName = @ChartName	
							,NumOfRows = @NumOfRows		
							,NumOfCols = @NumOfCols
							,InstrumentTypeID = @InstrumentTypeID	
							,IsSync1D_ISO = @IsSync1D_ISO
							,IsSync1D_G7 = @IsSync1D_G7
							,Validation_1D = @Validation_1D
							,IsSync4D = @IsSync4D
							,Validation_4D = @Validation_4D			
							,IsUsed	= @IsUsed
							,IsSync4D_Plus=@IsSync4D_Plus
							,Validation_4DPlus=@Validation_4DPlus
							,IsDLPMultiColor=@IsDLPMultiColor
							,Validation_4D_Mutli_Color=@Validation_4D_Mutli_Color
							,IsSupportMulticolor=@IsSupportMulticolor
							,TotalNoColor=@TotalNoColor
							,Channel_5=@Channel_5
							,Channel_6=@Channel_6
							,Channel_7=@Channel_7
							,Channel_8=@Channel_8
							,Channel_9=@Channel_9
							,IsSync1DMultiColor_ISO=@IsSync1DMultiColor_ISO
							,IsSync1DMultiColor_G7=@IsSync1DMultiColor_G7
							,Validation_Sync1D_Multi_Color=@Validation_Sync1D_Multi_Color

						WHERE
							ReferenceChartTypeID = @ReferenceChartTypeID
				END
			ELSE
				BEGIN
					IF @ChartPreview IS NOT NULL
						BEGIN
							UPDATE	CP.ReferenceChartType
								SET 
									 ChartName = @ChartName	
									,NumOfRows = @NumOfRows		
									,NumOfCols = @NumOfCols
									,InstrumentTypeID = @InstrumentTypeID	
									,IsSync1D_ISO = @IsSync1D_ISO
									,IsSync1D_G7 = @IsSync1D_G7
									,Validation_1D = @Validation_1D
									,IsSync4D = @IsSync4D
									,Validation_4D = @Validation_4D									
									,IsUsed	= @IsUsed									
									,ChartPreview = @ChartPreview							
									,IsSync4D_Plus=@IsSync4D_Plus
									,Validation_4DPlus=@Validation_4DPlus
									,IsDLPMultiColor=@IsDLPMultiColor
									,Validation_4D_Mutli_Color=@Validation_4D_Mutli_Color
									,IsSupportMulticolor=@IsSupportMulticolor
									,Channel_5=@Channel_5
									,Channel_6=@Channel_6
									,Channel_7=@Channel_7
									,Channel_8=@Channel_8
									,Channel_9=@Channel_9
									,IsSync1DMultiColor_ISO=@IsSync1DMultiColor_ISO
									,IsSync1DMultiColor_G7=@IsSync1DMultiColor_G7
									,Validation_Sync1D_Multi_Color=@Validation_Sync1D_Multi_Color

								WHERE
									ReferenceChartTypeID = @ReferenceChartTypeID
						END
					IF @OriginalChart IS NOT NULL
						BEGIN
							UPDATE	CP.ReferenceChartType
								SET 
									 ChartName = @ChartName	
									,NumOfRows = @NumOfRows		
									,NumOfCols = @NumOfCols
									,InstrumentTypeID = @InstrumentTypeID	
									,IsSync1D_ISO = @IsSync1D_ISO
									,IsSync1D_G7 = @IsSync1D_G7
									,Validation_1D = @Validation_1D
									,IsSync4D = @IsSync4D
									,Validation_4D = @Validation_4D							
									,IsUsed	= @IsUsed									
									,OriginalChart = @OriginalChart	
									,IsSync4D_Plus=@IsSync4D_Plus
									,Validation_4DPlus=@Validation_4DPlus
									,IsDLPMultiColor=@IsDLPMultiColor
									,Validation_4D_Mutli_Color=@Validation_4D_Mutli_Color
									,IsSupportMulticolor=@IsSupportMulticolor
									,Channel_5=@Channel_5
									,Channel_6=@Channel_6
									,Channel_7=@Channel_7
									,Channel_8=@Channel_8
									,Channel_9=@Channel_9
									,IsSync1DMultiColor_ISO=@IsSync1DMultiColor_ISO
									,IsSync1DMultiColor_G7=@IsSync1DMultiColor_G7
									,Validation_Sync1D_Multi_Color=@Validation_Sync1D_Multi_Color

								WHERE
									ReferenceChartTypeID = @ReferenceChartTypeID
						END
				END
				
				IF @ChartSetting IS NOT NULL
				BEGIN
					IF EXISTS(SELECT * FROM CP.ChartSetting WHERE ReferenceChartTypeID = @ReferenceChartTypeID)
					BEGIN
						UPDATE CP.ChartSetting SET ChartSetting = @ChartSetting WHERE ReferenceChartTypeID = @ReferenceChartTypeID
					END
					ELSE
					BEGIN
						DECLARE @ChartSettingId TINYINT
						SELECT
							@ChartSettingId = MAX(ChartSettingID) + 1 
						FROM CP.ChartSetting WITH (UPDLOCK, HOLDLOCK)
						INSERT INTO CP.ChartSetting(ChartSettingID, ChartSetting, ReferenceChartTypeID) 
							VALUES(@ChartSettingId, @ChartSetting, @ReferenceChartTypeID)					
					END										
				END

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
		RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
	END CATCH

END





GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CP].[GetReferenceChartList]'))
	exec('CREATE PROCEDURE [CP].[GetReferenceChartList] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CP].[GetReferenceChartList]
 @ReferenceChartTypeID INT

AS    
BEGIN 

IF ISNULL(@ReferenceChartTypeID,0) = 0 
BEGIN  
	SELECT 
		RCT.ReferenceChartTypeID,
		RCT.ChartName,
		RCT.NumOfRows,
		RCT.NumOfCols,
		(CAST (RCT.NumOfRows As Decimal(9,0)) * RCT.NumOfCols) As TotalPatches,
		RCT.InstrumentTypeID,
		IST.InstrumentType AS InstrumentName,		
		RCT.IsUsed,		
		RCT.ChartPreview,		
		RCT.OriginalChart,
		RCT.IsSync1D_ISO,
		RCT.IsSync1D_G7,
		RCT.Validation_1D,
		RCT.IsSync4D,
		RCT.Validation_4D,
		RCT.IsSync4D_Plus,
		RCT.Validation_4DPlus,
		RCT.IsDLPMultiColor,
		RCT.Validation_4D_Mutli_Color,
		RCT.IsSupportMulticolor,
		ISNULL(RCT.TotalNoColor,0) AS TotalNoColor,
		RCT.Channel_5,
		RCT.Channel_6,
		RCT.Channel_7,
		RCT.Channel_8,
		RCT.Channel_9,
		NULL AS ChartSetting,
		RCT.IsSync1DMultiColor_ISO,
		RCT.IsSync1DMultiColor_G7,
		RCT.Validation_Sync1D_Multi_Color


	FROM
		CP.ReferenceChartType  AS RCT WITH (NOLOCK) INNER JOIN
		CP.InstrumentType  AS  IST  WITH (NOLOCK) ON IST.InstrumentTypeID = RCT.InstrumentTypeID 
END  
 ELSE  
 BEGIN  
	SELECT 
		RCT.ReferenceChartTypeID,
		RCT.ChartName,
		RCT.NumOfRows,
		RCT.NumOfCols,
		(CAST (RCT.NumOfRows As Decimal(9,0)) * RCT.NumOfCols) As TotalPatches,
		RCT.InstrumentTypeID,
		IST.InstrumentType AS InstrumentName,	
		RCT.IsUsed,		
		RCT.ChartPreview,
		RCT.OriginalChart,
		RCT.IsSync1D_ISO,
		RCT.IsSync1D_G7,
		RCT.Validation_1D,
		RCT.IsSync4D,
		RCT.Validation_4D,
		RCT.IsSync4D_Plus,
		RCT.Validation_4DPlus,
		RCT.IsDLPMultiColor,
		RCT.Validation_4D_Mutli_Color,
		RCT.IsSupportMulticolor,
		ISNULL(RCT.TotalNoColor,0) AS TotalNoColor,
		RCT.Channel_5,
		RCT.Channel_6,
		RCT.Channel_7,
		RCT.Channel_8,
		RCT.Channel_9,		
		(SELECT ChartSetting FROM CP.ChartSetting WHERE ReferenceChartTypeId = RCT.ReferenceChartTypeID) AS ChartSetting,
		RCT.IsSync1DMultiColor_ISO,
		RCT.IsSync1DMultiColor_G7,
		RCT.Validation_Sync1D_Multi_Color

	FROM
		CP.ReferenceChartType AS RCT INNER JOIN
		CP.InstrumentType AS IST ON IST.InstrumentTypeID = RCT.InstrumentTypeID 
	WHERE RCT.ReferenceChartTypeID = @ReferenceChartTypeID
END
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CP].[GetReferenceChartTypeList]'))
	exec('CREATE PROCEDURE [CP].[GetReferenceChartTypeList] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CP].[GetReferenceChartTypeList] 
	@MeasurementDeviceID INT
   ,@ChartDisplayType INT
   ,@ProjectTypeID INT
   ,@MetricTargetPresetID INT
   ,@InstrumentTypeID INT
   ,@IsDLPPlus BIT=0 
   ,@LicenseTypeId TINYINT
   ,@DeviceId INT
AS
	
	DECLARE @FinalInstrumentTypeID INT;
	DECLARE @TotalNoColor INT;
	IF (@InstrumentTypeID = 0) ------For normal Measurement Setup page Reference chart Type list binding
	BEGIN
			--	NEED TO FIX THIS ISSUE FOR I1-PRO2, I1-PRO3 AND IO-PRO2, IO-PRO3 RIGHT NOW PASSING HARDCODED --
			DECLARE @MainInstrumentTypeID INT;
			
			SELECT @MainInstrumentTypeID = InstrumentTypeID FROM CP.MeasurementDevice WHERE MeasurementDeviceID = @MeasurementDeviceID
			IF (@MainInstrumentTypeID = 4 OR @MainInstrumentTypeID = 9)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@MainInstrumentTypeID = 5 OR @MainInstrumentTypeID = 10)
			BEGIN
				SET @FinalInstrumentTypeID = 3;
			END
			ELSE IF (@MainInstrumentTypeID = 7)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@MainInstrumentTypeID = 8)
			BEGIN
				SET @FinalInstrumentTypeID = 2;
			END
			
			-- END --

			IF (@ChartDisplayType = 1)
			BEGIN
				--ChartDisplayType = 1 is Profile Chart
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						IF(@LicenseTypeId = 13)---When project type is Sync1DMulticolor
						BEGIN
							   SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)))
										
											
									
								END
								IF (@TotalNoColor = 6)
								BEGIN
										SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId											
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0))
												)
								END
								IF (@TotalNoColor = 7)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))

													)
												)
								END
								IF (@TotalNoColor = 8)
								BEGIN
												SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0))

												)
											)			
								END
								IF (@TotalNoColor = 9)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_G7 = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_8,0))
												)
										)	
								END
						END
						ELSE
						BEGIN
		
								SELECT 
									RCT.ReferenceChartTypeID, ChartName, ChartPreview
								FROM
									CP.ReferenceChartType  AS RCT INNER JOIN
									CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
									AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
								WHERE				
									InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
									AND IsSync1D_G7 = 1 AND IsUsed = 1
						END
				END
				ELSE IF (@ProjectTypeID = 2) ----Sync1D ISO Project Type
				BEGIN
				
						IF(@LicenseTypeId = 13)---When project type is Sync1DMulticolor
						BEGIN
								SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										 
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)))
										
											
									
								END
								IF (@TotalNoColor = 6)
								BEGIN
										 
										SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId											
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0))
												)
								END
								IF (@TotalNoColor = 7)
								BEGIN
											
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0))

													)
												)
								END
								IF (@TotalNoColor = 8)
								BEGIN
											 
											SELECT RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0))

												)
											)			
								END
								IF (@TotalNoColor = 9)
								BEGIN
											SELECT 
											RCT.ReferenceChartTypeID, ChartName, ChartPreview
										FROM
											CP.ReferenceChartType  AS RCT 
											INNER JOIN CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID 
											INNER JOIN CP.Device AS DC ON  ISNULL(DC.TotalNoColor,0) = ISNULL(RCT.TotalNoColor,0)
											AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
										WHERE				
											RCT.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)											
											AND ((RCT.IsSync1DMultiColor_ISO = 1 AND RCT.IsUsed=1)) AND DC.DeviceID=@DeviceId																						
											AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RCT.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RCT.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RCT.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RCT.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RCT.Channel_8,0))
												)
										)	
								END
						   
						END
						ELSE
						BEGIN
								SELECT 
									RCT.ReferenceChartTypeID, ChartName, ChartPreview
								FROM
									CP.ReferenceChartType  AS RCT INNER JOIN
									CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
								WHERE				
									InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
									AND IsSync1D_ISO = 1 AND IsUsed = 1
						END
				
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
				END
				ELSE  ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN
						IF(@LicenseTypeId = 12)---When project type is multicolor
						BEGIN
								
								
								SELECT @TotalNoColor =TotalNoColor FROM CP.Device where DeviceID=@DeviceId
								/*
									 START:Sequancing of channel color 
									 if Totalcolor =6 then channel_5 and channel_6
									 need to compare both way with Device and referencechart table.								 
								*/  
								IF (@TotalNoColor = 5)
								BEGIN
										
										SELECT 
											RC.ReferenceChartTypeID, RC.ChartName, RC.ChartPreview
										FROM
											CP.ReferenceChartType  AS RC
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RC.TotalNoColor,0) 					   
										WHERE				
											RC.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RC.IsDLPMultiColor = 1 AND RC.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND ((ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)))
								END
								IF (@TotalNoColor = 6)
								BEGIN
										SELECT 
											RC.ReferenceChartTypeID, RC.ChartName, RC.ChartPreview
										FROM
											CP.ReferenceChartType  AS RC
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RC.TotalNoColor,0) 					   
										WHERE				
											RC.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RC.IsDLPMultiColor = 1 AND RC.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND (
													(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0))
																		OR 
													(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_5,0))
												)
									END
								IF (@TotalNoColor = 7)
								BEGIN
										SELECT 
											RC.ReferenceChartTypeID, RC.ChartName, RC.ChartPreview
										FROM
											CP.ReferenceChartType  AS RC
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RC.TotalNoColor,0) 					   
										WHERE				
											RC.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RC.IsDLPMultiColor = 1 AND RC.IsUsed=1)) AND DC.DeviceID=@DeviceId
											AND (
													(
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_5,0))									
														 						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_6,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_6,0))

													)
												)
									END
								IF (@TotalNoColor = 8)
								BEGIN
										SELECT 
											RC.ReferenceChartTypeID, RC.ChartName, RC.ChartPreview
										FROM
											CP.ReferenceChartType  AS RC
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RC.TotalNoColor,0) 					   
										WHERE				
											RC.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RC.IsDLPMultiColor = 1 AND RC.IsUsed=1)) AND DC.DeviceID=@DeviceId
										AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0))
																	    				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0))									
														 								OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_5,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0))
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_6,0))														
																						OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_7,0))

												)
											)				
									END
								IF (@TotalNoColor = 9)
								BEGIN
										SELECT 
											RC.ReferenceChartTypeID, RC.ChartName, RC.ChartPreview
										FROM
											CP.ReferenceChartType  AS RC
											INNER JOIN CP.Device AS DC ON 
											ISNULL(DC.TotalNoColor,0) = ISNULL(RC.TotalNoColor,0) 					   
										WHERE				
											RC.InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
											AND ((RC.IsDLPMultiColor = 1 AND RC.IsUsed=1)) AND DC.DeviceID=@DeviceId
										AND (
												(
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_6,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_7,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_8,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_5,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_9,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_5,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))													
																				OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_9,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_6,0))													
																			  OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_9,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_9,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_8,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_7,0))
																			OR 
														(ISNULL(DC.Channel_5,0) = ISNULL(RC.Channel_5,0)  AND  ISNULL(DC.Channel_6,0) = ISNULL(RC.Channel_6,0) AND  ISNULL(DC.Channel_7,0) = ISNULL(RC.Channel_7,0) AND  ISNULL(DC.Channel_8,0) = ISNULL(RC.Channel_9,0) AND  ISNULL(DC.Channel_9,0) = ISNULL(RC.Channel_8,0))
												)
										)			
									END
								
								/*
									END:Sequancing of channel color 
									if Totalcolor =6 then channel_5 and channel_6
									need to compare both way with Device and referencechart table.
								*/  	

						END							
						ELSE---WHen we have only 4D project
						BEGIN
							SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM CP.ReferenceChartType WHERE InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
						AND ((IsSync4D = 1 AND IsUsed = 1))
						END
						
				END
			END
			ELSE IF(@ChartDisplayType = 3)
			BEGIN
			
				--ChartDisplayType = 3 is Validation Chart
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_1D >= 0 AND IsUsed = 1
				END
				ELSE IF (@ProjectTypeID = 2) ----Sync1D ISO Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_1D >= 0 AND IsUsed = 1
				END   
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							SELECT @MainInstrumentTypeID,@FinalInstrumentTypeID
				END
				IF (@ProjectTypeID = 3 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type Validation project
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND Validation_4DPlus = 1 AND IsUsed = 1
							SELECT @MainInstrumentTypeID,@FinalInstrumentTypeID
				END
				ELSE ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN

					IF(@LicenseTypeId = 12)---When project type is multicolor
					BEGIN
							
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND ((Validation_4D_Mutli_Color = 1 AND IsUsed=1))
					END
					ELSE
					BEGIN ---When we have only 4D project
							
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND ((Validation_4D >= 0 AND IsUsed = 1))
					END
				
				END
			END
			ELSE
			BEGIN
				--Here we are supposed to show Profile and Optimization Chart types
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 0) ----Sync1D G7 Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_G7 >= 1 AND IsUsed = 1
				END
				ELSE IF (@ProjectTypeID = 2)  ----Sync1D ISO Project Type
				BEGIN
						SELECT 
							RCT.ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  AS RCT INNER JOIN
							CPSyncV.ReferenceChartPreset AS RCP ON RCT.ReferenceChartTypeID = RCP.ReferenceChartTypeID AND RCP.MetricTargetPresetID = @MetricTargetPresetID 
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync1D_ISO >= 1 AND IsUsed = 1
				END
				IF (@ProjectTypeID = 1 AND @IsDLPPlus = 1) ----DLPPLUS G7 Project Type
				BEGIN
					SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							AND IsSync4D_Plus = 1 AND IsUsed = 1
				END
				ELSE   ----Sync4D, Ink Optimization or multicolor Project Type
				BEGIN
						SELECT 
							ReferenceChartTypeID, ChartName, ChartPreview
						FROM
							CP.ReferenceChartType  
						WHERE				
							InstrumentTypeID IN (@MainInstrumentTypeID,@FinalInstrumentTypeID)
							 AND ((IsSync4D >= 1 AND IsUsed = 1)) 
							---OR (IsDLPMultiColor >= 1 AND IsUsed = 1)) ---Don't display Multicolor charts for profiler charts
				END
			END
	END
	ELSE ----For Getting ReferenceChartType list for Admin Patch Level Toletance page (Display Sync4D validation charts by default)
	BEGIN	
	

		IF (@InstrumentTypeID = 4 OR @InstrumentTypeID = 9)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 5 OR @InstrumentTypeID = 10)
			BEGIN
				SET @FinalInstrumentTypeID = 3;
			END
			ELSE IF (@InstrumentTypeID = 7)
			BEGIN
				SET @FinalInstrumentTypeID = 1;
			END
			ELSE IF (@InstrumentTypeID = 8)
			BEGIN
				SET @FinalInstrumentTypeID = 2;
			END

		SELECT 
			ReferenceChartTypeID, ChartName
		FROM
			CP.ReferenceChartType
		WHERE				
			InstrumentTypeID IN (@InstrumentTypeID,@FinalInstrumentTypeID)
			AND ((Validation_4D >= 0 AND IsUsed = 1) OR (Validation_4D_Mutli_Color >= 1 AND IsUsed = 1))
	END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSync4D].[UpdateProject]'))
	exec('CREATE PROCEDURE [CPSync4D].[UpdateProject] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CPSync4D].[UpdateProject]
	@ProjectID INT,
	@PrintStandardTypeID INT,  
	@ProjectName NVARCHAR(30),  
	@Media NVARCHAR(50),  
	@Colorant NVARCHAR(50),  
	@RipSettings NVARCHAR(50),  
	@TechnicalName NVARCHAR(30),  
	@Notes NVARCHAR(500),  
	@PrintingType INT,
	@InkOrder INT,  
	@FountainSolution NVARCHAR(100),  
	@Blanket NVARCHAR(100),  
	@BlanketAge NVARCHAR(100),  
	@Screening NVARCHAR(100),  
	@Coating NVARCHAR(100),  
	@ObserverType TINYINT,  
	@Illuminant TINYINT,  
	@UVMode TINYINT,  
	@MeasurementCount TINYINT, 
    @ReadMode NVARCHAR(30),

	@MeasurementDeviceID SMALLINT,  
	@ReferenceChartTypeID TINYINT,  
	@DeviceFamilyProfilerOptionId INT,
	@ProjectOptions XML,
	@VMeasurementDeviceID SMALLINT,
	@OPTReferenceChartTypeID TINYINT,
	@VReferenceChartTypeID TINYINT,
	@EnabledValidation BIT,
	@MetricTargetPresetID INT,
	@ProjectTargetTVI XML,
	@MetricTargetPresetOptionID INT,
	@ProjectTargetLAB XML,
	@ProjectTargetDensity XML,
	@ProjectTargetHueAngle XML,
	@ProjectTypeId TINYINT,

	@VUVMode TINYINT,  
	@VMeasurementCount TINYINT, 
    @VReadMode NVARCHAR(30),
	@DensityType TINYINT,
    @PCSDataType TINYINT,
	@ReTargetProjectID INT,
	@EnabledPaperScaling BIT,
	@NewPaper_White_L DECIMAL(6,2),
	@NewPaper_White_a DECIMAL(6,2),
	@NewPaper_White_b DECIMAL(6,2),
	@NewPaper_Black_L DECIMAL(6,2),
	@NewPaper_Black_a DECIMAL(6,2),
	@NewPaper_Black_b DECIMAL(6,2),
	@IsCustomProfilerOption BIT,
	@IsCustomPlugin BIT

AS  
	BEGIN TRY  
		BEGIN TRAN  
			
			DECLARE @LicenseTypeId int
			SELECT @LicenseTypeId = LicenseTypeId FROM CPSync4D.Project WHERE ProjectID = @ProjectID
			
			UPDATE
				CPSync4D.Project  
			SET
				PrintStandardTypeID = @PrintStandardTypeID,  
				ProjectName=@ProjectName,  
				BaseDeviceFamilyProfilerOption=@DeviceFamilyProfilerOptionId,
				EnabledValidation = @EnabledValidation,
				MetricTargetPresetID = @MetricTargetPresetOptionID,
				DensityType = @DensityType,
				PCSDataType = @PCSDataType,
				ReTargetProjectID = @ReTargetProjectID,
				EnabledPaperScaling = @EnabledPaperScaling,
				NewPaper_White_L = @NewPaper_White_L,
				NewPaper_White_a = @NewPaper_White_a,
				NewPaper_White_b = @NewPaper_White_b,
				NewPaper_Black_L = @NewPaper_Black_L,
				NewPaper_Black_a = @NewPaper_Black_a,
				NewPaper_Black_b = @NewPaper_Black_b,
				LastActiveDate = GETDATE()
				
			WHERE
				ProjectID =	@ProjectID


			UPDATE
				CPSync4D.PrintCondition  
			SET
				Media = @Media,  
				Colorant = @Colorant,  
				RipSettings = @RipSettings,  
				TechnicianName = @TechnicalName,  
				Notes  = @Notes,
				PrintingType = @PrintingType,
				InkOrder = @InkOrder,  
				FountainSolution = @FountainSolution,  
				Blanket = @Blanket,  
				BlanketAge = @BlanketAge,  
				Screening = @Screening,
				Coating = @Coating,
				IsCustomProfilerOption = @IsCustomProfilerOption
				,IsCustomPlugin = @IsCustomPlugin
			WHERE
				ProjectID = @ProjectID

			UPDATE
				CPSync4D.MeasurementSetting  
			SET
				ObserverType=@ObserverType,  
				Illuminant=@Illuminant,  				
				UVMode = @UVMode,
				MeasurementCount = @MeasurementCount,
				ReadMode = @ReadMode,
				MeasurementDeviceID=@MeasurementDeviceID,  
				ReferenceChartTypeID  =@ReferenceChartTypeID,
				VMeasurementDeviceID = @VMeasurementDeviceID,
				OPTReferenceChartTypeID = @OPTReferenceChartTypeID,
				VReferenceChartTypeID = @VReferenceChartTypeID,
				MetricTargetPresetID = CASE @MetricTargetPresetID
											WHEN 0 THEN NULL
											ELSE @MetricTargetPresetID
										END,
				VUVMode = @VUVMode,
				VMeasurementCount = @VMeasurementCount,
				VReadMode = @VReadMode
				--,PluginType = @PluginType
			WHERE
				ProjectID = @ProjectID

			
	
	IF (@LicenseTypeId = 1  OR @LicenseTypeId = 13) -- SYNC1D,SYNC1D Multi Color
	BEGIN
		
		--Insert into ProjectOption
		DELETE FROM [CPSync].[ProjectOption]
		WHERE ProjectID = @ProjectID
		
		 ;WITH ProjectOptions AS  
		  (            
		   SELECT             
			[ProjectOptionXml].value('@ProfileTypeID', 'INT') AS ProfileTypeID,  
			[ProjectOptionXml].value('@ProfilerOptionID', 'INT') AS ProfilerOptionID,  
			[ProjectOptionXml].value('@ProfilerOptionValue', 'NVARCHAR(100)') AS ProfilerOptionValue  
		   FROM  
			@ProjectOptions.  
			 nodes('/ProjectOptions/ProjectOption') p([ProjectOptionXml])  
		  )   
		  
		  INSERT INTO CPSync.ProjectOption  
		   (  
			ProjectID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		   )  
		  SELECT  
			@ProjectID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		  FROM  
			ProjectOptions  
	  
	
		--Insert into ProjectTargetLAB
		DELETE FROM [CPSync].[ProjectTargetLAB]
		WHERE ProjectID = @ProjectID
		
		;WITH ProjectTargetLAB AS  
		  (            
		   SELECT             
			[ProjectTargetLABXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetLABXml].value('@TargetL', 'DECIMAL(6,2)') AS TargetL,  
			[ProjectTargetLABXml].value('@TargetA', 'DECIMAL(6,2)') AS TargetA,  
			[ProjectTargetLABXml].value('@TargetB', 'DECIMAL(6,2)') AS TargetB 
		   FROM  
			@ProjectTargetLAB.  
			 nodes('/ProjectTargetLAB/TargetLAB') p([ProjectTargetLABXml])  
		  ) 
		  
		INSERT INTO [CPSync].[ProjectTargetLAB]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[TargetL*]
			   ,[Targeta*]
			   ,[Targetb*])
		 SELECT
				@ProjectID
				,PatchTypeID
				,TargetL
				,TargetA
				,TargetB
			FROM ProjectTargetLAB
		
		
		--Insert into ProjectTargetDensity
		DELETE FROM [CPSync].[ProjectTargetDensity]
		WHERE ProjectID = @ProjectID
		
		;WITH ProjectTargetDensity AS  
		  (            
		   SELECT             
			[ProjectTargetDensityXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetDensityXml].value('@Target', 'DECIMAL(6,2)') AS Target
		   FROM  
			@ProjectTargetDensity.  
			 nodes('/ProjectTargetDensity/TargetDensity') p([ProjectTargetDensityXml])  
		  )  
	
		INSERT INTO [CPSync].[ProjectTargetDensity]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[Target])
		  SELECT
			   @ProjectID
			   ,PatchTypeID
			   ,Target
		FROM
			ProjectTargetDensity
		
		--Insert into ProjectTargetTVI
		
		DELETE FROM [CPSync].[ProjectTargetTVI]
			WHERE
				ProjectID = @ProjectID
		
		;WITH ProjectTargetTVI AS  
		  (            
		   SELECT             
			[ProjectTargetTVIXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetTVIXml].value('@TVI', 'DECIMAL(6,2)') AS TVI
		   FROM  
			@ProjectTargetTVI.  
			 nodes('/ProjectTargetTVI/TargetTVI') p([ProjectTargetTVIXml])  
		  )  
	
		INSERT INTO [CPSync].[ProjectTargetTVI]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[TVI])
		 SELECT
			   @ProjectID
			   ,PatchTypeID
			   ,TVI  
		FROM
			ProjectTargetTVI

		--Insert into ProjectTargetFlexoHueAngle

		DELETE FROM [CPSync].[ProjectTargetFlexoHueAngle]
		WHERE ProjectID = @ProjectID
		
		IF @PrintingType = 1 ----If Sync1D Flexo project
		BEGIN
			;WITH ProjectTargetHueAngle AS  
			  (            
			   SELECT             
				[ProjectTargetHueAngleXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
				[ProjectTargetHueAngleXml].value('@Target', 'DECIMAL(6,2)') AS Target
			   FROM  
				@ProjectTargetHueAngle.  
				 nodes('/ProjectTargetHueAngle/TargetHueAngle') p([ProjectTargetHueAngleXml])  
			  )  
		
			INSERT INTO [CPSync].[ProjectTargetFlexoHueAngle]
				   ([ProjectID]
				   ,[PatchTypeID]
				   ,[Target])
			  SELECT
				   @ProjectID
				   ,PatchTypeID
				   ,Target
			FROM
				ProjectTargetHueAngle
		END

	END	
	ELSE
	BEGIN
			DELETE FROM
				CPSync4D.ProjectOption
			WHERE
				ProjectID = @ProjectID
	
				;WITH ProjectOptions AS  
			(            
				SELECT             
					[ProjectOptionXml].value('@ProfileTypeID', 'INT') AS ProfileTypeID,  
					[ProjectOptionXml].value('@ProfilerOptionID', 'INT') AS ProfilerOptionID,  
					[ProjectOptionXml].value('@ProfilerOptionValue', 'NVARCHAR(100)') AS ProfilerOptionValue  
				FROM  
					@ProjectOptions.nodes('/ProjectOptions/ProjectOption') p([ProjectOptionXml])  
			)

			

			INSERT INTO
				CPSync4D.ProjectOption(  
					ProjectID,  
					ProfileTypeID,  
					ProfilerOptionID,  
					ProfilerOptionValue)  
				SELECT  
					@ProjectID,  
					ProfileTypeID,  
					ProfilerOptionID,  
					ProfilerOptionValue  
				FROM  
					ProjectOptions  

				IF @LicenseTypeId = 11 --DLPPLus
				BEGIN
						--Insert into ProjectTargetLAB
					DELETE FROM [CPSync4DPlus].[ProjectTargetLAB]
					WHERE ProjectID = @ProjectID
					
					;WITH ProjectTargetLAB AS  
					  (            
					   SELECT             
						[ProjectTargetLABXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
						[ProjectTargetLABXml].value('@TargetL', 'DECIMAL(6,2)') AS TargetL,  
						[ProjectTargetLABXml].value('@TargetA', 'DECIMAL(6,2)') AS TargetA,  
						[ProjectTargetLABXml].value('@TargetB', 'DECIMAL(6,2)') AS TargetB 
					   FROM  
						@ProjectTargetLAB.  
						 nodes('/ProjectTargetLAB/TargetLAB') p([ProjectTargetLABXml])  
					  ) 
					  
					INSERT INTO [CPSync4DPlus].[ProjectTargetLAB]
						   ([ProjectID]
						   ,[PatchTypeID]
						   ,[TargetL*]
						   ,[Targeta*]
						   ,[Targetb*])
					 SELECT
							@ProjectID
							,PatchTypeID
							,TargetL
							,TargetA
							,TargetB
						FROM ProjectTargetLAB
				END
	END

	
	---- Update table entry into "[CP].[MeasurementPluginStatus]" for this ProjectID for EntryDate, because Project just got updated
	 -----If there are any existing projects with newly selected serial number device entries available then delete those first before updating this entry-------
		IF EXISTS (SELECT * FROM CP.MeasurementPluginStatus WHERE ProjectID != @ProjectID  
			AND MeaDeviceSerialNo IN (SELECT [SerialNumber] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] IN (@MeasurementDeviceID, @VMeasurementDeviceID)) 
			AND InstrumentTypeID IN (SELECT [InstrumentTypeID] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] IN (@MeasurementDeviceID, @VMeasurementDeviceID)))
		BEGIN
			DELETE FROM CP.MeasurementPluginStatus WHERE ProjectID != @ProjectID 
				AND MeaDeviceSerialNo IN (SELECT [SerialNumber] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] IN (@MeasurementDeviceID, @VMeasurementDeviceID)) 
				AND InstrumentTypeID IN (SELECT [InstrumentTypeID] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] IN (@MeasurementDeviceID, @VMeasurementDeviceID))
		END

		UPDATE [CP].[MeasurementPluginStatus]
		SET [EntryDate] = GETDATE(),
			[MeaDeviceSerialNo] = (SELECT [SerialNumber] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] = 
									CASE 
										WHEN [ProfileType] = 'Validation' THEN @VMeasurementDeviceID
										ELSE @MeasurementDeviceID
									END
									AND [ProjectID] = @ProjectID),
			[InstrumentTypeID] = (SELECT [InstrumentTypeID] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] = 
									CASE 
										WHEN [ProfileType] = 'Validation' THEN @VMeasurementDeviceID
										ELSE @MeasurementDeviceID
									END
									AND [ProjectID] = @ProjectID)
		WHERE [ProjectID] = @ProjectID


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
	END CATCH
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSync4D].[CreateProject]'))
	exec('CREATE PROCEDURE [CPSync4D].[CreateProject] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CPSync4D].[CreateProject]  
 @CPDeviceID INT,   
 @PrintStandardTypeID INT,  
 @ProjectName NVARCHAR(30),  
 @CreateDate DATETIME,  
 @Media NVARCHAR(50),  
 @Colorant NVARCHAR(50),  
 @RipSettings NVARCHAR(50),  
 @TechnicalName NVARCHAR(30),  
 @Notes NVARCHAR(500),

 @PrintingType INT,  
 @InkOrder INT,  
 @FountainSolution NVARCHAR(100),  
 @Blanket NVARCHAR(100),  
 @BlanketAge NVARCHAR(100),  
 @Screening NVARCHAR(100),  
 @Coating NVARCHAR(100),  
 @ObserverType TINYINT,  
 @Illuminant TINYINT,  
 @UVMode TINYINT,  
 @MeasurementCount TINYINT, 
 @ReadMode NVARCHAR(30),

 @MeasurementDeviceID SMALLINT,  
 @ReferenceChartTypeID TINYINT,  
 @Status INT,
 @StartDateTime DATETIME,
 @DeviceFamilyProfilerOptionId INT,
 @ProjectOptions XML, 
 @LicenseTypeID INT, 
 @ProjectID INT OUTPUT  ,
 @VMeasurementDeviceID SMALLINT,
 @OPTReferenceChartTypeID TINYINT,
 @VReferenceChartTypeID TINYINT,
 @EnabledValidation BIT,
 @MetricTargetPresetID INT,
 @ProjectTargetTVI XML,
 @MetricTargetPresetOptionID INT,
 @ProjectTargetLAB XML,
 @ProjectTargetDensity XML,
 @ProjectTargetHueAngle XML,
 @ProjectTypeId TINYINT,

 @VUVMode TINYINT,  
 @VMeasurementCount TINYINT, 
 @VReadMode NVARCHAR(30),
 @DensityType TINYINT,
 @PCSDataType TINYINT,
 @ReTargetProjectID INT,
 @EnabledPaperScaling BIT,
 @NewPaper_White_L DECIMAL(6,2),
 @NewPaper_White_a DECIMAL(6,2),
 @NewPaper_White_b DECIMAL(6,2),
 @NewPaper_Black_L DECIMAL(6,2),
 @NewPaper_Black_a DECIMAL(6,2),
 @NewPaper_Black_b DECIMAL(6,2),
 @IsCustomProfilerOption BIT,
 @IsCustomPlugin BIT


AS  
BEGIN
--	IF EXISTS (SELECT * FROM CPSync4D.SourceProfile Where Upper(ProfileName) = cast(Upper(@ProjectName) as varchar) + '.icc')
--		BEGIN  
--			Return -1
--		END   

	BEGIN TRY  
	 BEGIN TRAN  
	  INSERT INTO CPSync4D.Project  
	   (  
		CPDeviceID,     
		PrintStandardTypeID,  
		DestinationProfile,  
		ProjectName,  
		CreateDate,  
		CurrDLPID,
		BaseDeviceFamilyProfilerOption,
		IsUsed,
		LicenseTypeID,
		EnabledValidation,
		MetricTargetPresetID,
		DensityType,
		PCSDataType,
		ReTargetProjectID,
		EnabledPaperScaling,
		NewPaper_White_L,
		NewPaper_White_a,
		NewPaper_White_b,
		NewPaper_Black_L,
		NewPaper_Black_a,
		NewPaper_Black_b,
		LastActiveDate
	   )  
	  VALUES  
	   (  
		@CPDeviceID,     
		@PrintStandardTypeID,  
		NULL,  
		@ProjectName,  
		@CreateDate,  
		NULL,  
		@DeviceFamilyProfilerOptionId,
		1,
		@LicenseTypeID,
		@EnabledValidation,
		@MetricTargetPresetOptionID,
		@DensityType,
		@PCSDataType,
		@ReTargetProjectID,
		@EnabledPaperScaling,
		@NewPaper_White_L,
		@NewPaper_White_a,
		@NewPaper_White_b,
		@NewPaper_Black_L,
		@NewPaper_Black_a,
		@NewPaper_Black_b,
		GETDATE()
	   )  
	     
	  SET @ProjectID = @@IDENTITY  
	    
	  INSERT INTO CPSync4D.PrintCondition  
	   (  
		ProjectID,  
		Media,  
		Colorant,  
		RipSettings,  
		TechnicianName,  
		Notes,
		PrintingType,
		InkOrder,  
		FountainSolution,  
		Blanket,  
		BlanketAge,  
		Screening,
		Coating,
		IsCustomProfilerOption,
		IsCustomPlugin
	   )  
	  VALUES  
	   (  
		@ProjectID,  
		@Media,  
		@Colorant,  
		@RipSettings,  
		@TechnicalName,  
		@Notes,
		@PrintingType,
		@InkOrder,  
		@FountainSolution,  
		@Blanket,  
		@BlanketAge,  
		@Screening,
		@Coating,
		@IsCustomProfilerOption,
		@IsCustomPlugin
	   )  
	    
	  INSERT INTO CPSync4D.MeasurementSetting  
	   (  
		ProjectID,  
		ObserverType,  
		Illuminant,  
		UVMode,
		MeasurementCount,
		ReadMode,
		MeasurementDeviceID,  
		ReferenceChartTypeID ,
		VMeasurementDeviceID,
		OPTReferenceChartTypeID,
		VReferenceChartTypeID,
		MetricTargetPresetID,
		VUVMode,
		VMeasurementCount,
		VReadMode
		--,PluginType
	   )  
	  VALUES  
	   (  
		@ProjectID,  
		@ObserverType,  
		@Illuminant,  
		@UVMode,
		@MeasurementCount,
		@ReadMode,
		@MeasurementDeviceID,  
		@ReferenceChartTypeID,
		@VMeasurementDeviceID,
		@OPTReferenceChartTypeID,
		@VReferenceChartTypeID,
		CASE @MetricTargetPresetID
			WHEN 0 THEN NULL
			ELSE @MetricTargetPresetID
		END,

		@VUVMode,
		@VMeasurementCount,
		@VReadMode
		--,@PluginType
	   )  
	  
	  IF (@LicenseTypeID = 1 OR @LicenseTypeID = 13) -- Sync1D and Sync1DMultiColor
	  BEGIN

		--Insert into ProjectOption
		;WITH ProjectOptions AS  
		  (            
		   SELECT             
			[ProjectOptionXml].value('@ProfileTypeID', 'INT') AS ProfileTypeID,  
			[ProjectOptionXml].value('@ProfilerOptionID', 'INT') AS ProfilerOptionID,  
			[ProjectOptionXml].value('@ProfilerOptionValue', 'NVARCHAR(100)') AS ProfilerOptionValue  
		   FROM  
			@ProjectOptions.  
			 nodes('/ProjectOptions/ProjectOption') p([ProjectOptionXml])  
		  )   
		  
		  INSERT INTO CPSync.ProjectOption  
		   (  
			ProjectID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		   )  
		  SELECT  
			@ProjectID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		  FROM  
			ProjectOptions  
	  
		--Insert into ProjectTargetLAB
		;WITH ProjectTargetLAB AS  
		  (            
		   SELECT             
			[ProjectTargetLABXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetLABXml].value('@TargetL', 'DECIMAL(6,2)') AS TargetL,  
			[ProjectTargetLABXml].value('@TargetA', 'DECIMAL(6,2)') AS TargetA,  
			[ProjectTargetLABXml].value('@TargetB', 'DECIMAL(6,2)') AS TargetB 
		   FROM  
			@ProjectTargetLAB.  
			 nodes('/ProjectTargetLAB/TargetLAB') p([ProjectTargetLABXml])  
		  ) 
		  
		INSERT INTO [CPSync].[ProjectTargetLAB]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[TargetL*]
			   ,[Targeta*]
			   ,[Targetb*])
		 SELECT
				@ProjectID
				,PatchTypeID
				,TargetL
				,TargetA
				,TargetB
			FROM ProjectTargetLAB
				
				
		
		--Insert into ProjectTargetDensity
		;WITH ProjectTargetDensity AS  
		  (            
		   SELECT             
			[ProjectTargetDensityXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetDensityXml].value('@Target', 'DECIMAL(6,2)') AS Target
		   FROM  
			@ProjectTargetDensity.  
			 nodes('/ProjectTargetDensity/TargetDensity') p([ProjectTargetDensityXml])  
		  )  

		INSERT INTO [CPSync].[ProjectTargetDensity]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[Target])
		  SELECT
			   @ProjectID
			   ,PatchTypeID
			   ,Target 
		FROM
			ProjectTargetDensity
		
		--Insert into ProjectTargetTVI
		;WITH ProjectTargetTVI AS  
		  (            
		   SELECT             
			[ProjectTargetTVIXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetTVIXml].value('@TVI', 'DECIMAL(6,2)') AS TVI
		   FROM  
			@ProjectTargetTVI.  
			 nodes('/ProjectTargetTVI/TargetTVI') p([ProjectTargetTVIXml])  
		  )  
		
		INSERT INTO [CPSync].[ProjectTargetTVI]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[TVI])
		 SELECT
			   @ProjectID
			   ,PatchTypeID
			   ,TVI 
		FROM
			ProjectTargetTVI

		IF @PrintingType = 1 ----If Sync1D Flexo project
		BEGIN
			--Insert into ProjectTargetFlexoHueAngle
			;WITH ProjectTargetHueAngle AS  
			  (            
			   SELECT             
				[ProjectTargetHueAngleXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
				[ProjectTargetHueAngleXml].value('@Target', 'DECIMAL(6,2)') AS Target
			   FROM  
				@ProjectTargetHueAngle.  
				 nodes('/ProjectTargetHueAngle/TargetHueAngle') p([ProjectTargetHueAngleXml])  
			  )  

			INSERT INTO [CPSync].[ProjectTargetFlexoHueAngle]
				   ([ProjectID]
				   ,[PatchTypeID]
				   ,[Target])
			  SELECT
				   @ProjectID
				   ,PatchTypeID
				   ,Target 
			FROM
				ProjectTargetHueAngle
		END

	  END
	  ELSE IF @LicenseTypeID = 11 --- DLPPlus
	  BEGIN
		--Insert into ProjectTargetLAB
		;WITH ProjectTargetLAB AS  
		  (            
		   SELECT             
			[ProjectTargetLABXml].value('@PatchTypeID', 'TINYINT') AS PatchTypeID,  
			[ProjectTargetLABXml].value('@TargetL', 'DECIMAL(6,2)') AS TargetL,  
			[ProjectTargetLABXml].value('@TargetA', 'DECIMAL(6,2)') AS TargetA,  
			[ProjectTargetLABXml].value('@TargetB', 'DECIMAL(6,2)') AS TargetB 
		   FROM  
			@ProjectTargetLAB.  
			 nodes('/ProjectTargetLAB/TargetLAB') p([ProjectTargetLABXml])  
		  ) 
		  
		INSERT INTO [CPSync4DPlus].[ProjectTargetLAB]
			   ([ProjectID]
			   ,[PatchTypeID]
			   ,[TargetL*]
			   ,[Targeta*]
			   ,[Targetb*])
		 SELECT
				@ProjectID
				,PatchTypeID
				,TargetL
				,TargetA
				,TargetB
			FROM ProjectTargetLAB
		--Sync4D
		  ;WITH ProjectOptions AS  
		  (            
		   SELECT             
			[ProjectOptionXml].value('@ProfileTypeID', 'INT') AS ProfileTypeID,  
			[ProjectOptionXml].value('@ProfilerOptionID', 'INT') AS ProfilerOptionID,  
			[ProjectOptionXml].value('@ProfilerOptionValue', 'NVARCHAR(100)') AS ProfilerOptionValue  
		   FROM  
			@ProjectOptions.  
			 nodes('/ProjectOptions/ProjectOption') p([ProjectOptionXml])  
		  )   
		  
		  INSERT INTO CPSync4D.ProjectOption  
		   (  
			ProjectID,  
			ProfileTypeID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		   )  
		  SELECT  
			@ProjectID,  
			ProfileTypeID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		  FROM  
			ProjectOptions  

		IF @EnabledPaperScaling = 1
		BEGIN
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 3, 43,(CAST(@NewPaper_White_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 3, 44,(CAST(@NewPaper_Black_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 4, 43,(CAST(@NewPaper_White_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 4, 44,(CAST(@NewPaper_Black_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			END
	  END
	  ELSE
	  BEGIN
		--Sync4D
		  ;WITH ProjectOptions AS  
		  (            
		   SELECT             
			[ProjectOptionXml].value('@ProfileTypeID', 'INT') AS ProfileTypeID,  
			[ProjectOptionXml].value('@ProfilerOptionID', 'INT') AS ProfilerOptionID,  
			[ProjectOptionXml].value('@ProfilerOptionValue', 'NVARCHAR(100)') AS ProfilerOptionValue  
		   FROM  
			@ProjectOptions.  
			 nodes('/ProjectOptions/ProjectOption') p([ProjectOptionXml])  
		  )   
		  
		  INSERT INTO CPSync4D.ProjectOption  
		   (  
			ProjectID,  
			ProfileTypeID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		   )  
		  SELECT  
			@ProjectID,  
			ProfileTypeID,  
			ProfilerOptionID,  
			ProfilerOptionValue  
		  FROM  
			ProjectOptions  
		 
		IF @EnabledPaperScaling = 1
			BEGIN
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 3, 43,(CAST(@NewPaper_White_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 3, 44,(CAST(@NewPaper_Black_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 4, 43,(CAST(@NewPaper_White_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_White_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			   INSERT INTO CPSync4D.ProjectOption  
			   (  
				ProjectID, ProfileTypeID, ProfilerOptionID, ProfilerOptionValue  
			   ) 
			   VALUES
			   (
		    	@ProjectID, 4, 44,(CAST(@NewPaper_Black_L AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_a AS NVARCHAR(10)) + ' ' + CAST(@NewPaper_Black_b AS NVARCHAR(10)) + ' ' + 'Lab Method=ISO13655')
		       )
			END

		
	 END
	 
	 --okay to send close status same as open status as there will be no optimize run to close at this stage...   
	 IF (@LicenseTypeID = 1 OR @LicenseTypeID = 13) -- Sync1D and Sync1DMultiColor
		EXEC CPSync.CreateOptimizeRun @ProjectID,@Status,@StartDateTime,0
	ELSE
		EXEC CPSync4D.CreateOptimizeRun @ProjectID,@Status,@StartDateTime
		
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
	END CATCH
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('[CPSync4D].[GetProjectList]'))
	exec('CREATE PROCEDURE [CPSync4D].[GetProjectList] AS BEGIN SET NOCOUNT ON; END')
GO
ALTER PROCEDURE [CPSync4D].[GetProjectList] 
 @DeviceID INT
,@LicenseTypeID INT        
AS        
    
  BEGIN TRY	 
   
	  SELECT         
	   PRJ.ProjectID,    
	   PRJ.ProjectName,    
	   PRJ.CPDeviceID,    
	   PRJ.LicenseTypeID,
	   PRJ.IsUsed,    
	   PST.SourceProfileID,  
	   PRJ.EnabledValidation,  
	   SP.ProfileName,  
	   PRJ.PrintStandardTypeID,
	   PST.PrintStandard,
	   PCON.Media,    
	   PCON.Colorant,    
	   PCON.RipSettings,    
	   PRJ.MetricTargetPresetID,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
			(SELECT TOP 1 
			CASE 
					WHEN  ISNULL(PRJ.LastActiveDate,'1900-01-01') > ISNULL(SR.EndDateTime,'1900-01-01') THEN PRJ.LastActiveDate 
					ELSE SR.EndDateTime 
			END
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)

		WHEN 13 THEN ---	Sync1DMultiColor
			(SELECT TOP 1 
			CASE 
					WHEN  ISNULL(PRJ.LastActiveDate,'1900-01-01') > ISNULL(SR.EndDateTime,'1900-01-01') THEN PRJ.LastActiveDate 
					ELSE SR.EndDateTime 
			END
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)
	   
	   ELSE	   
			(SELECT TOP 1 
			CASE 
					WHEN  ISNULL(PRJ.LastActiveDate,'1900-01-01') > ISNULL(SR.EndDateTime,'1900-01-01') THEN PRJ.LastActiveDate 
					ELSE SR.EndDateTime 
			END
			FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC) END) 
	   AS CreateDateTime,
	  (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
			(SELECT TOP 1 SR.EndDateTime 
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)
	    WHEN 13 THEN ---	Sync1DMultiColor
			(SELECT TOP 1 SR.EndDateTime 
			FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC)
	   ELSE	   
			(SELECT TOP 1 SR.EndDateTime 
			FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID AND ((SR.RunName = 'Run-0') OR (SR.Status = 2)) ORDER BY SR.OptimizeRunID DESC) END) 
		AS EndDateTime,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		ISNULL((SELECT TOP 1 SR.Status FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC),1)
	   ELSE
		ISNULL((SELECT TOP 1 SR.Status FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC),1)
	   END)AS ProjStatus,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		(SELECT TOP 1 
		CASE 
			WHEN DeviceLinkProfile IS NULL THEN 0
			ELSE 1
			END			
		FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC)
	   ELSE
		(SELECT TOP 1 
		CASE 
			WHEN DeviceLinkProfile IS NULL THEN 0
			ELSE 1
			END			
		FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID ORDER BY SR.OptimizeRunID DESC)
	   END) AS HasDLP,
		(CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		ISNULL((SELECT COUNT(SR.OptimizeRunID) FROM CPSync.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID),0) 
	   ELSE
		ISNULL((SELECT COUNT(SR.OptimizeRunID) FROM CPSync4D.OptimizeRun SR WHERE SR.ProjectID = PRJ.ProjectID),0) 
	   END)AS TotalRuns,
	   (CASE PRJ.LicenseTypeId 
	   WHEN 1 THEN
		(SELECT (R-1) FROM (SELECT OptimizeRunId,RANK() OVER (ORDER BY OptimizeRunID) R FROM CPSync.OptimizeRun OPT WHERE ProjectId = PRJ.ProjectID) rankTbl WHERE rankTbl.OptimizeRunId = PRJ.CurrDlpID)
	   ELSE
		(SELECT (R-1) FROM (SELECT OptimizeRunId,RANK() OVER (ORDER BY OptimizeRunID) R FROM CPSync4D.OptimizeRun OPT WHERE ProjectId = PRJ.ProjectID) rankTbl WHERE rankTbl.OptimizeRunId = PRJ.CurrDlpID)
	   END) AS CurrentDlpNo,
	   CPSyncV.GetValidationStatus(PRJ.ProjectID, PRJ.EnabledValidation, PRJ.LicenseTypeID) AS ValidationStatus
	   ,(
		CASE 
			WHEN EXISTS (
					SELECT 1
					FROM CP.DeviceLicenseType DLT
					INNER JOIN [CP].[LicenseType] LT ON LT.LicenseTypeID = DLT.LicenseTypeID
					WHERE DeviceID = @DeviceID
						AND LT.LicenseTypeID = 10
						AND NumOfProject != 0
					)
				THEN CAST(1 AS BIT)
			ELSE CASE 
					WHEN EXISTS (
							SELECT 1
							FROM [CPSpot].[SpotProject]
							WHERE ProjectID = PRJ.ProjectID
							)
						THEN CAST(1 AS BIT)
					ELSE CAST(0 AS BIT)
					END
			END
		) AS HasSpotProject
	  FROM    
	   [CPSync4D].[Project] PRJ INNER JOIN CPSync4D.PrintCondition PCON     
		ON PCON.ProjectID = PRJ.ProjectID    
		INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
	   INNER JOIN CPSync4D.SourceProfile SP     
		ON SP.SourceProfileID = PST.SourceProfileID    		
		  WHERE    
	   PRJ.CPDeviceID = @DeviceID AND (ISNULL(@LicenseTypeID,0) = 0 OR PRJ.LicenseTypeID = @LicenseTypeID) AND
	   PRJ.IsUsed = 1   
      ORDER BY CreateDateTime DESC
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