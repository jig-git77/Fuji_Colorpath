
GO
/****** Object:  StoredProcedure [CP].[ValidateApplicationMeasurement]    Script Date: 8/6/2020 6:27:30 PM ******/
SET ANSI_NULLS ON
GO



GO
/****** Object:  StoredProcedure [CP].[InsertMeasurementPluginStatus]    Script Date: 8/6/2020 6:24:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [CP].[InsertMeasurementPluginStatus]
 (@UserID int,
 @ProjectID INT,
 @OptimizationRunID int,
 @ProfileType varchar(50),
 @DeviceSerial nvarchar(100),
 @InstrumentTypeId tinyint,
 @LicenseType tinyint
 )
AS  
BEGIN TRY   	
	
	IF(@OptimizationRunID = 0)
	BEGIN
		SET @OptimizationRunID=NULL;
	END
	IF EXISTS (SELECT * FROM CP.MeasurementPluginStatus WHERE ProjectID != @ProjectID  AND MeaDeviceSerialNo = @DeviceSerial AND InstrumentTypeID = @InstrumentTypeId )
	BEGIN
			DELETE FROM CP.MeasurementPluginStatus WHERE ProjectID != @ProjectID AND MeaDeviceSerialNo = @DeviceSerial AND InstrumentTypeID = @InstrumentTypeId
	END
	IF EXISTS (SELECT * FROM CP.MeasurementPluginStatus WHERE ProjectID = @ProjectID  AND MeaDeviceSerialNo != @DeviceSerial)
	BEGIN
			DELETE FROM CP.MeasurementPluginStatus WHERE ProjectID = @ProjectID AND MeaDeviceSerialNo != @DeviceSerial
	END
	IF NOT EXISTS ( SELECT 1 FROM CP.MeasurementPluginStatus WHERE MeaDeviceSerialNo = @DeviceSerial AND InstrumentTypeID = @InstrumentTypeId)
	BEGIN
		INSERT INTO CP.MeasurementPluginStatus
			   (UserID
			   ,ProjectID
			   ,OptimizationRunID
			   ,ProfileType
			   ,MeaDeviceSerialNo
			   ,InstrumentTypeID
			   ,EntryDate
			   ,IsActive
			   ,LicenseTypeID)
		 SELECT
			   @UserID
			   ,@ProjectID
			   ,@OptimizationRunID
			   ,@ProfileType     
			   ,@DeviceSerial
			   ,@InstrumentTypeId   
			   ,GETDATE()
			   ,1
			   ,@LicenseType
	END
	ELSE 
	BEGIN
		UPDATE CP.MeasurementPluginStatus
		SET ProjectID = @ProjectID,
			OptimizationRunID=@OptimizationRunID,
			ProfileType=@ProfileType,
			EntryDate=GETDATE(),
			MeaDeviceSerialNo=@DeviceSerial,
			InstrumentTypeId=@InstrumentTypeId,
			LicenseTypeID=@LicenseType,
			isActive=1
		WHERE MeaDeviceSerialNo=@DeviceSerial AND InstrumentTypeID=@InstrumentTypeId
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
	RAISERROR(@ERROR_MESSAGE,@ERROR_SEVERITY,@ERROR_STATE) /******Raise Error backto client *****/ 
END CATCH
GO












GO
/****** Object:  StoredProcedure [CPSync4D].[UpdateProject]    Script Date: 8/6/2020 6:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

			
	
	IF @LicenseTypeId = 1 
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
	END

	
	---- Update table entry into "[CP].[MeasurementPluginStatus]" for this ProjectID for EntryDate, because Project just got updated
	UPDATE [CP].[MeasurementPluginStatus] SET [EntryDate] = GETDATE() WHERE [ProjectID] = @ProjectID

	 -- UPDATE [CP].[MeasurementPluginStatus]
		--SET [EntryDate] = GETDATE(),
		--	[MeaDeviceSerialNo] = (SELECT [SerialNumber] FROM [CP].[MeasurementDevice] WHERE [MeasurementDeviceID] = 
		--							CASE 
		--								WHEN [ProfileType] = 'Validation' THEN @VMeasurementDeviceID
		--								ELSE @MeasurementDeviceID
		--							END
		--							AND [ProjectID] = @ProjectID)
		--WHERE [ProjectID] = @ProjectID
			

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

























