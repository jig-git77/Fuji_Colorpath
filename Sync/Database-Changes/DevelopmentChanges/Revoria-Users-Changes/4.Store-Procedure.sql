USE [ColorPathSync_RND]
GO

/****** Object:  StoredProcedure [CPSync4D].[UpdateProject]    Script Date: 12/27/2021 1:05:50 PM ******/
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

/****** Object:  StoredProcedure [CPSync4D].[CreateProject]    Script Date: 12/27/2021 1:05:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
 @IsCustomPlugin BIT,
 @PrintQId NVARCHAR(MAX),
 @IsRevoriaType BIT


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
		LastActiveDate,
		PrintQId,
		IsRevoriaType
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
		GETDATE(),
		@PrintQId,
		@IsRevoriaType
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

/****** Object:  StoredProcedure [CPRevoriaUsers].[GetRevoriaUserProjectList]    Script Date: 12/27/2021 1:05:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [CPRevoriaUsers].[GetRevoriaUserProjectList] 
 @DeviceID INT
,@LicenseTypeID INT    
,@pQId NVARCHAR(MAX)    
AS        
    
  BEGIN TRY	 
  
  IF EXISTS(SELECT * FROM CPSync4D.Project WHERE PrintQId=@pQId AND CPDeviceID=@DeviceID)
  BEGIN
   
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
		,0 AS IsTempTableRecord
	  FROM    
	   [CPSync4D].[Project] PRJ INNER JOIN CPSync4D.PrintCondition PCON     
		ON PCON.ProjectID = PRJ.ProjectID    
		INNER JOIN CP.PrintStandardType PST ON PRJ.PrintStandardTypeID = PST.PrintStandardTypeID
        INNER JOIN CPSync4D.SourceProfile SP     
		ON SP.SourceProfileID = PST.SourceProfileID    		
		  WHERE    
	   PRJ.CPDeviceID = @DeviceID AND (ISNULL(@LicenseTypeID,0) = 0 OR PRJ.LicenseTypeID = @LicenseTypeID) AND
	   PRJ.IsUsed = 1 
	   AND Prj.PrintQId=@pQId  
	   AND IsRevoriaType = 1
      ORDER BY CreateDateTime DESC	 
  END
  ELSE
  BEGIN
	 SELECT         
	   TPRJ.RevoriaUsersProjectId AS ProjectID,    
	   TPRJ.ProjectName,    
	   TPRJ.DeviceID AS CPDeviceID,    
	   0 AS LicenseTypeID,
	   1 AS IsUsed,    
	   0 AS SourceProfileID,  
	   0 AS EnabledValidation,  
	   '' AS ProfileName,  
	   0 AS PrintStandardTypeID,
	   0 AS PrintStandard,
	   TPRJ.Media,    
	   TPRJ.Colorant,    
	   TPRJ.RipSettings,    
	   0 AS MetricTargetPresetID,
	   TPRJ.CreatedDate AS CreateDateTime,
	   TPRJ.ModifiedDate AS EndDateTime,
	   1 AS ProjStatus,
	   0 AS HasDLP,
	   0 AS TotalRuns,
	   0 AS CurrentDlpNo,
	   0 AS ValidationStatus
	   ,0 AS HasSpotProject
	   ,1 AS IsTempTableRecord -- This field is used for Projectlisting js file for visible button
	  FROM    
	   [CPRevoriaUsers].[TempRevoriaUsersProject] TPRJ  	
	    WHERE TPRJ.DeviceId = @DeviceID AND TPRJ.PrintQId=@pQId  	   
      ORDER BY CreateDateTime DESC	
  
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

/****** Object:  StoredProcedure [CPRevoriaUsers].[InsertTempRevoriaUsersProject]    Script Date: 12/27/2021 1:05:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [CPRevoriaUsers].[InsertTempRevoriaUsersProject] 
 
	@ProjectName NVARCHAR(MAX),
	@UserName NVARCHAR(MAX),
	@ProjectGuid UNIQUEIDENTIFIER,
	@DeviceKey NVARCHAR(MAX),
	@Media NVARCHAR(MAX),
	@Colorant NVARCHAR(MAX),
	@RipSettings NVARCHAR(MAX),
	@Notes NVARCHAR(MAX), 
	@PrintQId NVARCHAR(MAX),
	@ServicePostUrl NVARCHAR(MAX)
	
AS
BEGIN
	DECLARE @UserID INT,@DeviceID INT
	SELECT @UserID=ISNULL(UserID,0) FROM [CP].[User] WHERE LoginID=@UserName
	SELECT @DeviceID=ISNULL(DeviceID,0) FROM [CP].[Device] WHERE DeviceKey=@DeviceKey AND IsRevoriaType=1
	
	IF NOT EXISTS(SELECT * FROM [CPRevoriaUsers].[TempRevoriaUsersProject] WHERE PrintQId=@PrintQId AND DeviceId=@DeviceID)
	BEGIN
			INSERT INTO [CPRevoriaUsers].[TempRevoriaUsersProject]
			           ([ProjectName]
			           ,[UserId]
			           ,[ProjectGuid]
			           ,[DeviceId]
			           ,[Media]
			           ,[Colorant]
			           ,[RipSettings]
			           ,[Notes]
			           ,[PrintQId]
			           ,[ServicePostUrl])
			     VALUES
			           (@ProjectName,@UserID,@ProjectGuid,@DeviceID,@Media,@Colorant,@RipSettings,@Notes,@PrintQId,@ServicePostUrl )
			END
	ELSE
	BEGIN
			UPDATE [CPRevoriaUsers].[TempRevoriaUsersProject]
			SET 
					ProjectName=@ProjectName
					,DeviceId=@DeviceId
					,Media=@Media
					,Colorant=@Colorant
					,RipSettings=@RipSettings
					,Notes=@Notes
					,ServicePostUrl=@ServicePostUrl
					,ModifiedDate=GetDate()
				WHERE PrintQId=@PrintQId AND DeviceId=@DeviceID
	END
	SELECT   
			D.DeviceName  
			,DF.DeviceFamilyDesc  
			,B.BranchName  
			,C.CompanyName      
			,D.DeviceID  
			,D.LicenseID  
			,D.DeviceFamilyID  
			,C.CompanyId  
			,B.BranchId 
			,@DeviceID AS PDeviceID
			,@UserID AS PUserId
		FROM CP.Device D INNER JOIN CP.DeviceFamily DF ON
			DF.DeviceFamilyID = D.DeviceFamilyID  
		INNER JOIN CP.License L ON
			L.LicenseID = D.LicenseID 
		INNER JOIN CP.Branch B
			ON B.BranchID = L.BranchID
		INNER JOIN CP.Company C
			ON C.CompanyID = B.CompanyID 
		WHERE
			D.DeviceID=@DeviceID
		ORDER BY
			D.DeviceName  
END


GO

/****** Object:  StoredProcedure [CP].[GetDevices]    Script Date: 12/27/2021 1:05:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [CP].[GetDevices]  
	@DeviceID INT,
	@CurrentUserID INT
AS  
BEGIN
	IF ISNULL(@DeviceID,0) = 0
	BEGIN
		SELECT   
			D.DeviceName  
			,DF.DeviceFamilyDesc  
			,B.BranchName  
			,C.CompanyName      
			,D.DeviceID  
			,D.LicenseID  
			,D.DeviceFamilyID  
			,C.CompanyId  
			,B.BranchId  			
			,D.IsActive  
			,D.IsValidation
			,ISNULL(D.IsFlexo,0) AS IsFlexo
			,ISNULL(L.IsCustomProfOptions,0) AS IsCustomProfOptions
			,ISNULL(L.IsCustomPlugin,0) AS IsCustomPlugin
			,ISNULL(D.IsSupportMulticolor,0) AS IsSupportMulticolor
			,ISNULL(D.TotalNoColor,0) AS TotalNoColor
			,ISNULL(D.Channel_5,0) AS Channel_5
			,ISNULL(D.Channel_6,0) AS Channel_6
			,ISNULL(D.Channel_7,0) AS Channel_7
			,ISNULL(D.Channel_8,0) AS Channel_8
			,ISNULL(D.Channel_9,0) AS Channel_9
			,ISNULL(D.IsRevoriaType,0) AS IsRevoriaType
			,ISNULL(D.DeviceKey,'') AS DeviceKey
		FROM CP.Device D INNER JOIN CP.DeviceFamily DF ON
			DF.DeviceFamilyID = D.DeviceFamilyID  
		INNER JOIN CP.License L ON
			L.LicenseID = D.LicenseID 
		INNER JOIN CP.Branch B
			ON B.BranchID = L.BranchID
		INNER JOIN CP.Company C
			ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
		WHERE
			CP.CheckUserBranch(@CurrentUserID,B.BranchID) = 1
		ORDER BY
			D.DeviceName  
		
		------ Query to get list of total limit of projects by License Types ----------
		SELECT
			DL.DeviceId,DL.LicenseTypeID, DL.NumOfProject
		FROM
			CP.DeviceLicenseType DL INNER JOIN CP.Device D ON
				DL.DeviceID = D.DeviceID
			INNER JOIN CP.License L ON
				L.LicenseID = D.LicenseID 
			INNER JOIN CP.Branch B
				ON B.BranchID = L.BranchID
			INNER JOIN CP.Company C
				ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
			WHERE
				CP.CheckUserBranch(@CurrentUserID,B.BranchID) = 1 AND DL.NumOfProject != 0
    END
    ELSE
    BEGIN
		SELECT   
			D.DeviceName  
			,DF.DeviceFamilyDesc  
			,B.BranchName  
			,C.CompanyName      
			,D.DeviceID  
			,D.LicenseID  
			,D.DeviceFamilyID  
			,C.CompanyId  
			,B.BranchId  			
			,D.IsActive  
			,D.IsValidation
			,ISNULL(D.IsFlexo,0) AS IsFlexo
			,ISNULL(L.IsCustomProfOptions,0) AS IsCustomProfOptions
			,ISNULL(L.IsCustomPlugin,0) AS IsCustomPlugin
			,ISNULL(D.IsSupportMulticolor,0) AS IsSupportMulticolor
			,ISNULL(D.TotalNoColor,0) AS TotalNoColor
			,ISNULL(D.Channel_5,0) AS Channel_5
			,ISNULL(D.Channel_6,0) AS Channel_6
			,ISNULL(D.Channel_7,0) AS Channel_7
			,ISNULL(D.Channel_8,0) AS Channel_8
			,ISNULL(D.Channel_9,0) AS Channel_9
			,ISNULL(D.IsRevoriaType,0) AS IsRevoriaType
			,ISNULL(D.DeviceKey,'') AS DeviceKey
		FROM CP.Device D INNER JOIN CP.DeviceFamily DF ON
			DF.DeviceFamilyID = D.DeviceFamilyID  
		INNER JOIN CP.License L ON
			L.LicenseID = D.LicenseID  
		INNER JOIN CP.Branch B
			ON B.BranchID = L.BranchID
		INNER JOIN CP.Company C
			ON C.CompanyID = B.CompanyID AND CP.CheckUserCompany(@CurrentUserID,C.CompanyID) = 1
		WHERE
			D.DeviceID = @DeviceID


		------ Query to get list of total limit of projects by License Types ----------
		SELECT DeviceId,LicenseTypeId,NumOfProject FROM CP.DeviceLicenseType WHERE DeviceID = @DeviceID AND NumOfProject != 0

	
    END  
END






GO

/****** Object:  StoredProcedure [CP].[InsertDevice]    Script Date: 12/27/2021 1:05:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CP].[InsertDevice]
 @LicenseID			INT
,@DeviceName		NVARCHAR(50)
,@DeviceFamilyID	TINYINT
,@IsActive			BIT
,@IsValidation			BIT
,@LicenseDetail		XML
,@IsFlexo			BIT
,@IsSupportMulticolor BIT
,@TotalNoColor TINYINT
,@Channel_5 TINYINT
,@Channel_6 TINYINT
,@Channel_7 TINYINT
,@Channel_8 TINYINT
,@Channel_9 TINYINT
,@IsRevoriaType BIT
,@DeviceKey NVARCHAR(50)
AS
BEGIN
	
	IF EXISTS (SELECT * FROM [CP].Device Where Upper(DeviceName) = Upper(@DeviceName) AND LicenseID = @LicenseID)  
				BEGIN  
					Return -1
				END   

	BEGIN TRY
		BEGIN TRAN
		
		INSERT INTO	CP.Device 
			(
				 LicenseID
				,DeviceName
				,DeviceFamilyID
				,IsActive
				,IsValidation
				,IsFlexo
				,IsSupportMulticolor
				,TotalNoColor
				,Channel_5
				,Channel_6
				,Channel_7
				,Channel_8
				,Channel_9
				,IsRevoriaType
				,DeviceKey
			)
		VALUES 
			(
				 @LicenseID
				,@DeviceName
				,@DeviceFamilyID
				,@IsActive
				,@IsValidation
				,@IsFlexo
				,@IsSupportMulticolor
				,@TotalNoColor
				,@Channel_5
				,@Channel_6
				,@Channel_7
				,@Channel_8
				,@Channel_9
				,@IsRevoriaType
				,@DeviceKey
			) 		
		DECLARE @DeviceID INT
		SET @DeviceID = @@IDENTITY

		--INSERTING LICENSE DETAILS
		;WITH LicenseDetails AS
		(
			SELECT           
				[LicenseDetailXml].value('@LicenseTypeID', 'TINYINT') AS LicenseTypeID,
				[LicenseDetailXml].value('@NumOfProject', 'INT') AS NumOfProject
			FROM
				@LicenseDetail.
					nodes('/LicenseDetails/LicenseDetail') p([LicenseDetailXml])
		)
		
		INSERT INTO CP.DeviceLicenseType
			(
				DeviceID,
				LicenseTypeID,
				NumOfProject
			)
		SELECT
				@DeviceID,
				LicenseTypeID,
				NumOfProject
		FROM
				LicenseDetails
		  
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

/****** Object:  StoredProcedure [CP].[UpdateDevice]    Script Date: 12/27/2021 1:05:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [CP].[UpdateDevice]

 @DeviceID			INT
,@LicenseID			INT
,@DeviceName		NVARCHAR(50)
,@DeviceFamilyID	TINYINT
,@IsActive			BIT
,@IsValidation		BIT
,@LicenseDetail		XML
,@IsFlexo			BIT
,@IsSupportMulticolor BIT
,@TotalNoColor TINYINT
,@Channel_5 TINYINT
,@Channel_6 TINYINT
,@Channel_7 TINYINT
,@Channel_8 TINYINT
,@Channel_9 TINYINT
,@IsRevoriaType BIT
,@DeviceKey NVARCHAR(50)
AS
BEGIN
			 
	IF EXISTS (SELECT * FROM CP.Device Where DeviceID <> @DeviceID AND Upper(DeviceName) = Upper(@DeviceName) AND LicenseID = @LicenseID)  
	BEGIN  
		Return -1
	END   
	
	BEGIN TRY
		BEGIN TRAN
		
		UPDATE	CP.Device 
		SET 
				 LicenseID = @LicenseID
				,DeviceName = @DeviceName
				,DeviceFamilyID = @DeviceFamilyID
				,IsActive = @IsActive
				,IsValidation = @IsValidation
				,IsFlexo = @IsFlexo
				,IsSupportMulticolor=@IsSupportMulticolor
				,TotalNoColor=@TotalNoColor
				,Channel_5=@Channel_5
				,Channel_6=@Channel_6
				,Channel_7=@Channel_7
				,Channel_8=@Channel_8
				,Channel_9=@Channel_9
				,IsRevoriaType=@IsRevoriaType
				,DeviceKey=@DeviceKey
		WHERE
				DeviceID = @DeviceID				

		--UPDATING LICENSE DETAILS
		DELETE FROM CP.DeviceLicenseType WHERE DeviceID = @DeviceID

		;WITH LicenseDetails AS
		(
			SELECT           
				[LicenseDetailXml].value('@LicenseTypeID', 'TINYINT') AS LicenseTypeID,
				[LicenseDetailXml].value('@NumOfProject', 'INT') AS NumOfProject
			FROM
				@LicenseDetail.
					nodes('/LicenseDetails/LicenseDetail') p([LicenseDetailXml])
		)
		
		INSERT INTO CP.DeviceLicenseType
			(
				DeviceID,
				LicenseTypeID,
				NumOfProject
			)
		SELECT
				@DeviceID,
				LicenseTypeID,
				NumOfProject
		FROM
				LicenseDetails

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


