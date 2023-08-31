
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
 @DestinationProfile VARBINARY(MAX) --This parameter added for CustomBCO Data


AS  
BEGIN
--	IF EXISTS (SELECT * FROM CPSync4D.SourceProfile Where Upper(ProfileName) = cast(Upper(@ProjectName) as varchar) + '.icc')
--		BEGIN  
--			Return -1
--		END   

	BEGIN TRY  
	 BEGIN TRAN  

	  IF(@LicenseTypeID = 14) --IF it is custom BCO then optimization run will set by default 3
	  BEGIN 
			SET @Status=3
	  END
	
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
		CASE WHEN @PrintStandardTypeID != 0 THEN @PrintStandardTypeID ELSE NULL END,  
		CASE WHEN @LicenseTypeID = 14 THEN @DestinationProfile ELSE NULL END,  
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

	  IF(@LicenseTypeID != 14) --IF it is not custom BCO then we store other information to associate table
	  BEGIN 
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
	  END
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
