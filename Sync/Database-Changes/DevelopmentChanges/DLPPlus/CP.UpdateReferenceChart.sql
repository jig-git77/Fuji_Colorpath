
GO

/****** Object:  StoredProcedure [CP].[UpdateReferenceChart]    Script Date: 12/30/2020 5:37:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Dhruti Patel>
-- =============================================
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
,@IsSync4D_Plus					TINYINT
,@Validation_4DPlus				TINYINT
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


