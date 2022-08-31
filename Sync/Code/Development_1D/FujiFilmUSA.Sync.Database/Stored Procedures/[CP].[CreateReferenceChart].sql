
/****** Object:  StoredProcedure [CP].[CreateReferenceChart]    Script Date: 8/31/2022 10:52:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	,@DeviceFamilyID			TINYINT

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
					Validation_Sync1D_Multi_Color,
					DeviceFamilyID
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
					@Validation_Sync1D_Multi_Color,
					@DeviceFamilyID
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




