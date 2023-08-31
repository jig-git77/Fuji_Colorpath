
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
			CPSync4D.Project PRJ LEFT JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeId = PST.PrintStandardTypeId  
			LEFT JOIN CPSync4D.SourceProfile SP ON PST.SourceProfileID = SP.SourceProfileID
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
				NULL AS DeviceLinkProfile,
			   '' AS CurrentRunName --Pass blank name as CurrentRunName for validation			
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


























